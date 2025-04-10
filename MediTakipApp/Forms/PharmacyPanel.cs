using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms
{
    public partial class PharmacyPanel : Form
    {
        private string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public PharmacyPanel()
        {
            InitializeComponent();
        }

        private void PharmacyPanel_Load(object sender, EventArgs e)
        {
            LoadPrescriptions();
        }

        private void LoadPrescriptions()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
                    SELECT P.Id AS PrescriptionId,
                           U.Username AS Doctor,
                           (Pa.FirstName + ' ' + Pa.LastName) AS Patient,
                           P.DateCreated
                    FROM Prescriptions P
                    JOIN Patients Pa ON P.PatientId = Pa.Id
                    JOIN Users U ON P.DoctorId = U.Id
                    ORDER BY P.DateCreated DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPrescriptions.DataSource = dt;
            }
        }

        private void dgvPrescriptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int prescriptionId = Convert.ToInt32(dgvPrescriptions.Rows[e.RowIndex].Cells["PrescriptionId"].Value);
                LoadPrescriptionDetails(prescriptionId);
            }
        }

        private void LoadPrescriptionDetails(int prescriptionId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
                    SELECT D.Name AS DrugName,
                           D.ActiveIngredient,
                           PD.Quantity,
                           PD.UsageInstructions
                    FROM PrescriptionDrugs PD
                    JOIN Drugs D ON PD.DrugId = D.Id
                    WHERE PD.PrescriptionId = @pid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pid", prescriptionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDrugsInPrescription.DataSource = dt;
            }
        }
    }
}