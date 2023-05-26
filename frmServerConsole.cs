using System;
using System.Data;
using System.Timers;
using System.Windows.Forms;
using AlbyAirLines.Controllers;
using AlbyLib;

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
            tmrFetch.Interval = 500;
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
    }
}
