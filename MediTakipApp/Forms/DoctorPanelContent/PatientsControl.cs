using System.Data;
using Microsoft.Data.SqlClient;
using Timer = System.Windows.Forms.Timer;

namespace MediTakipApp.Forms
{
    public partial class PatientsControl : UserControl
    {
        string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";
        private DataTable allPatients = new DataTable();
        private Panel selectedPatientCard = null;
        private Label currentToast = null;


        public PatientsControl()
        {
            InitializeComponent();
        }

        private void PatientsControl_Load(object sender, EventArgs e)
        {
            LoadPatients();
        }


        private void SelectPatientCard(Panel card)
        {
            // Önce önceki seçimi sıfırla
            if (selectedPatientCard != null)
            {
                selectedPatientCard.BackColor = Color.White;
                selectedPatientCard.Width = 250;
                selectedPatientCard.Height = 120;
            }

            // Şimdi yeni kartı seç
            selectedPatientCard = card;
            card.BackColor = Color.LightBlue;

            // 🔥 Seçilen hastanın bilgilerini static class'a kaydet
            if (card.Tag != null && card.Tag is DataRow row)
            {
                SelectedPatient.Id = Convert.ToInt32(row["Id"]);
                SelectedPatient.FirstName = row["FirstName"].ToString();
                SelectedPatient.LastName = row["LastName"].ToString();
                SelectedPatient.TcNo = row["TcNo"].ToString();
                SelectedPatient.Insurance = row["Insurance"].ToString();
                SelectedPatient.BirthDate = Convert.ToDateTime(row["BirthDate"]);
                SelectedPatient.Gender = row["Gender"].ToString();
                SelectedPatient.City = row["City"].ToString();
                SelectedPatient.District = row["District"].ToString();
                SelectedPatient.Phone = row["Phone"].ToString();
            }

            ShowToast($"{SelectedPatient.FullName} seçildi!");

            // 🔥 Hasta detayları kartlara doldur
            LoadPatientDetails();
        }



        private void LoadPatients()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Patients", conn);
                allPatients.Clear();
                da.Fill(allPatients);

                flpPatients.Controls.Clear();

                foreach (DataRow row in allPatients.Rows)
                {
                    Panel patientCard = new Panel
                    {
                        Width = 250,
                        Height = 120,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(10),
                        BackColor = Color.White,
                        Tag = row
                    };

                    Label lblName = new Label
                    {
                        Text = row["FirstName"] + " " + row["LastName"],
                        Dock = DockStyle.Top,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    Label lblInsurance = new Label
                    {
                        Text = $"Sigorta: {row["Insurance"]}",
                        Dock = DockStyle.Top,
                        Font = new Font("Segoe UI", 9),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    Label lblPhone = new Label
                    {
                        Text = $"Telefon: {row["Phone"]}",
                        Dock = DockStyle.Bottom,
                        Font = new Font("Segoe UI", 9),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    patientCard.Controls.Add(lblPhone);
                    patientCard.Controls.Add(lblInsurance);
                    patientCard.Controls.Add(lblName);

                    flpPatients.Controls.Add(patientCard);

                    patientCard.MouseEnter += (s, e) =>
                    {
                        patientCard.BackColor = Color.LightGray;
                        patientCard.Width += 10;
                        patientCard.Height += 5;
                    };

                    patientCard.MouseLeave += (s, e) =>
                    {
                        if (selectedPatientCard != patientCard)
                            patientCard.BackColor = Color.White;
                        patientCard.Width = 250;
                        patientCard.Height = 120;
                    };

                    patientCard.Click += (s, e) =>
                    {
                        SelectPatientCard(patientCard);
                    };
                }
            }
        }

        private Panel CreatePatientCard(DataRow row)
        {
            Panel card = new Panel()
            {
                Width = 250,
                Height = 120,
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = row
            };

            string fullName = row["FirstName"] + " " + row["LastName"];
            Label name = new Label() { Text = fullName, Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true };
            Label tc = new Label() { Text = "TC: " + row["TcNo"], Location = new Point(10, 35), AutoSize = true };
            Label city = new Label() { Text = row["City"] + ", " + row["District"], Location = new Point(10, 55), AutoSize = true };

            card.Controls.Add(name);
            card.Controls.Add(tc);
            card.Controls.Add(city);

            card.Click += (s, e) => SelectPatientCard(card);

            return card;
        }

        private void LoadPatientDetails()
        {
            flowCards.Controls.Clear();

            if (SelectedPatient.Id == 0)
            {
                flowCards.Controls.Add(CreatePatientInfoCard("⚠️", "Hasta seçilmedi"));
                return;
            }

            flowCards.Controls.Add(CreatePatientInfoCard("👤", $"{SelectedPatient.FirstName} {SelectedPatient.LastName}"));
            flowCards.Controls.Add(CreatePatientInfoCard("🆔", SelectedPatient.TcNo));
            flowCards.Controls.Add(CreatePatientInfoCard("💊", SelectedPatient.Insurance));
            flowCards.Controls.Add(CreatePatientInfoCard("🎂", SelectedPatient.BirthDate.ToShortDateString()));
            flowCards.Controls.Add(CreatePatientInfoCard("🚻", SelectedPatient.Gender));
            flowCards.Controls.Add(CreatePatientInfoCard("🏙️", SelectedPatient.City));
            flowCards.Controls.Add(CreatePatientInfoCard("🏘️", SelectedPatient.District));
            flowCards.Controls.Add(CreatePatientInfoCard("☎️", SelectedPatient.Phone));
        }

        private Panel CreatePatientInfoCard(string icon, string text)
        {
            Panel card = new Panel
            {
                Width = 280,
                Height = 80,
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            // Border-radius efekti için region
            //card.Region = System.Drawing.Region.FromHrgn(
            //    NativeMethods.CreateRoundRectRgn(0, 0, card.Width, card.Height, 10, 10)
            //);

            Label lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 20, FontStyle.Regular),
                Location = new Point(15, 15),
                AutoSize = true
            };

            Label lblText = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(80, 25),
                AutoSize = true
            };

