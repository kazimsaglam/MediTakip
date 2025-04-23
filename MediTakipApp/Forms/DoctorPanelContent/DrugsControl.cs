using System.Data;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.DoctorPanelContent
{
    public partial class DrugsControl : UserControl
    {
        private string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";
        private List<PrescribedDrug> selectedDrugs = new List<PrescribedDrug>();
        private DataTable allDrugs = new DataTable();
        private int selectedPatientId => SelectedPatient.Id;

        public DrugsControl(int patientId = 0, string diagnosis = "")
        {
            InitializeComponent();
            txtDiagnosis.Text = diagnosis;
        }

        private void DrugsControl_Load(object sender, EventArgs e)
        {
            if (SelectedPatient.Id == 0)
            {
                lblPatientInfo.Text = "👤 Seçilen Hasta: Yok";
                return; // hasta seçilmemişse liste bile gösterme
            }
            else
            {
                lblPatientInfo.Text = $"Seçilen Hasta: {SelectedPatient.FullName} ({GetAge(SelectedPatient.BirthDate)} yaşında)";
            }      

            LoadDrugs();
        }

        private void LoadDrugs()
        {
            flpDrugs.Controls.Clear();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Drugs", conn);
                allDrugs.Clear();
                da.Fill(allDrugs);

                foreach (DataRow row in allDrugs.Rows)
                {
                    Panel card = CreateDrugCard(row);
                    flpDrugs.Controls.Add(card);
                }
            }
        }

        private Panel CreateDrugCard(DataRow row)
        {
            Panel card = new Panel
            {
                Width = 300,
                Height = 140,
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = row,
                Cursor = Cursors.Hand
            };

            card.MouseEnter += (s, e) => { card.BackColor = Color.LightGray; };
            card.MouseLeave += (s, e) => { card.BackColor = Color.White; };
            card.Click += (s, e) => OnDrugCardClick(row);

            card.Controls.Add(new Label { Text = row["Name"].ToString(), Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true });
            card.Controls.Add(new Label { Text = $"Etken Madde: {row["ActiveIngredient"]}", Location = new Point(10, 35), AutoSize = true });
            card.Controls.Add(new Label { Text = $"Stok: {row["StockQuantity"]} adet", Location = new Point(10, 55), AutoSize = true });
            card.Controls.Add(new Label { Text = $"Fiyat: {Convert.ToDecimal(row["Price"]):C2}", Location = new Point(10, 75), AutoSize = true });
            card.Controls.Add(new Label { Text = Convert.ToBoolean(row["IsPrescription"]) ? "Reçeteli" : "Reçetesiz", ForeColor = Convert.ToBoolean(row["IsPrescription"]) ? Color.Red : Color.DarkGreen, Location = new Point(10, 95), AutoSize = true });

            return card;
        }

        private void OnDrugCardClick(DataRow row)
        {
            int drugId = Convert.ToInt32(row["Id"]);
            var existing = selectedDrugs.FirstOrDefault(d => d.DrugId == drugId);

            if (existing != null)
            {
                if (MessageBox.Show($"{row["Name"]} zaten reçetede var. Adet artırılsın mı?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    existing.Quantity++;
                }
            }
            else
            {
                selectedDrugs.Add(new PrescribedDrug
                {
                    DrugId = drugId,
                    DrugName = row["Name"].ToString(),
                    Quantity = 1,
                    Instructions = "Günde 2 kez",
                    Price = Convert.ToDecimal(row["Price"])
                });
            }
            RefreshSelectedDrugList();
        }

        private void RefreshSelectedDrugList()
        {
            flpSelectedDrugs.Controls.Clear();
            foreach (var drug in selectedDrugs)
            {
                Panel row = new Panel { Height = 40, Width = flpSelectedDrugs.Width - 25, BackColor = Color.WhiteSmoke, Margin = new Padding(5), Tag = drug };

                row.Controls.Add(new Label { Text = $"{drug.DrugName} x{drug.Quantity} ({drug.Instructions})", AutoSize = true, Location = new Point(10, 10) });

                Button btnRemove = new Button { Text = "-", Size = new Size(30, 30), Location = new Point(row.Width - 40, 5), BackColor = Color.IndianRed, ForeColor = Color.White };
                btnRemove.Click += (s, e) => {
                    if (drug.Quantity > 1) drug.Quantity--;
                    else selectedDrugs.Remove(drug);
                    RefreshSelectedDrugList();
                };
                row.Controls.Add(btnRemove);

                flpSelectedDrugs.Controls.Add(row);
            }
        }

        private void BtnSavePrescription_Click(object sender, EventArgs e)
        {
            if (selectedDrugs.Count == 0 || selectedPatientId == 0 || string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Lütfen hasta, teşhis ve ilaç bilgilerini eksiksiz girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Prescriptions (PatientId, DoctorId, Diagnosis, DateCreated) OUTPUT INSERTED.Id VALUES (@pId, @docId, @diag, @date)", conn);

                cmd.Parameters.AddWithValue("@pId", selectedPatientId);
                cmd.Parameters.AddWithValue("@docId", LoggedUser.Id); // 🔥 Giriş yapan doktorun ID'si
                cmd.Parameters.AddWithValue("@diag", txtDiagnosis.Text.Trim());
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                int prescriptionId = (int)cmd.ExecuteScalar();

                foreach (var drug in selectedDrugs)
                {
                    SqlCommand detailCmd = new SqlCommand("INSERT INTO PrescriptionDetails (PrescriptionId, DrugId, Quantity, Instructions) VALUES (@presId, @drugId, @qty, @inst)", conn);
                    detailCmd.Parameters.AddWithValue("@presId", prescriptionId);
                    detailCmd.Parameters.AddWithValue("@drugId", drug.DrugId);
                    detailCmd.Parameters.AddWithValue("@qty", drug.Quantity);
                    detailCmd.Parameters.AddWithValue("@inst", drug.Instructions);
                    detailCmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Reçete başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            selectedDrugs.Clear();
            txtDiagnosis.Clear();
            RefreshSelectedDrugList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            flpDrugs.Controls.Clear();
            foreach (DataRow row in allDrugs.Rows)
            {
                if (row["Name"].ToString().ToLower().Contains(keyword))
                {
                    flpDrugs.Controls.Add(CreateDrugCard(row));
                }
            }
        }

        private int GetAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear) age--;
            return age;
        }
    }

    public class PrescribedDrug
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; } = 1;
        public string Instructions { get; set; } = "Günde 2 kez";
        public decimal Price { get; set; }
    }
}