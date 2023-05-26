using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AlbyLib.controlRoom;

namespace AlbyAirLines
{
    public partial class frmControlRoom : Form
    {
        ControlRoom controlRoom = new ControlRoom();

        public frmControlRoom()
        {
            InitializeComponent();
            controlRoom.InitialisePcb(Controls);
        }

        private void frmControlRoom_Load(object sender, EventArgs e)
        {
            tmrFetch.Interval = 90;
            
            tmrFetch.Start();
        }
        
        private void tmrFetch_Tick(object sender, EventArgs e)
        {
            PlotPositions();
        }

        private void PlotPositions()
        {
            DataTable dt = controlRoom.GetLivePositions();

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

                PictureBox pcb = controlRoom.PlotPosition(lstApc[i], pcbMap, lstApc[i].Id);
                pcb.BringToFront();

                i++;
            }

            controlRoom.ClearOldPositions(lstApc);
        }
    }
}