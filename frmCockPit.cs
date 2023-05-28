using AlbyAirLines.Controllers.cockpit;
using System;
using System.Net;
using System.Windows.Forms;

namespace AlbyAirLines
{
    public partial class frmCockPit : Form
    {
        CockPit CockPit;
        int mult = 1;

        public frmCockPit()
        {
            InitializeComponent();
        }

        private void frmCockPit_Load(object sender, System.EventArgs e)
        {
            CockPit = new CockPit();
            CockPit.Error += ErrorHandler;
            CockPit.StopFlight += StopFlight;

            cmbStart.DataSource = CockPit.GetAirports();
        }

        private void pcbStart_Click(object sender, System.EventArgs e)
        {
            string ipString = txtIp.Text.Trim();

            IPAddress ip;

            if (IPAddress.TryParse(ipString, out ip))
            {
                CockPit.Start(ip, cmbStart.Text);

                tmrSend.Interval = 100;

                tmrSend.Start();

                tmrUpdate.Start();
            }
            else
                MessageBox.Show("Ip errato");
        }

        private void tmrSend_Tick(object sender, System.EventArgs e)
        {
            CockPit.SendPosition();
        }

        private void tmrUpdate_Tick(object sender, System.EventArgs e)
        {
            double delta = 0;

            if (trbDelta.Value != trbDelta.Maximum)
                if (trbDelta.Value < 10)
                    delta = Convert.ToDouble($"0,0{trbDelta.Value}");
                else
                    delta = Convert.ToDouble($"0,{trbDelta.Value}");
            else
                delta = 1;

            Direction direction = Direction.right;

            if (chkDown.Checked)
                direction = Direction.down;
            else if (chkUp.Checked)
                direction = Direction.up;
            else if (chkLeft.Checked)
                direction = Direction.left;
            else if (chkRight.Checked)
                direction = Direction.right;

            CockPit.CalculateNextPosition(direction, delta * mult);
        }

        private void StopFlight()
        {
            tmrSend.Stop();
            tmrUpdate.Stop();

            MessageBox.Show("Errore durante l'invio");
        }

        private void ErrorHandler(string message)
        {
            MessageBox.Show(message);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            mult = mult == 10 ? 1 : 10;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tmrSend.Stop();
            tmrUpdate.Stop();

            CockPit.Stop();
        }
    }
}
