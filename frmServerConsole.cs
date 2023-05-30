using AlbyAirLines.Controllers;
using System;
using System.Data;
using System.Timers;
using System.Windows.Forms;

namespace AlbyAirLines
{
    public partial class frmServerConsole : Form
    {
        private Server server;

        public frmServerConsole()
        {
            InitializeComponent();

            server = new Server(Application.StartupPath);
            server.Error += ErrorHandler;
        }

        private void frmServerConsole_Load(object sender, EventArgs e)
        {
            try
            {
                tmrFetch.Interval = 1500;

                tmrFetch.Start();

                server.Start();
            }
            catch (Exception ex)
            {
                ErrorHandler(ex.Message);
            }
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            frmClient drm = new frmClient();
            drm.Show();
        }

        private void tmrFetch_Elapsed(object sender, ElapsedEventArgs e)
        {
            DataTable dt = server.GetLivePositions();

            if (dt != null)
                dgvProva.DataSource = dt;
            else
            {
                tmrFetch.Stop();
                btnClearLive.Enabled = false;
                btnApriClient.Enabled = false;
            }
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

        private void frmServerConsole_Closing(object sender, EventArgs e)
        {
            server.Close();
        }

        private void ErrorHandler(string message)
        {
            MessageBox.Show(message);
        }
    }
}
