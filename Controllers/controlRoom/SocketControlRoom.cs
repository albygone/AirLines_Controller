using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AlbyAirLines.Controllers.controlRoom
{
    public class SocketControlRoom
    {
        private int MaximumConnection = 1;
        private int Port { get; }
        private IPAddress Ip { get; }

        private Socket _server;
        private EndPoint _endPoint;
        private Thread _mainThread;

        public delegate void SocketServerHandler(string msg);
        public delegate string ResponseHandler(string received);

        public event ResponseHandler ClientConnected;
        public event SocketServerHandler Error;

        public SocketControlRoom(string ip, int port)
        {
            Ip = IPAddress.Parse(ip);
            Port = port;
        }

        public void Start()
        {
            try
            {
                _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _endPoint = new IPEndPoint(Ip, Port);
                _server.Bind(_endPoint);
                _server.Listen(MaximumConnection);

                _mainThread = new Thread(() => Listen());
                _mainThread.Start();
            }
            catch (Exception ex)
            {
                if (Error != null) Error(ex.Message);
            }
        }

        private void Listen()
        {
            try
            {
                while (true)
                {
                    Socket serverClone = _server.Accept();

                    AcceptRequestHandler(serverClone);
                }
            }
            catch (Exception ex)
            {
                if (Error != null) Error(ex.Message);
            }
        }

        private void AcceptRequestHandler(Socket serverClone)
        {
            byte[] b = new byte[1024];
            string received = "";
            bool fetch = true;

            try
            {
                while (fetch)
                {
                    serverClone.Receive(b);

                    received = Encoding.UTF8.GetString(b);

                    int eof = received.IndexOf("<EOF>");

                    if (eof != -1)
                    {
                        received = received.Substring(0, eof);

                        if (received != "close")
                        {
                            string responseMessage = "";

                            if (ClientConnected != null) responseMessage = ClientConnected(received) + "<EOF>";

                            if (serverClone.Connected)
                                serverClone.Send(Encoding.ASCII.GetBytes(responseMessage));
                        }
                        else
                            fetch = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Error != null) Error(ex.Message);
            }
        }
    }
}
