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
    public class CreateAccount : Example
    {
        public override string id
        {
            get
            {
               return "CREATE ACCOUNT";
            }
        }

        public Text log;

        public override void Run()
        {
            //Create new random Keypair
            KeyPair newKeyPair = KeyPair.Random();

            //Build the message for the log
            string logMessage = string.Empty;
            logMessage = string.Concat("Public: ", newKeyPair.AccountId, Environment.NewLine);
            logMessage += string.Concat(Environment.NewLine, "Secret: ", newKeyPair.SecretSeed);
            WriteToLog(logMessage);
        }

        private void WriteToLog(string message)
        {
            log.text = message;
        }
    }
}