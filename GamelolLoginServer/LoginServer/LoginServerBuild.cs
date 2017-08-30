using System;
using TNet;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;


namespace GamelolLoginServer.LoginServer
{
    public class LoginServerBuild
    {
        public LoginServerBuild() {
            IninGameServer();
        }

        private void IninGameServer() {

            string name = "TNet Server";
            int tcpPort = 1995;
            int udpPort = 1996;
            string lobbyAddress = null;
            int lobbyPort = 1997;
            bool tcpLobby = false;

            Start(name, tcpPort, udpPort, lobbyAddress, lobbyPort, tcpLobby);
        }

        private void Start(string name, int tcpPort, int udpPort, string lobbyAddress, int lobbyPort, bool useTcp)
        {
            List<IPAddress> ips = Tools.localAddresses;
            string text = "Local IPs: " + ips.size;

            for (int i = 0; i < ips.size; ++i)
            {
                text += "\n  " + (i + 1) + ": " + ips[i];
                if (ips[i] == TNet.Tools.localAddress) text += " (Primary)";
            }
            Console.WriteLine(text + "\n");

            {
                UPnP up = new UPnP();
                up.WaitForThreads();

                if (up.status == UPnP.Status.Success)
                {
                    Console.WriteLine("Gateway:  " + up.gatewayAddress + "\n");
                }
                else
                {
                    Console.WriteLine("Gateway:  None found\n");
                    up = null;
                }

                GameServer gameServer = null;
                LobbyServer lobbyServer = null;

                if (tcpPort > 0)
                {
                    gameServer = new GameServer();
                    gameServer.name = name;

                    if (!string.IsNullOrEmpty(lobbyAddress))
                    {
                        IPEndPoint ip = Tools.ResolveEndPoint(lobbyAddress, lobbyPort);
                        if (useTcp) gameServer.lobbyLink = new TcpLobbyServerLink(ip);
                        else gameServer.lobbyLink = new UdpLobbyServerLink(ip);

                    }
                    else if (lobbyPort > 0)
                    {
                        // Server lobby port should match the lobby port on the client
                        if (useTcp)
                        {
                            lobbyServer = new TcpLobbyServer();
                            lobbyServer.Start(lobbyPort);
                            if (up != null) up.OpenTCP(lobbyPort, OnPortOpened);
                        }
                        else
                        {
                            lobbyServer = new UdpLobbyServer();
                            lobbyServer.Start(lobbyPort);
                            if (up != null) up.OpenUDP(lobbyPort, OnPortOpened);
                        }

                        // Local lobby server
                        gameServer.lobbyLink = new LobbyServerLink(lobbyServer);
                    }

                    // Start the actual game server and load the save file
                    gameServer.Start(tcpPort, udpPort);
                    gameServer.LoadFrom("server.dat");
                }
                else if (lobbyPort > 0)
                {
                    if (useTcp)
                    {
                        if (up != null) up.OpenTCP(lobbyPort, OnPortOpened);
                        lobbyServer = new TcpLobbyServer();
                        lobbyServer.Start(lobbyPort);
                    }
                    else
                    {
                        if (up != null) up.OpenUDP(lobbyPort, OnPortOpened);
                        lobbyServer = new UdpLobbyServer();
                        lobbyServer.Start(lobbyPort);
                    }
                }

                // Open up ports on the router / gateway
                if (up != null)
                {
                    if (tcpPort > 0) up.OpenTCP(tcpPort, OnPortOpened);
                    if (udpPort > 0) up.OpenUDP(udpPort, OnPortOpened);
                }

                for (; ; )
                {
                    Console.WriteLine("Press 'q' followed by ENTER when you want to quit.\n");
                    string command = Console.ReadLine();
                    if (command == "q") break;
                }
                Console.WriteLine("Shutting down...");

                // Close all opened ports
                if (up != null)
                {
                    up.Close();
                    up.WaitForThreads();
                    up = null;
                }

                // Stop the game server
                if (gameServer != null)
                {
                    gameServer.SaveTo("server.dat");
                    gameServer.Stop();
                    gameServer = null;
                }

                // Stop the lobby server
                if (lobbyServer != null)
                {
                    lobbyServer.Stop();
                    lobbyServer = null;
                }
            }
            Console.WriteLine("There server has shut down. Press ENTER to terminate the application.");
            Console.ReadLine();
        }

        private void OnPortOpened(UPnP up, int port, ProtocolType protocol, bool success)
        {
            if (success)
            {
                Console.WriteLine("UPnP: " + protocol.ToString().ToUpper() + " port " + port + " was opened successfully.");
            }
            else
            {
                Console.WriteLine("UPnP: Unable to open " + protocol.ToString().ToUpper() + " port " + port);
            }
        }
    }
}
