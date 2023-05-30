using AlbyAirLines.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AlbyAirLines
{
    public partial class frmClient : Form
    {
        private Client client;
        private AirPlaneClientModel apc;
        private Thread threadSend;

        private double delta;

        private Dictionary<char, double> types_delta = new Dictionary<char, double>
        {
            { 'L', 0.15},
            { 'M', 0.25 },
            { 'G', 0.10 },
            { 'E', 0.08 }
        };

        Dictionary<string, char> names_types = new Dictionary<string, char>
        {
            { "boeing 747", 'L' },
            { "f-16", 'M' },
            { "elicottero", 'E' },
            { "ultraleggero", 'G' }
        };

        public frmClient()
        {
            InitializeComponent();

            client = new Client("127.0.0.1", 5000, Application.StartupPath);
            client.UpdateProgress += UpdateProgress;
            client.StopTravelling += StopTravelling;
            client.Error += Error;
        }

        private void UpdateProgress(int progress)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (pgbAndamento != null)
                        pgbAndamento.Value = progress;
                }));
            }
            catch { }
        }

        private void btnManda_Click(object sender, EventArgs e)
        {
            btnManda.Enabled = false;
            btnRoute.Enabled = false;

            try
            {
                threadSend = client.SendPositionSequence(1000, apc, types_delta[apc.Type]);
            }
            catch (Exception ex)
            {
                if (ex.Message == "400")
                    MessageBox.Show("Prima devi generare una rotta");
                else
                    MessageBox.Show($"Errore generico, codice: {ex.Message}");
            }
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            int from, to;

            DataTable airports = client.GetAirports();

            if (airports != null)
            {
                Random rnd = new Random();

                pgbAndamento.Value = 0;

                var rnd_name_type = names_types.ElementAt(rnd.Next(0, names_types.Count));

                delta = types_delta[rnd_name_type.Value];

                do
                {
                    from = rnd.Next(0, airports.Rows.Count);
                    to = rnd.Next(0, airports.Rows.Count);
                } while (from == to);

                apc = new AirPlaneClientModel(
                    Convert.ToDouble(airports.Rows[from][2]),
                    Convert.ToDouble(airports.Rows[from][3]),
                    rnd_name_type.Key,
                    rnd_name_type.Value,
                    client.RequestId()
                )
                {
                    ToLong = Convert.ToDouble(airports.Rows[to][2]),
                    ToLat = Convert.ToDouble(airports.Rows[to][3]),
                    FromLat = Convert.ToDouble(airports.Rows[from][3]),
                    FromLong = Convert.ToDouble(airports.Rows[from][2])
                };

                var routeInformation = new Dictionary<string, string>
                {
                    { "Aereo", apc.Name },
                    { "From", airports.Rows[from][1].ToString() },
                    { "To", airports.Rows[to][1].ToString() }
                };

                var lstDgv = new List<KeyValuePair<string, string>>();

                lstDgv.AddRange(routeInformation);

                dgvRoute.DataSource = lstDgv;
                dgvRoute.AutoResizeColumns();

                btnManda.Enabled = true;
            }
        }

        private void StopTravelling()
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (btnManda != null && btnRoute != null)
                    {
                        btnManda.Enabled = true;
                        btnRoute.Enabled = true;
                    }
                }));
            }
            catch { }
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadSend != null)
                threadSend.Abort();
        }

        private void Error(string message)
        {
            MessageBox.Show(message);
        }
    }
}
