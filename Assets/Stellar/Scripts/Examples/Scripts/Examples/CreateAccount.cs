using stellar_dotnet_sdk;
using System;

namespace UStellar.Examples
{
    /// <summary>
    /// This example let you 'create' a new account.
    /// When you run the example, you will get the public and secret keys.
    /// For this account to exist in the Stellar Network you need to send at least 1 lumen to the PublicKey address.
    /// </summary>
    public class CreateAccount : Example
    {
        public override int id
        {
            get
            {
                return 0;
            }
        }

        public override string title
        {
            get
            {
                return "CREATE ACCOUNT";
            }
        }

        public override string description
        {
            get
            {
                return "This example will generate a KeyPair with a random public address and a secret seed.";
            }
        }

        public override void Run()
        {
            base.Run();
            GenerateAccountKeyPair();
        }

        private void GenerateAccountKeyPair()
        {
            //Create new random Keypair
            KeyPair newKeyPair = KeyPair.Random();

            //Log it
            Log(string.Concat("Public: ", newKeyPair.AccountId), 0);
            Log(string.Concat("Secret: ", newKeyPair.SecretSeed), 2);
        }
    }
}