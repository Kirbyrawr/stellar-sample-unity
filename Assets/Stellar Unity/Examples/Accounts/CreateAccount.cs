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

namespace UStellar.Examples
{
	/// <summary>
	/// This example let you 'create' a new account.
	/// When you run the example, you will get the public and secret keys.
	/// For this account to exist in the Stellar Network you need to send at least 1 lumen to the PublicKey address.
	/// </summary>
    public class CreateAccount
    {
        private void Run() 
		{	
			//Create new random Keypair
			KeyPair newKeyPair = KeyPair.Random();

			//Show the keypairs on the console
			Debug.Log(string.Concat("Public: ", newKeyPair.PublicKey));
			Debug.Log(string.Concat("Private: ", newKeyPair.PrivateKey));
		}
    }
}