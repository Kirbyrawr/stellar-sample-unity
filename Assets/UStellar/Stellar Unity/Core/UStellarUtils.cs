using System.Collections;
using System.Collections.Generic;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.requests;

namespace UStellar.Core
{
    public static class UStellarUtils
    {
        //Stellar Network
        public static readonly string STELLAR_PUBLIC_NETWORK_PASSPHRASE = "Public Global Stellar Network ; September 2015";
        public static readonly string STELLAR_PUBLIC_SERVER_URL = "https://horizon.stellar.org";
        public static readonly string STELLAR_TEST_NETWORK_PASSPHRASE = "Test SDF Network ; September 2015";
        public static readonly string STELLAR_TEST_SERVER_URL = "https://horizon-testnet.stellar.org";

        public static string ShortAddress(string address, int charactersAtStart = 5, int charactersAtEnd = 4, string separator = "...")
        {
            return string.Concat(address.Substring(0, charactersAtStart), separator, address.Substring(address.Length - charactersAtEnd, charactersAtEnd));
        }
    }
}