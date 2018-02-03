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

namespace UStellar.Core
{
    public class UStellarManager
    {
        //Parameters
        private static string network = "";
        private static Server server;
        private static string network_passphrase = "Test SDF Network ; September 2015";

        //Test
        public static bool test = true;

        public static void Init()
        {
            //Workaround of Unity
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

            //Set Stellar Data
            SetNetwork();
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

		public static Server GetServer() 
		{
			return server;
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
}