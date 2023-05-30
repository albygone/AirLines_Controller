using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AlbyAirLines
{
    public class SocketClient
    {
        private int Port { get; }
        private IPAddress Ip { get; }

        private Socket _client;

        public delegate void SocketClientHandler(string msg);
        public event SocketClientHandler Error;

        public SocketClient(string ip, int port)
        {
            Ip = IPAddress.Parse(ip);
            Port = port;
        }

        public string SendData(string data)
        {
            string response = "";

            try
            {
                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _client.Connect(Ip.ToString(), Port);

                data += "<EOF>";

                _client.Send(Encoding.ASCII.GetBytes(data));

                response = WaitForResponse();

                _client.Shutdown(SocketShutdown.Both);
                _client.Close();
            }
            catch (Exception ex)
            {
                if (Error != null) Error(ex.Message);
            }

            return response;
        }

        public string SendJsonData(object data) => SendData(JsonConvert.SerializeObject(data));

        private string WaitForResponse()
        {
            byte[] b = new byte[1024];
            string response = "";

            try
            {
                while (true)
                {
                    if (_client.Available > 0)
                        _client.Receive(b);

                    response = Encoding.UTF8.GetString(b);

                    if (response.IndexOf("<EOF>") != -1)
                        break;
                }
            }
            catch (Exception ex)
            {
                if (Error != null) Error(ex.Message);
                response = "<EOF>";
            }

            return response.Substring(0, response.IndexOf("<EOF>"));
        }
    }
}