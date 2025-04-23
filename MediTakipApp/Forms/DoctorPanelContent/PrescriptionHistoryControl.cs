using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.DoctorPanelContent
{
    public partial class PrescriptionHistoryControl : UserControl
    {
        string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public PrescriptionHistoryControl()
        {
            InitializeComponent();
        }

        private void PrescriptionHistoryControl_Load(object sender, EventArgs e)
        {
            LoadPrescriptions();
        }

        private void LoadPrescriptions()
        {
            flpPrescriptions.Controls.Clear();

            if (SelectedPatient.Id == 0)
            {
                lblSelectedPatient.Text = "👤 Seçilen Hasta: Yok";
                return; // hasta seçilmemişse liste bile gösterme
            }
            else
            {
                lblSelectedPatient.Text = $"👤 Seçilen Hasta: {SelectedPatient.FirstName} {SelectedPatient.LastName}";
            }


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Prescriptions WHERE PatientId = @id ORDER BY DateCreated DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", SelectedPatient.Id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Panel card = new Panel
                    {
                        Width = 250,
                        Height = 100,
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Tag = row
                    };

                    card.Controls.Add(new Label
                    {
                        Text = $"📅 {Convert.ToDateTime(row["DateCreated"]).ToShortDateString()}",
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Location = new Point(10, 10),
                        AutoSize = true
                    });

                    card.Controls.Add(new Label
                    {
                        Text = $"📝 {row["Diagnosis"].ToString()}",
                        Font = new Font("Segoe UI", 9),
                        Location = new Point(10, 35),
                        AutoSize = true
                    });

                    card.Click += (s, e) =>
                    {
                        LoadPrescriptionDetails((int)row["Id"]);
                    };

                    card.MouseEnter += (s, e) =>
                    {
                        card.BackColor = Color.LightGray;
                        card.Width += 10;
                        card.Height += 5;
                    };

                    card.MouseLeave += (s, e) =>
                    {
                        card.BackColor = Color.White;
                        card.Width = 250;
                        card.Height = 100;
                    };

                    flpPrescriptions.Controls.Add(card);
                }
            }
        }

        private void LoadPrescriptionDetails(int prescriptionId)
        {
            flpDetails.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"SELECT d.Name, pd.Quantity, pd.Instructions
                                 FROM PrescriptionDetails pd
                                 JOIN Drugs d ON d.Id = pd.DrugId
                                 WHERE pd.PrescriptionId = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", prescriptionId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Panel row = new Panel
                    {
                        Height = 40,
                        Width = flpDetails.Width - 30,
                        BackColor = Color.WhiteSmoke,
                        Margin = new Padding(5)
                    };

                    row.Controls.Add(new Label
                    {
                        Text = $"{reader["Name"]} x{reader["Quantity"]} ({reader["Instructions"]})",
                        AutoSize = true,
                        Location = new Point(10, 10)
                    });

                    flpDetails.Controls.Add(row);
                }
            }
        }
    }
}
