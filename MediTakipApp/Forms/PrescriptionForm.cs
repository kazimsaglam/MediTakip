using System.Data;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace MediTakipApp.Forms
{
    public partial class PrescriptionForm : Form
    {
        private string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PatientId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PatientName { get; set; }

        private List<PrescribedDrug> selectedDrugs = new List<PrescribedDrug>();

        public PrescriptionForm()
        {
            InitializeComponent();
        }

        private void PrescriptionForm_Load(object sender, EventArgs e)
        {
            lblPatient.Text = $"Hasta: {PatientName}";
            dtpDate.Value = DateTime.Now;
            dtpDate.Visible = false;
            LoadDrugList();
        }

        private void LoadDrugList()
        {
            using var conn = new SqlConnection(connStr);
            conn.Open();
            var da = new SqlDataAdapter("SELECT Id, Barcode, Name, ActiveIngredient, UsageAge, Price FROM Drugs", conn);
            var dt = new DataTable();
            da.Fill(dt);

            dgvDrugs.DataSource = dt;

            if (!dgvDrugs.Columns.Contains("Ekle"))
            {
                var btnCol = new DataGridViewButtonColumn();
                btnCol.Name = "Ekle";
                btnCol.HeaderText = "";
                btnCol.Text = "Ekle";
                btnCol.UseColumnTextForButtonValue = true;
                btnCol.Width = 120;
                dgvDrugs.Columns.Add(btnCol);
            }
        }

        private void dgvDrugs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDrugs.Columns["Ekle"].Index && e.RowIndex >= 0)
            {
                var row = dgvDrugs.Rows[e.RowIndex];

                int drugId = Convert.ToInt32(row.Cells["Id"].Value);
                string name = row.Cells["Name"].Value.ToString();
                string dosage = row.Cells["ActiveIngredient"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                selectedDrugs.Add(new PrescribedDrug
                {
                    DrugId = drugId,
                    DrugName = name,
                    Instructions = "Günde 2 kez",
                    Quantity = 1,
                    Price = price
                });

                RefreshSelectedList();
            }
        }

        private void RefreshSelectedList()
        {
            dgvSelected.DataSource = null;
            dgvSelected.DataSource = null;
            dgvSelected.Columns.Clear();

            dgvSelected.DataSource = selectedDrugs;

            if (!dgvSelected.Columns.Contains("Sil"))
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.Name = "Sil";
                btnCol.HeaderText = "";
                btnCol.Text = "Sil";
                btnCol.UseColumnTextForButtonValue = true;
                btnCol.Width = 120;
                dgvSelected.Columns.Add(btnCol);
            }
        }


        private void dgvSelected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSelected.Columns["Sil"].Index)
            {
                selectedDrugs.RemoveAt(e.RowIndex);
                RefreshSelectedList();
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            using var conn = new SqlConnection(connStr);
            conn.Open();

            var cmd = new SqlCommand("INSERT INTO Prescriptions (DoctorId, PatientId, DateCreated) OUTPUT INSERTED.Id VALUES (@doc, @pat, @date)", conn);
            cmd.Parameters.AddWithValue("@doc", 1); // Gerçek sistemde giriş yapan doktorun ID'si gelir
            cmd.Parameters.AddWithValue("@pat", PatientId);
            cmd.Parameters.AddWithValue("@date", dtpDate.Value);
            int presId = (int)cmd.ExecuteScalar();

            foreach (var drug in selectedDrugs)
            {
                var drugCmd = new SqlCommand("INSERT INTO PrescriptionDrugs (PrescriptionId, DrugId, Quantity, UsageInstructions) VALUES (@pid, @did, @qty, @ins)", conn);
                drugCmd.Parameters.AddWithValue("@pid", presId);
                drugCmd.Parameters.AddWithValue("@did", drug.DrugId);
                drugCmd.Parameters.AddWithValue("@qty", drug.Quantity);
                drugCmd.Parameters.AddWithValue("@ins", drug.Instructions);
                drugCmd.ExecuteNonQuery();
            }

            MessageBox.Show("Reçete başarıyla kaydedildi!");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class PrescribedDrug
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public decimal Price { get; set; }
    }
}
