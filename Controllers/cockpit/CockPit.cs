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
        private AlbySqlControllerSingle _sqlController;
        private SocketCockPit SocketCockPit;

        private AirPlaneClientModel apc;

        private string DbPath =
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\albyg\\Documents\\Scuola\\2022-2023\\Informatica\\Progetti\\Alby air lines\\DataBase\\AAL_DB.mdf\";Integrated Security=True;Connect Timeout=30";

        public Dictionary<string, (double, double)> Airports = new Dictionary<string, (double, double)>();

        public delegate void ErrorDelegate(string message);
        public event ErrorDelegate Error;

        public delegate void StopFlightDelegate();
        public event StopFlightDelegate StopFlight;

        public CockPit()
        {
            _sqlController = new AlbySqlControllerSingle(DbPath);
        }

        public bool Start(IPAddress ip, string airportName)
        {
            bool ok = false;
            try
            {
                SocketCockPit = new SocketCockPit(ip.ToString(), 10000);
                SocketCockPit.Error += ErrorHandler;

                SocketCockPit.OpenConnection();

                (double lon, double lat) = Airports[airportName];

                apc = new AirPlaneClientModel(lon, lat, "Personal Airplane", 'M', "");

                ok = true;
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

            return ok;
        }

        public void Stop()
        {
            Thread.Sleep(100);
            SocketCockPit.SendData("close", false);
            SocketCockPit.CloseConnection();
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
                Error(ex.Message);
            }
        }

        public List<string> GetAirports()
        {
            DataTable dt = (DataTable)_sqlController.Query("SELECT * FROM airports");
            List<string> lstAirportsName = new List<string>();

            foreach (DataRow airport in dt.Rows)
            {
                Airports.Add(airport[1].ToString(), (Convert.ToDouble(airport[2]), Convert.ToDouble(airport[3])));
                lstAirportsName.Add(airport[1].ToString());
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
