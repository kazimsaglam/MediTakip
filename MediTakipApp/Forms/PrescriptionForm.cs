using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms
{
    public partial class PrescriptionForm : Form
    {
        private string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DoctorId { get; set; }

        private List<PrescribedDrug> prescriptionList = new List<PrescribedDrug>();

        public PrescriptionForm()
        {
            InitializeComponent();
        }

        private void PrescriptionForm_Load_1(object sender, EventArgs e)
        {
            LoadPatients();
            LoadDrugs();
        }

        private void LoadPatients()
        {
            cmbPatients.Items.Clear();
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, FirstName + ' ' + LastName AS FullName FROM Patients WHERE DoctorId = @docId", conn);
                cmd.Parameters.AddWithValue("@docId", DoctorId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbPatients.Items.Add(new ComboBoxItem(reader["FullName"].ToString(), (int)reader["Id"]));
                }
            }
        }

        private void LoadDrugs()
        {
            cmbDrugs.Items.Clear();
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Name FROM Drugs", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbDrugs.Items.Add(new ComboBoxItem(reader["Name"].ToString(), (int)reader["Id"]));
                }
            }
        }

        private void btnAddDrug_Click(object sender, EventArgs e)
        {
            if (cmbDrugs.SelectedItem == null) return;
            var selectedDrug = (ComboBoxItem)cmbDrugs.SelectedItem;
            if (int.TryParse(txtQuantity.Text.Trim(), out int quantity))
            {
                prescriptionList.Add(new PrescribedDrug
                {
                    DrugId = selectedDrug.Value,
                    DrugName = selectedDrug.Text,
                    Quantity = quantity,
                    Instructions = txtInstructions.Text.Trim()
                });

                dgvPrescription.DataSource = null;
                dgvPrescription.DataSource = prescriptionList;
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir adet girin.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedItem == null || prescriptionList.Count == 0)
            {
                MessageBox.Show("Lütfen hasta seçin ve en az bir ilaç ekleyin.");
                return;
            }

            var selectedPatient = (ComboBoxItem)cmbPatients.SelectedItem;
            int patientId = selectedPatient.Value;

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Reçete oluştur
                var cmd = new SqlCommand("INSERT INTO Prescriptions (DoctorId, PatientId) OUTPUT INSERTED.Id VALUES (@docId, @patId)", conn);
                cmd.Parameters.AddWithValue("@docId", DoctorId);
                cmd.Parameters.AddWithValue("@patId", patientId);
                int prescriptionId = (int)cmd.ExecuteScalar();

                // İlaçları kaydet
                foreach (var drug in prescriptionList)
                {
                    var drugCmd = new SqlCommand("INSERT INTO PrescriptionDrugs (PrescriptionId, DrugId, Quantity, UsageInstructions) VALUES (@presId, @drugId, @qty, @instr)", conn);
                    drugCmd.Parameters.AddWithValue("@presId", prescriptionId);
                    drugCmd.Parameters.AddWithValue("@drugId", drug.DrugId);
                    drugCmd.Parameters.AddWithValue("@qty", drug.Quantity);
                    drugCmd.Parameters.AddWithValue("@instr", drug.Instructions);
                    drugCmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Reçete başarıyla kaydedildi.");
            this.Close();
        }
    }

    public class PrescribedDrug
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}