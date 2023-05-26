using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using AlbyAirLines.Controllers;

namespace AlbyAirLines
{
    public partial class frmClient : Form
    {
        private Client client;
        private AirPlaneClientModel apc;

        public frmClient()
        {
            InitializeComponent();
        }
        
        private void frmClient_Load(object sender, EventArgs e)
        {
            client = new Client("127.0.0.1", 5000);
            client.UpdateProgress += UpdateProgress;
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
                client.SendPositionSequence(100, apc, StopTravelling);
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

            string[] names = {"boeing 747", "f-16", "elicottero", "ultraleggero" };

            do
            {
                from = rnd.Next(0, airports.Rows.Count);
                to = rnd.Next(0, airports.Rows.Count);
            } while (from == to);

            apc = new AirPlaneClientModel(
                    Convert.ToDouble(airports.Rows[from][2]),
                    Convert.ToDouble(airports.Rows[from][3]),
                    names[rnd.Next(0, names.Length)],
                    'L',
                    client.RequestId()
            )
            {
                ToLong = Convert.ToDouble(airports.Rows[to][2]),
                ToLat = Convert.ToDouble(airports.Rows[to][3]),
                FromLat = Convert.ToDouble(airports.Rows[from][3]),
                FromLong = Convert.ToDouble(airports.Rows[from][2])
            };

            Dictionary<string, string> routeInformation = new Dictionary<string, string>();
            
            routeInformation.Add("Aereo", apc.Name);
            routeInformation.Add("From", airports.Rows[from][1].ToString());
            routeInformation.Add("To", airports.Rows[to][1].ToString());

            List <KeyValuePair<string, string>> lstDgv = new List<KeyValuePair<string, string>>();

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
