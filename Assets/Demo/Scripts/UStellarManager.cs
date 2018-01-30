using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.requests;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

public class UStellarManager
{
    public static UStellarProductsDB productsDB;

    //Parameters
    private static string destinationPublic = "YOURKEY";
    private static string network = "";

    private static Server server;
    private static string network_passphrase = "Test SDF Network ; September 2015";
    private static string secret = "";

    //Test
    public static bool test = true;
    private static string sourceTestPublic = "GDF5HY6VD42NXEJTQHMYDJ2BYIDN6MP2JW5ULF6EVDDF5NZAYRT2ACQK";
    private static string sourceTestSecret = "SAN3TG3EK55WPVKIELONQ37UL74JBXUCCNASDBC2Q7LQDN7GB6Q5CKXZ";

    private static string destinationTestPublic = "GCSAVWAQ4BXYV2AGFHYZDOTOHZMP3LAZYWP4MBFXU6WBC3KQT5CXAHZF";



    public static void Init()
    {
        //Workaround of Unity
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

        //Set Stellar Data
        SetNetwork();
        SetAccount();

        //Load Products Database
        LoadProductsDB();
    }

    public static void SetNetwork()
    {
        if (test)
        {
            network = "test";
            stellar_dotnetcore_sdk.Network.UseTestNetwork();
            server = new Server("https://horizon-testnet.stellar.org");
        }
        else
        {
            network = "public";
            stellar_dotnetcore_sdk.Network.UsePublicNetwork();
        }
    }

    public static void SetAccount()
    {
        if (IsTestNetwork())
        {
            secret = sourceTestSecret;
            destinationPublic = destinationTestPublic;
        }
    }

    public static void LoadProductsDB()
    {
        //Let's make a copy of the original DB
        UStellarProductsDB originalProductsDB = (UStellarProductsDB)Resources.Load("Products", typeof(UStellarProductsDB));
        productsDB = UnityEngine.Object.Instantiate(originalProductsDB) as UStellarProductsDB;    
    }

    public static bool IsTestNetwork()
    {
        if (network == "test")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static async void Buy(string id)
    {
        //Source
        KeyPair sourceKeyPair = KeyPair.FromSecretSeed(sourceTestSecret);

        //Destination
        KeyPair destinationKeyPair = KeyPair.FromAccountId(destinationPublic);

        //Check if the address exists in the server.
        await server.Accounts.Account(destinationKeyPair);

        //Load up to date information in source account
        await server.Accounts.Account(sourceKeyPair);

        AccountResponse sourceAccountResponse = await server.Accounts.Account(sourceKeyPair);
        Account sourceAccount = new Account(sourceAccountResponse.KeyPair, sourceAccountResponse.SequenceNumber);

        //Get Product
        UStellarProduct product = GetProduct(id);

        Asset asset = null;
        string amount = product.amount;

        //Check if we have a native asset or not
        if (product.currency.ToLower() == "xlm")
        {
            asset = new AssetTypeNative();
        }
        else
        {
            asset = Asset.CreateNonNativeAsset(product.currency, sourceKeyPair);
        }

        PaymentOperation operation = new PaymentOperation.Builder(destinationKeyPair, asset, amount).SetSourceAccount(sourceAccount.KeyPair).Build();
        Transaction transaction = new Transaction.Builder(sourceAccount).AddOperation(operation).Build();

        //Sign Transaction
        transaction.Sign(sourceKeyPair);

        //Try to send the transaction
        try
        {
            SubmitTransactionResponse response = await server.SubmitTransaction(transaction);
            Debug.Log("Success!");
            Debug.Log(response);
        }
        catch (Exception exception)
        {
            Debug.Log("Something went wrong!");
            Debug.Log(exception.Message);

            // If the result is unknown (no response body, timeout etc.) we simply resubmit
            // already built transaction:
            // SubmitTransactionResponse response = server.submitTransaction(transaction);
        }
    }

    public static UStellarProduct GetProduct(string id)
    {
        UStellarProduct product = null;
        productsDB.products.TryGetValue(id, out product);
        return product;
    }


    //Workaround... I don't like this and needs to be changed.
    public static bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }
}
