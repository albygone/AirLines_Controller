using AlbyLib.controlRoom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AlbyAirLines
{
    public partial class frmControlRoom : Form
    {
        ControlRoom controlRoom;

        bool readyToFetch = false;

        PictureBox pcbCockPit = new PictureBox()
        {
            Image = Image.FromFile("../../assets/airplaneCockPit.png"),
            SizeMode = PictureBoxSizeMode.Zoom,
            Size = new Size(16, 16),
            Location = new Point(0, 0),
            Visible = true,
            Name = "pcbCockPit"
        };

        public frmControlRoom()
        {
            InitializeComponent();

            Controls.Add(pcbCockPit);

            controlRoom = new ControlRoom(pcbMap, Application.StartupPath);
            controlRoom.Error += ErrorHandler;

            bool dbConnected = controlRoom.GetLivePositions() != null;


            if (dbConnected)
            {
                controlRoom.InitialisePcb(Controls);
                tmrFetch.Start();
            }
        }

        private void frmControlRoom_Load(object sender, EventArgs e)
        {
            tmrFetch.Interval = 500;

            if (readyToFetch)
                tmrFetch.Start();

            tmrUpdateCockPit.Start();
        }

        private void tmrFetch_Tick(object sender, EventArgs e)
        {
            PlotPositions();
        }

        private void PlotPositions()
        {
            DataTable dt = controlRoom.GetLivePositions();

            if (dt != null)
            {
                List<AirPlaneClientModel> lstApc = new List<AirPlaneClientModel>();

                int i = 0;

                foreach (DataRow dtRow in dt.Rows)
                {
                    lstApc.Add(new AirPlaneClientModel(
                        Convert.ToDouble(dtRow[1]),
                        Convert.ToDouble(dtRow[2]),
                        dtRow[3].ToString(),
                        dtRow[4].ToString().ToCharArray()[0],
                        dtRow[0].ToString()));

                    PictureBox pcb = controlRoom.PlotPosition(lstApc[i], lstApc[i].Id);
                    pcb.BringToFront();

                    i++;
                }

                controlRoom.ClearOldPositions(lstApc);
            }
            else
                tmrFetch.Stop();
        }

        private void tmrUpdateCockPit_Tick(object sender, EventArgs e)
        {
            (int x, int y) = controlRoom.cockPitPosition;

            pcbCockPit.Location = new Point(x, y);
            pcbCockPit.BringToFront();
        }

        private void ErrorHandler(string message)
        {
            MessageBox.Show(message);
        }
    }
}