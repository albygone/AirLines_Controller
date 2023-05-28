using AlbyAirLines.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AlbyAirLines
{
    public partial class frmClient : Form
    {
        private Client client;
        private AirPlaneClientModel apc;
        private double delta = 0;

        public frmClient()
        {
            InitializeComponent();
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            client = new Client("127.0.0.1", 5000);
            client.UpdateProgress += UpdateProgress;
            client.StopTravelling += StopTravelling;
        }

        private void UpdateProgress(int progress)
        {
            Invoke(new Action(() => { pgbAndamento.Value = progress; }));
        }

        private void btnManda_Click(object sender, EventArgs e)
        {
            btnManda.Enabled = false;
            btnRoute.Enabled = false;

            try
            {
                double delta = 0.25;

                switch (apc.Type)
                {
                    default:
                        break;
                }

                client.SendPositionSequence(1000, apc, delta);
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
            Random rnd = new Random();

            Dictionary<string, char> names_types = new Dictionary<string, char>
            {
                { "boeing 747", 'L' },
                { "f-16", 'M' },
                { "elicottero", 'G' },
                { "ultraleggero", 'E' }
            };

            Dictionary<char, double> types_delta = new Dictionary<char, double>
            {
                { 'L', 0.15},
                { 'M', 0.25 },
                { 'G', 0.10 },
                { 'E', 0.8 }
            };

            KeyValuePair<string, char> rnd_name_type = names_types.ElementAt(rnd.Next(0, names_types.Count));
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

            Dictionary<string, string> routeInformation = new Dictionary<string, string>
            {
                { "Aereo", apc.Name },
                { "From", airports.Rows[from][1].ToString() },
                { "To", airports.Rows[to][1].ToString() }
            };

            List<KeyValuePair<string, string>> lstDgv = new List<KeyValuePair<string, string>>();

            lstDgv.AddRange(routeInformation);

            dgvRoute.DataSource = lstDgv;
            dgvRoute.AutoResizeColumns();
        }

        private void StopTravelling()
        {
            Invoke(new Action(() => btnManda.Enabled = true));
            Invoke(new Action(() => btnRoute.Enabled = true));
        }
    }
}
