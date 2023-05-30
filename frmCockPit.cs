using AlbyAirLines.Controllers.cockpit;
using System;
using System.Net;
using System.Windows.Forms;

namespace AlbyAirLines
{
    public partial class frmCockPit : Form
    {
        CockPit CockPit;

        string lastKey = "D";
        int mult = 1;

        public frmCockPit()
        {
            InitializeComponent();

            CockPit = new CockPit(Application.StartupPath);
            CockPit.Error += ErrorHandler;
            CockPit.StopFlight += StopFlight;
        }

        private void frmCockPit_Load(object sender, System.EventArgs e)
        {
            cmbStart.DataSource = CockPit.GetAirports();
        }

        private void pcbStart_Click(object sender, System.EventArgs e)
        {
            if (!tmrSend.Enabled && !tmrUpdate.Enabled)
            {
                string ipString = txtIp.Text.Trim();

                IPAddress ip;

                if (IPAddress.TryParse(ipString, out ip))
                {
                    if (CockPit.Start(ip, cmbStart.Text))
                    {
                        tmrSend.Interval = 100;

                        tmrSend.Start();

                        tmrUpdate.Start();
                    }
                }
                else
                    MessageBox.Show("Ip errato");
            }
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

            if (lastKey == "S")
                direction = Direction.down;
            else if (lastKey == "W")
                direction = Direction.up;
            else if (lastKey == "A")
                direction = Direction.left;
            else if (lastKey == "D")
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

        private void pcbRocket_Click(object sender, EventArgs e)
        {
            mult = mult == 10 ? 1 : 10;
        }

        private void pcbStop_Click(object sender, EventArgs e)
        {
            tmrSend.Stop();
            tmrUpdate.Stop();

            CockPit.Stop();
        }

        private void txtDirection_KeyPress(object sender, KeyPressEventArgs e)
        {
            lastKey = e.KeyChar.ToString().ToUpper();
            e.Handled = true;
        }

        private void frmCockPit_FormClosing(object sender, FormClosingEventArgs e)
        {
            CockPit.Stop();
        }
    }
}
