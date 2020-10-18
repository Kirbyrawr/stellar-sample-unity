using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.requests;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UStellar.Core;

using UnityEngine.UI;

namespace UStellar.Examples
{
    /// <summary>
    /// This example let you 'create' a new account.
    /// When you run the example, you will get the public and secret keys.
    /// For this account to exist in the Stellar Network you need to send at least 1 lumen to the PublicKey address.
    /// </summary>
    public class SendXLM : Example
    {
        public override int id
        {
            get
            {
                return 2;
            }
        }

        public override string title
        {
            get
            {
                return "SEND XLM";
            }
        }

        public override string description
        {
            get
            {
                return "This example will send 1 lumen (XLM) from one account to other";
            }
        }
        public string source = "SDAEMLPKF5W53EWXIYWGQV54AVKTYMSDFBMHC7ZUO5YC5ZGVWFF3ZFY2";
        public string destination = "GC5B4GM5BEMNQFMHMNIXPGOLJS46KGSKWJV7XUPK5EHIWAGAAREABIHA";

        public override void Run()
        {
            base.Run();
            RunAsync();
        }

        private async void RunAsync()
        {
            Server server = UStellarManager.GetServer();

            KeyPair sourceKeyPair = KeyPair.FromSecretSeed(source);

            //Check if the destination account exists in the server.
            Log("Checking if destination account exists in server", 0);
            await server.Accounts.Account(destination);
            Log("Done");

            //Load up to date information in source account
            await server.Accounts.Account(sourceKeyPair.AccountId);

            AccountResponse sourceAccountResponse = await server.Accounts.Account(sourceKeyPair.AccountId);
            Account sourceAccount = new Account(sourceAccountResponse.AccountId, sourceAccountResponse.SequenceNumber);

            //Create the Asset to send and put the amount we are going to send.
            Asset asset = new AssetTypeNative();
            string amount = "1";

            PaymentOperation operation = new PaymentOperation.Builder(KeyPair.FromAccountId(destination), asset, amount).SetSourceAccount(sourceAccount.KeyPair).Build();
            Transaction transaction = new TransactionBuilder(sourceAccount).AddOperation(operation).Build();

            //Sign Transaction
            Log("Signing Transaction", 2);
            transaction.Sign(KeyPair.FromSecretSeed(source));
            Log("Done");

            //Try to send the transaction
            try
            {
                Log("Sending Transaction", 2);
                await server.SubmitTransaction(transaction);
                Log("Success!", 1);
            }
            catch (Exception exception)
            {
                Log("Something went wrong", 2);
                Log("Exception: " + exception.Message, 1);
                // If the result is unknown (no response body, timeout etc.) we simply resubmit
                // already built transaction:
                // SubmitTransactionResponse response = server.submitTransaction(transaction);
            }
        }
    }
}