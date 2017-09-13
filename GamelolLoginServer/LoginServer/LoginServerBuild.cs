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
        UPnP mUPnP = null;
        GameServer mGameServer = null;
        LobbyServer mLobbyServer = null;
        public LoginServerBuild()
        {
            IninGameServer();
        }

        private void IninGameServer()
        {

            string name = "TNet Server";
            int tcpPort = 1998;
            int udpPort = 1996;
            string lobbyAddress = null;
            int lobbyPort = 1997;
            bool tcpLobby = false;

            Start(name, tcpPort, udpPort, lobbyAddress, lobbyPort, tcpLobby);
        }

        private void Start(string name, int tcpPort, int udpPort, string lobbyAddress, int lobbyPort, bool useTcp)
        {
            List<IPAddress> ips = Tools.localAddresses;
            string text = "\nLocal IPs: " + ips.size;

            for (int i = 0; i < ips.size; ++i)
            {
                text += "\n  " + (i + 1) + ": " + ips[i];
                if (ips[i] == TNet.Tools.localAddress) text += " (Primary)";
            }

            Console.WriteLine(text + "\n");
            {
             
                mUPnP = new UPnP();
                mUPnP.Start();
                mUPnP.WaitForThreads();

                Tools.Print("External IP: " + Tools.externalAddress);

                if (tcpPort > 0)
                {
                    mGameServer = new GameServer();
                    mGameServer.name = name;

                    if (!string.IsNullOrEmpty(lobbyAddress))
                    {
                        // Remote lobby address specified, so the lobby link should point to a remote location
                        IPEndPoint ip = Tools.ResolveEndPoint(lobbyAddress, lobbyPort);
                        if (useTcp) mGameServer.lobbyLink = new TcpLobbyServerLink(ip);
                        else mGameServer.lobbyLink = new UdpLobbyServerLink(ip);

                    }
                    else if (lobbyPort > 0)
                    {
                        // Server lobby port should match the lobby port on the client
                        if (useTcp)
                        {
                            mLobbyServer = new TcpLobbyServer();
                            mLobbyServer.Start(lobbyPort);
                            if (mUPnP.status != UPnP.Status.Failure) mUPnP.OpenTCP(lobbyPort, OnPortOpened);
                        }
                        else
                        {
                            mLobbyServer = new UdpLobbyServer();
                            mLobbyServer.Start(lobbyPort);
                            if (mUPnP.status != UPnP.Status.Failure) mUPnP.OpenUDP(lobbyPort, OnPortOpened);
                        }

                        mGameServer.lobbyLink = new LobbyServerLink(mLobbyServer);
                    }

                    mGameServer.Start(tcpPort, udpPort);
                    mGameServer.Load("server.dat");
                }
                else if (lobbyPort > 0)
                {
                    if (useTcp)
                    {
                        if (mUPnP.status != UPnP.Status.Failure) mUPnP.OpenTCP(lobbyPort, OnPortOpened);
                        mLobbyServer = new TcpLobbyServer();
                        mLobbyServer.Start(lobbyPort);
                    }
                    else
                    {
                        if (mUPnP.status != UPnP.Status.Failure) mUPnP.OpenUDP(lobbyPort, OnPortOpened);
                        mLobbyServer = new UdpLobbyServer();
                        mLobbyServer.Start(lobbyPort);
                    }
                }

                if (mUPnP.status != UPnP.Status.Failure)
                {
                    if (tcpPort > 0) mUPnP.OpenTCP(tcpPort, OnPortOpened);
                    if (udpPort > 0) mUPnP.OpenUDP(udpPort, OnPortOpened);
                    mUPnP.WaitForThreads();
                }
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(delegate (object sender, EventArgs e) { Dispose(); });


            }

             void OnPortOpened(UPnP up, int port, ProtocolType protocol, bool success)
            {
                if (success)
                {
                    Tools.Print("UPnP: " + protocol.ToString().ToUpper() + " port " + port + " was opened successfully.");
                }
                else
                {
                    Tools.Print("UPnP: Unable to open " + protocol.ToString().ToUpper() + " port " + port);
                }
            }

             void Dispose()
            {
                // Stop the game server
                if (mGameServer != null)
                {
                    mGameServer.Stop();
                    mGameServer = null;
                }

                // Stop the lobby server
                if (mLobbyServer != null)
                {
                    mLobbyServer.Stop();
                    mLobbyServer = null;
                }

                // Close all opened ports
                if (mUPnP != null)
                {
                    mUPnP.Close();
                    mUPnP.WaitForThreads();
                    mUPnP = null;
                }
            }
        }
    }
}