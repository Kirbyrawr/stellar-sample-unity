using System.Collections;
using System.Collections.Generic;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.requests;
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
        private static bool debug = false;

        public static void Init()
        {
            //Enable/Disable Debug.
            UStellarDebug.SetDebug(debug);

            //Info
            UStellarDebug.Debug(string.Concat("Stellar SDK - Started"));
        }

        #region Helpers
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
        #endregion

        #region Network
        public static Network GetNetwork()
        {
            //Info
            if(network == null) 
            {
                UStellarDebug.Debug(string.Concat("Stellar SDK - You don't have any 'Network' set up!"), DebugType.Warning);
            }

            return network;
        }

        public static void SetNetwork(Network network, Server server)
        {
            UStellarManager.network = network;
            stellar_dotnet_sdk.Network.Use(network);

            UStellarDebug.Debug(string.Concat("Network Setup", Environment.NewLine,
                                              "Passphrase: ", network.NetworkPassphrase), DebugType.Info);

            SetServer(server);
        }
        #endregion

        #region Server
        public static Server GetServer()
        {
            //Info
            if(server == null) 
            {
                UStellarDebug.Debug(string.Concat("Stellar SDK - You don't have any 'Server' set up!"), DebugType.Warning);
            }

            return server;
        }

        public static void SetServer(Server server)
        {
            UStellarManager.server = server;

            UStellarDebug.Debug("Server Setup", DebugType.Info);
        }
        #endregion
    }
}