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

namespace UStellar.Core
{
    public class UStellarManager
    {
        private static Network network = null;
        private static Server server = null;

        public static void Init(bool debug = true)
        {
            //Enable/Disable Debug.
            UStellarDebug.SetDebug(debug);

            UStellarDebug.Debug(string.Concat("Stellar SDK Init"));

            //Check if Network is null for give a simple warning.
            if (network == null)
            {
                UStellarDebug.Debug("Network is not setup, please set the one from Stellar or a custom one", DebugType.Warning);
            }
        }

        public static void SetStellarPublicNetwork()
        {
            Network network = new Network(UStellarUtils.STELLAR_PUBLIC_NETWORK_PASSPHRASE);
            Server server = new Server(UStellarUtils.STELLAR_PUBLIC_SERVER_URL);
            SetNetwork(network, server);
        }

        public static void SetStellarTestNetwork()
        {
            Network network = new Network(UStellarUtils.STELLAR_TEST_NETWORK_PASSPHRASE);
            Server server = new Server(UStellarUtils.STELLAR_TEST_SERVER_URL);
            SetNetwork(network, server);
        }

        public static Network GetNetwork()
        {
            return network;
        }

        public static void SetNetwork(Network network, Server server)
        {
            UStellarManager.network = network;
            stellar_dotnetcore_sdk.Network.Use(network);

            UStellarDebug.Debug(string.Concat("Network Setup", Environment.NewLine,
                                              "Passphrase: ", network.NetworkPassphrase), DebugType.Info);

            SetServer(server);
        }

        public static Server GetServer()
        {
            return server;
        }

        public static void SetServer(Server server)
        {
            UStellarManager.server = server;

            UStellarDebug.Debug("Server Setup", DebugType.Info);
        }
    }
}