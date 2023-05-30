using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AlbyAirLines
{
    public class SocketServer
    {
        private int MaximumConnection { get; }
        private int Port { get; }
        private IPAddress Ip { get; }

        private Socket _server;
        private EndPoint _endPoint;
        private Thread _mainThread;

        public delegate void SocketHandlerDelegate(string msg);
        public delegate string ResponseHandler(string received);

        public event ResponseHandler ClientConnected;
        public event SocketHandlerDelegate Error;

        public SocketServer(int maximumConnection, int port)
        {
            if (maximumConnection < 1)
                throw new Exception("Wrong parameters");

            MaximumConnection = maximumConnection;
            Ip = IPAddress.Any;
            Port = port;
        }

        public void Start(bool syncronous = false)
        {
            try
            {
                _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _endPoint = new IPEndPoint(Ip, Port);
                _server.Bind(_endPoint);
                _server.Listen(MaximumConnection);

                if (syncronous)
                    Listen();
                else
                {
                    _mainThread = new Thread(() => Listen());
                    _mainThread.Start();
                }
            }
            catch (Exception ex)
            {
                if (Error != null) Error(ex.Message);
            }
        }

        public void Close()
        {
            try
            {
                _server.Close();
                _mainThread.Abort();
            }
            catch { }
        }

        private void Listen()
        {
            try
            {
                while (true)
                {
                    Socket serverClone = _server.Accept();

                    if (serverClone != null)
                    {
                        Thread acceptRequest = new Thread(() => AcceptRequestHandler(serverClone));
                        acceptRequest.Start();
                    }
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

            try
            {
                while (true)
                {
                    serverClone.Receive(b);

                    received = Encoding.UTF8.GetString(b);

                    int eof = received.IndexOf("<EOF>");

                    received = received.Substring(0, eof);

                    string responseMessage = "";

                    if (ClientConnected != null) responseMessage = ClientConnected(received);

                    serverClone.Send(Encoding.ASCII.GetBytes(responseMessage));

                    if (!string.IsNullOrEmpty(responseMessage))
                        return;
                }
            }
            catch (Exception ex)
            {
                if (Error != null) Error(ex.Message);
            }

        }
    }
}
