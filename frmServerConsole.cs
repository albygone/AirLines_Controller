using AlbyAirLines.Controllers;
using System;
using System.Timers;
using System.Windows.Forms;

namespace AlbyAirLines
{
    public partial class frmServerConsole : Form
    {
        private Server server = new Server();
        public frmServerConsole()
        {
            InitializeComponent();
        }

        private void frmServerConsole_Load(object sender, EventArgs e)
        {
            tmrFetch.Interval = 1500;
            tmrFetch.Start();
            server.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            frmClient drm = new frmClient();
            drm.Show();
        }

        private void tmrFetch_Elapsed(object sender, ElapsedEventArgs e)
        {
            dgvProva.DataSource = server.GetLivePositions();
        }

        private void btnOpenControlRoom_Click(object sender, EventArgs e)
        {
            frmControlRoom frm = new frmControlRoom();
            frm.Show();
        }


        private void btnClearLive_Click(object sender, EventArgs e)
        {
            server.ClearLivePositions();
        }

        private void btnOpenCockpit_Click(object sender, EventArgs e)
        {
            frmCockPit frmCP = new frmCockPit();
            frmCP.Show();
        }
    }
}
