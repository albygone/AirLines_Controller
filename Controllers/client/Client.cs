using AlbyLib;
using System;
using System.Data;
using System.Threading;

namespace AlbyAirLines.Controllers
{
    public class Client
    {
        private SocketClient socketClient;

        private AlbySqlControllerMultiple sqlController;

        public delegate void UpdateProgressDelegate(int progress);
        public delegate void ErrorDelegate(string message);
        public delegate void StopTravellingDelegate();

        public event UpdateProgressDelegate UpdateProgress;
        public event StopTravellingDelegate StopTravelling;
        public event ErrorDelegate Error;


        private string DbPath = "";

        public Client(string ip, int port, string path)
        {
            socketClient = new SocketClient(ip, port);
            socketClient.Error += HandleError;

            DbPath = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{path}\\DataBase\\AAL_DB.mdf\";Integrated Security=True;Connect Timeout=30";

            sqlController = new AlbySqlControllerMultiple(DbPath);
        }

        public string RequestId()
        {
            return socketClient.SendData("idRequest<EOF>");
        }

        public DataTable GetAirports()
        {
            DataTable dt = null;
            try
            {
                dt = (DataTable)sqlController.Query("SELECT * FROM airports");
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }
            return dt;
        }

        public Thread SendPositionSequence(int delay, AirPlaneClientModel apc, double delta)
        {
            Thread thSend = new Thread(() => SendPositionSequenceHandler(delay, apc, delta));
            thSend.Start();

            return thSend;
        }

        private void SendPositionSequenceHandler(int delay, object data, double delta)
        {
            AirPlaneClientModel apc = (AirPlaneClientModel)data;

            double totalDistance = GetDistance(apc.FromLat, apc.FromLong, apc.ToLat, apc.ToLong);
            double oldDistance = totalDistance;

            double deltaDouble = delta * 2;

            while (true)
            {
                if (apc.Longitude != apc.ToLong)
                {
                    if (apc.Longitude > apc.ToLong)
                    {
                        apc.Longitude -= delta;

                        if (GetDistance(apc.Latitude, apc.Longitude, apc.ToLat, apc.ToLong) > oldDistance)
                            apc.Longitude += deltaDouble;
                    }
                    else
                    {
                        apc.Longitude += delta;
                        if (GetDistance(apc.Latitude, apc.Longitude, apc.ToLat, apc.ToLong) > oldDistance)
                            apc.Longitude -= deltaDouble;
                    }
                }

                if (!(apc.Latitude <= apc.ToLat + deltaDouble && apc.Latitude >= apc.ToLat - deltaDouble))
                {
                    if (apc.Latitude > apc.ToLat)
                        apc.Latitude -= delta;
                    else
                        apc.Latitude += delta;
                }
                else
                    apc.Latitude = apc.ToLat;

                if (apc.Longitude <= apc.ToLong + deltaDouble && apc.Longitude >= apc.ToLong - deltaDouble)
                    apc.Longitude = apc.ToLong;

                if (apc.Longitude < -180)
                    apc.Longitude = 180;
                else if (apc.Longitude > 180)
                    apc.Longitude = -180;

                if (apc.Latitude < -90)
                    apc.Latitude = 90;
                else if (apc.Latitude > 90)
                    apc.Latitude = -90;

                if ((((apc.Longitude <= apc.ToLong + deltaDouble && apc.Longitude >= apc.ToLong - deltaDouble)
                      && (apc.Latitude <= apc.ToLat + deltaDouble && apc.Latitude >= apc.ToLat - deltaDouble))))
                {
                    if (UpdateProgress != null) UpdateProgress(100);

                    StopTravelling();

                    return;
                }

                string response = socketClient.SendJsonData(apc);

                if (response != "200")
                {
                    StopTravelling();
                }

                double progress = 100 - (GetDistance(apc.Latitude, apc.Longitude, apc.ToLat, apc.ToLong) * 100 / totalDistance);

                if (progress <= 100 && progress >= 0)
                    if (UpdateProgress != null)
                        UpdateProgress((int)Math.Round(progress));

                Thread.Sleep(delay);
            }
        }

        private double GetDistance(double lat1, double long1, double lat2, double long2)
        {
            return Math.Acos(
                Math.Sin(lat1 * Math.PI / 180) * Math.Sin(lat2 * Math.PI / 180) + Math.Cos(lat1 * Math.PI / 180)
                * Math.Cos(lat2 * Math.PI / 180) * Math.Cos((long2 * Math.PI / 180) - (long1 * Math.PI / 180))) * 6371;
        }

        public void HandleError(string message)
        {
            if (message != "Thread interrotto." && Error != null)
                Error(message);
        }
    }
}