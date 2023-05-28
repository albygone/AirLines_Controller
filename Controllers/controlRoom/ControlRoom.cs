using AlbyAirLines;
using AlbyAirLines.Controllers.controlRoom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AlbyLib.controlRoom
{
    public class ControlRoom
    {
        private AlbySqlControllerSingle _sqlController;
        private SocketControlRoom _socketControlRoom;

        private string DbPath =
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\albyg\\Documents\\Scuola\\2022-2023\\Informatica\\Progetti\\Alby air lines\\DataBase\\AAL_DB.mdf\";Integrated Security=True;Connect Timeout=30";

        private List<PictureBox> lstPcbAirPlane;

        public (int, int) cockPitPosition;

        private PictureBox pcbMap;

        public ControlRoom(PictureBox pcbMap)
        {
            this.pcbMap = pcbMap;

            _sqlController = new AlbySqlControllerSingle(DbPath);
            _socketControlRoom = new SocketControlRoom("127.0.0.1", 10000);
            _socketControlRoom.ClientConnected += CockPitConnected;

            _socketControlRoom.Start();
        }

        private string CockPitConnected(string data)
        {
            string response = "200";

            AirPlaneClientModel apc = JsonConvert.DeserializeObject<AirPlaneClientModel>(data);

            (double x, double y) = GetPixelPosition(apc.Longitude, apc.Latitude, pcbMap.Width);

            int xPlot = (int)Math.Round(pcbMap.Location.X + pcbMap.Width / 2 + x);
            int yPlot = (int)Math.Round(pcbMap.Location.Y + pcbMap.Height / 2 - y);

            cockPitPosition = (xPlot, yPlot);

            return response;
        }

        public DataTable GetLivePositions()
        {
            return (DataTable)_sqlController.Query("SELECT * FROM live_air_traffic");
        }

        public PictureBox PlotPosition(AirPlaneClientModel apc, string id)
        {
            (double x, double y) = GetPixelPosition(apc.Longitude, apc.Latitude, pcbMap.Width);

            int xPlot = (int)Math.Round(pcbMap.Location.X + pcbMap.Width / 2 + x);
            int yPlot = (int)Math.Round(pcbMap.Location.Y + pcbMap.Height / 2 - y);

            PictureBox pcb = ShowNewPcb(xPlot, yPlot, id);

            return pcb;
        }

        public (double x, double y) GetPixelPosition(double longitude, double latitude, int width)
        {
            double x = width / (2 * Math.PI) * (longitude * (Math.PI / 180));

            double y = width / (2 * Math.PI) * Math.Log(Math.Tan((Math.PI / 4) + ((latitude * (Math.PI / 180) / 2))));

            return (x, y);
        }

        public PictureBox ShowNewPcb(int xplot, int yplot, string id)
        {
            int index = lstPcbAirPlane.FindIndex(pcb => (string)pcb.Tag == id);

            if (index == -1)
            {
                index = lstPcbAirPlane.FindIndex(pcb => !pcb.Visible);
                lstPcbAirPlane[index].Tag = id;
            }

            lstPcbAirPlane[index].Visible = true;
            lstPcbAirPlane[index].Location = new Point(xplot, yplot);

            return lstPcbAirPlane[index];
        }

        public void InitialisePcb(Control.ControlCollection controls)
        {
            lstPcbAirPlane = new List<PictureBox>(50);

            for (int i = 0; i < 50; i++)
            {

                lstPcbAirPlane.Add(new PictureBox()
                {
                    Image = Image.FromFile("../../assets/airplane.png"),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(16, 16),
                    Location = new Point(0, 0),
                    Visible = false
                });

                controls.Add(lstPcbAirPlane[i]);
            }
        }

        public void ClearOldPositions(List<AirPlaneClientModel> lstApc)
        {
            foreach (PictureBox pcb in lstPcbAirPlane)
            {
                int index = lstApc.FindIndex(apc => apc.Id == (string)pcb.Tag);

                if (index == -1)
                {
                    pcb.Visible = false;
                    pcb.Tag = "";
                }
            }
        }
    }
}