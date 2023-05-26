using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using AlbyLib;
using Newtonsoft.Json;

namespace AlbyAirLines.Controllers
{
    public class Client
    {
        private SocketClient _sc;
        
        private AlbySqlControllerMultiple _sqlController;

        public delegate void UpdateProgressDelegate(int progress);

        public event UpdateProgressDelegate UpdateProgress;

        private string DbPath =
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\albyg\\Documents\\Scuola\\2022-2023\\Informatica\\Progetti\\Alby air lines\\DataBase\\AAL_DB.mdf\";Integrated Security=True;Connect Timeout=30";

        public Client(string ip, int port)
        {
            _sc = new SocketClient(ip, port);
            _sc.Error += HandleError;
            _sqlController = new AlbySqlControllerMultiple(DbPath);
        }

        public string RequestId()
        {
            return _sc.SendData("idRequest<EOF>");
        }

        public DataTable GetAirports()
        {
            return (DataTable)_sqlController.Query("SELECT * FROM airports");
        }
        
        public void SendPositionSequence(int delay, AirPlaneClientModel apc, Action StopTravelling)
        {
            Thread thSend = new Thread(() => SendPositionSequenceHandler(delay, apc, StopTravelling));
            thSend.Start();
        }

        private void SendPositionSequenceHandler(int delay, object data, Action StopTravelling)
        {
            AirPlaneClientModel apc = (AirPlaneClientModel)data;
            
            double totalDistance = GetDistance(apc.FromLat, apc.FromLong, apc.ToLat, apc.ToLong);
            double oldDistance = totalDistance;

            List<(double, double)> lstRoute = new List<(double, double)>();

            while (!(((apc.Longitude <= apc.ToLong + 0.50 && apc.Longitude >= apc.ToLong - 0.50)
                      && (apc.Latitude <= apc.ToLat + 0.50 && apc.Latitude >= apc.ToLat - 0.50))))
            {
                //calculate the new longitude

                if (apc.Longitude != apc.ToLong)
                {
                    if (apc.Longitude > apc.ToLong)
                    {
                        apc.Longitude -= 0.25;
                        
                        if (GetDistance(apc.Latitude, apc.Longitude, apc.ToLat, apc.ToLong) > oldDistance)
                            apc.Longitude += 0.50;
                    }
                    else
                    {
                        apc.Longitude += 0.25;
                        if (GetDistance(apc.Latitude, apc.Longitude, apc.ToLat, apc.ToLong) > oldDistance)
                            apc.Longitude -= 0.50;
                    }
                }
                
                //check if i am at the right latitude

                if (!(apc.Latitude <= apc.ToLat + 0.50 && apc.Latitude >= apc.ToLat - 0.50))
                {
                    if (apc.Latitude > apc.ToLat)
                        apc.Latitude -= 0.25;
                    else
                        apc.Latitude += 0.25;
                }
                else
                    apc.Latitude = apc.ToLat;
                
                //check if i am at the right longitude
                
                if (apc.Longitude <= apc.ToLong + 0.50 && apc.Longitude >= apc.ToLong - 0.50)
                    apc.Longitude = apc.ToLong;
                
                //pacman effect

                if (apc.Longitude < -180)
                    apc.Longitude = 180;
                else if (apc.Longitude > 180)
                    apc.Longitude = -180;

                lstRoute.Add((apc.Longitude, apc.Latitude));
            }
            
            lstRoute.Add((apc.ToLong, apc.ToLat));

            for (int i = 0; i < lstRoute.Count; i++)
            {
                (double longitude, double latitude) = lstRoute[i];

                apc.Longitude = longitude;
                apc.Latitude = latitude;
                
                string response = _sc.SendJsonData(apc);

                if (response != "200")
                {
                    StopTravelling();
                }
                
                double progress = i * 100 / lstRoute.Count;
                
                if(progress <= 100 && progress >= 0)
                    if (UpdateProgress != null)
                        UpdateProgress((int)Math.Round(progress));

                Thread.Sleep(delay);
            }

            if (UpdateProgress != null) UpdateProgress(100);

            StopTravelling();
        }

        private double GetDistance(double lat1, double long1, double lat2, double long2)
        {
            return Math.Acos(
                Math.Sin(lat1* Math.PI/180) * Math.Sin(lat2* Math.PI/180) + Math.Cos(lat1* Math.PI/180)
                * Math.Cos(lat2* Math.PI/180) * Math.Cos((long2 * Math.PI/180) - (long1 * Math.PI/180))) * 6371;
        }

        public void HandleError(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}