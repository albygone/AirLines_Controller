using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AlbyAirLines.Controllers.cockpit
{
    public class SocketCockPit
    {
        private int Port { get; }
        private IPAddress Ip { get; }

        private Socket _client;

        public delegate void SocketClientHandler(string msg);

        public event SocketClientHandler Error;

        public SocketCockPit(string ip, int port)
        {
            Ip = IPAddress.Parse(ip);
            Port = port;
        }

        public void OpenConnection()
        {
            try
            {
                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _client.Connect(Ip.ToString(), Port);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        public void CloseConnection()
        {
            _client.Shutdown(SocketShutdown.Both);
            _client.Close();
        }

        public string SendData(string data, bool wait = true)
        {
            string response = "";

            try
            {
                data += "<EOF>";

                _client.Send(Encoding.ASCII.GetBytes(data));

                if (wait)
                    response = WaitForResponse();
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
                while (_client.Connected)
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
