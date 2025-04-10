using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace MediTakipApp.Forms
{
    public partial class DoctorPanel : Form
    {
        string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";
        int doctorId = 1; // ← Gerçek projede giriş yapan doktorun ID'si atanacak


        public DoctorPanel()
        {
            InitializeComponent();
        }

        private void DoctorPanel_Load_1(object sender, EventArgs e)
        {
            LoadPatients();
            dgvPatients.Columns["Id"].Visible = false;
        }

        private void LoadPatients()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Id, FirstName, LastName, TcNo, Insurance, BirthDate, Gender, City, District FROM Patients WHERE DoctorId = @docId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@docId", doctorId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPatients.DataSource = dt;
            }
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            PatientForm form = new PatientForm();
            form.DoctorId = doctorId;
            form.IsUpdateMode = false;
            form.FormClosed += (s, args) => LoadPatients();
            form.ShowDialog();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek bir hasta seç!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçilen satırdan hasta ID'sini al
            int selectedPatientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["Id"].Value);

            DialogResult result = MessageBox.Show("Bu hastayı silmek istediğine emin misin?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "DELETE FROM Patients WHERE Id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", selectedPatientId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Hasta başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPatients(); // Listeyi yenile
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek bir hasta seçin.");
                return;
            }

            DataGridViewRow row = dgvPatients.SelectedRows[0];
            PatientForm form = new PatientForm();
            form.IsUpdateMode = true;
            form.PatientId = Convert.ToInt32(row.Cells["Id"].Value);
            form.FirstName = row.Cells["FirstName"].Value.ToString();
            form.LastName = row.Cells["LastName"].Value.ToString();
            form.TcNo = row.Cells["TcNo"].Value.ToString();
            form.BirthDate = Convert.ToDateTime(row.Cells["BirthDate"].Value);
            form.Gender = row.Cells["Gender"].Value.ToString();
            form.Insurance = row.Cells["Insurance"].Value.ToString();
            form.City = row.Cells["City"].Value.ToString();
            form.District = row.Cells["District"].Value.ToString();
            form.FormClosed += (s, args) => LoadPatients();
            form.ShowDialog();
        }

        private void btnPrescriptions_Click(object sender, EventArgs e)
        {
            PrescriptionForm form = new PrescriptionForm();
            form.DoctorId = doctorId; // giriş yapan doktorun ID'si
            form.ShowDialog(); // modal olarak aç
            LoadPatients(); // reçete sonrası hasta verisini yenilemek istersen
        }

    }
}
