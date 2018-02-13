using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.requests;
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
    public class SendMoney : Example
    {
        public override string id
        {
            get
            {
                return "SEND MONEY";
            }
        }

        public Text log;
        public string sourceSecretSeed = "SAN3TG3EK55WPVKIELONQ37UL74JBXUCCNASDBC2Q7LQDN7GB6Q5CKXZ";
        public string destinationPublicAddress = "GCSAVWAQ4BXYV2AGFHYZDOTOHZMP3LAZYWP4MBFXU6WBC3KQT5CXAHZF";

        private KeyPair sourceKeyPair;
        private KeyPair destinationKeyPair;

        public override void Run()
        {
            RunAsync();
        }

        private async void RunAsync() 
        {
            Server server = UStellarManager.GetServer();

            //Source Account KeyPair
            //In this case we need the secret seed. (Usually only Wallets will do this..)
            sourceKeyPair = KeyPair.FromSecretSeed(sourceSecretSeed);

            //Destination Account KeyPair
            //Here we only take the public address.
            destinationKeyPair = KeyPair.FromAccountId(destinationPublicAddress);

            //Check if the destination account exists in the server.
            WriteToLog("Checking if destination account exists in server", 0);
            await server.Accounts.Account(destinationKeyPair);
            WriteToLog("Done", 1);

            //Load up to date information in source account
            await server.Accounts.Account(sourceKeyPair);

            AccountResponse sourceAccountResponse = await server.Accounts.Account(sourceKeyPair);
            Account sourceAccount = new Account(sourceAccountResponse.KeyPair, sourceAccountResponse.SequenceNumber);

            //Create the Asset to send and put the amount we are going to send.
            Asset asset = new AssetTypeNative();
            string amount = "1";

            PaymentOperation operation = new PaymentOperation.Builder(destinationKeyPair, asset, amount).SetSourceAccount(sourceAccount.KeyPair).Build();
            Transaction transaction = new Transaction.Builder(sourceAccount).AddOperation(operation).Build();

            //Sign Transaction
            WriteToLog("Signing Transaction", 2);
            transaction.Sign(sourceKeyPair);
            WriteToLog("Done", 1);

            //Try to send the transaction
            try
            {
                WriteToLog("Sending Transaction", 2);
                await server.SubmitTransaction(transaction);
                WriteToLog("Success!", 1);
            }
            catch (Exception exception)
            {
                WriteToLog("Something went wrong", 2);
                WriteToLog("Exception: " + exception.Message, 1);
                // If the result is unknown (no response body, timeout etc.) we simply resubmit
                // already built transaction:
                // SubmitTransactionResponse response = server.submitTransaction(transaction);
            }
        }

        private void WriteToLog(string message, int newLines)
        {
            string finalMessage = log.text;

            for (int i = 0; i < newLines; i++)
            {
                finalMessage += Environment.NewLine;
            }

            finalMessage += message;

            log.text = finalMessage;
        }
    }
}