            card.Controls.Add(lblIcon);
            card.Controls.Add(lblText);

            return card;
        }




        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            flpPatients.Controls.Clear();
            foreach (DataRow row in allPatients.Rows)
            {
                string fullName = (row["FirstName"] + " " + row["LastName"]).ToLower();
                if (fullName.Contains(keyword))
                {
                    Panel card = CreatePatientCard(row);
                    flpPatients.Controls.Add(card);
                }
            }
        }

        private void BtnAddPatient_Click(object sender, EventArgs e)
        {
            PatientForm form = new PatientForm();
            form.DoctorId = 1;
            form.ShowDialog();
            LoadPatients();
        }

        private void BtnUpdatePatient_Click(object sender, EventArgs e)
        {
            if (selectedPatientCard == null || selectedPatientCard.Tag == null)
            {
                MessageBox.Show("Lütfen önce bir hasta seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = selectedPatientCard.Tag as DataRow;

            PatientForm form = new PatientForm();
            form.IsUpdateMode = true;
            form.PatientId = Convert.ToInt32(row["Id"]);
            form.FirstName = row["FirstName"].ToString();
            form.LastName = row["LastName"].ToString();
            form.TcNo = row["TcNo"].ToString();
            form.Insurance = row["Insurance"].ToString();
            form.BirthDate = Convert.ToDateTime(row["BirthDate"]);
            form.Gender = row["Gender"].ToString();
            form.City = row["City"].ToString();
            form.District = row["District"].ToString();
            form.Phone = row["Phone"].ToString();
            form.DoctorId = 1;

            form.ShowDialog();
            LoadPatients();
        }

        private void BtnDeletePatient_Click(object sender, EventArgs e)
        {
            if (selectedPatientCard == null || selectedPatientCard.Tag == null)
            {
                MessageBox.Show("Lütfen önce bir hasta seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = selectedPatientCard.Tag as DataRow;
            int id = Convert.ToInt32(row["Id"]);

            DialogResult result = MessageBox.Show("Bu hastayı silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Patients WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadPatients();
            }
        }

        private void ShowToast(string message)
        {
            // 🔥 Önce eski tostu sil  
            if (currentToast != null && !currentToast.IsDisposed)
            {
                currentToast.Dispose();
            }

            Label lblToast = new Label
            {
                Text = message,
                AutoSize = true,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                Padding = new Padding(10),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Form? parentForm = FindForm();
            if (parentForm != null)
            {
                lblToast.Location = new Point((parentForm.Width - lblToast.Width) / 2, 30);
                parentForm.Controls.Add(lblToast);
                lblToast.BringToFront();

                currentToast = lblToast; // 🔥 Şu anda ekranda olan toast  

                Timer timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (s, e) =>
                {
                    if (lblToast != null && !lblToast.IsDisposed)
                    {
                        lblToast.Dispose();
                    }
                    timer.Stop();
                };
                timer.Start();
            }
        }
    }
}
