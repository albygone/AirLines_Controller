using AlbyLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading;

namespace AlbyAirLines.Controllers.cockpit
{
    public class CockPit
    {
        private AlbySqlControllerMultiple _sqlController;
        private SocketCockPit SocketCockPit;

        private AirPlaneClientModel apc;

        private string DbPath = "";

        public Dictionary<string, (double, double)> Airports = new Dictionary<string, (double, double)>();

        public delegate void ErrorDelegate(string message);
        public event ErrorDelegate Error;

        public delegate void StopFlightDelegate();
        public event StopFlightDelegate StopFlight;

        public CockPit(string path)
        {
            DbPath = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{path}\\DataBase\\AAL_DB.mdf\";Integrated Security=True;Connect Timeout=30";

            try
            {
                _sqlController = new AlbySqlControllerMultiple(DbPath);
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }
        }

        public bool Start(IPAddress ip, string airportName)
        {
            bool ok = false;
            try
            {
                SocketCockPit = new SocketCockPit(ip.ToString(), 10000);
                SocketCockPit.Error += ErrorHandler;

                if (SocketCockPit.OpenConnection())
                {
                    (double lon, double lat) = airportName != "" ? Airports[airportName] : (0, 0);

                    apc = new AirPlaneClientModel(lon, lat, "Personal Airplane", 'M', "");

                    ok = true;
                }
                else
                {
                    if (Error != null)
                        Error("Connessione non riuscita");
                }
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }

            return ok;
        }

        public void Stop()
        {
            if (SocketCockPit != null)
            {
                Thread.Sleep(100);
                SocketCockPit.SendData("close", false);
                SocketCockPit.CloseConnection();
            }
        }

        public void SendPosition()
        {
            Random rnd = new Random();

            try
            {
                string response = SocketCockPit.SendJsonData(apc);

                if (response != "200")
                    StopFlight();
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }
        }

        public List<string> GetAirports()
        {
            List<string> lstAirportsName = new List<string>();

            try
            {
                DataTable dt = (DataTable)_sqlController.Query("SELECT * FROM airports");

                foreach (DataRow airport in dt.Rows)
                {
                    Airports.Add(airport[1].ToString(), (Convert.ToDouble(airport[2]), Convert.ToDouble(airport[3])));
                    lstAirportsName.Add(airport[1].ToString());
                }
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error(ex.Message);
            }

            return lstAirportsName;
        }

        public void CalculateNextPosition(Direction direction, double delta)
        {
            switch (direction)
            {
                case Direction.up:
                    apc.Latitude += delta;
                    break;
                case Direction.down:
                    apc.Latitude -= delta;
                    break;
                case Direction.left:
                    apc.Longitude -= delta;
                    break;
                case Direction.right:
                    apc.Longitude += delta;
                    break;
            }

            //pacman effect

            if (apc.Longitude < -180)
                apc.Longitude = 180;
            else if (apc.Longitude > 180)
                apc.Longitude = -180;

            if (apc.Latitude < -90)
                apc.Latitude = 90;
            else if (apc.Latitude > 90)
                apc.Latitude = -90;
        }

        private void ErrorHandler(string message)
        {
            if (Error != null)
                Error(message);
        }
    }

    public enum Direction
    {
        up,
        down,
        left,
        right
    }
}
