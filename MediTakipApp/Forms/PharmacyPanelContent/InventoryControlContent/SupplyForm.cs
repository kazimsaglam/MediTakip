using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class SupplyForm : Form
    {
        string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";

        private int drugId;
        private string drugName;

        public SupplyForm(int drugId, string drugName)
        {
            this.drugId = drugId;
            this.drugName = drugName;
            InitializeComponent();
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Name FROM Suppliers WHERE IsActive = 1 ORDER BY Name", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            cmbSupplier.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                cmbSupplier.Items.Add(row["Name"].ToString());
            }

            if (cmbSupplier.Items.Count > 0)
                cmbSupplier.SelectedIndex = 0;
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            int quantity = (int)nudQuantity.Value;
            DateTime expiry = dtpExpiry.Value;
            string supplier = cmbSupplier.SelectedItem?.ToString() ?? "";

            if (quantity <= 0 || string.IsNullOrWhiteSpace(supplier))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO DrugStocks (DrugId, StockQuantity, ExpirationDate, EntryDate, Supplier)
                                                  VALUES (@drugId, @quantity, @expiry, @entryDate, @supplier)", conn);
                cmd.Parameters.AddWithValue("@drugId", drugId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@expiry", expiry);
                cmd.Parameters.AddWithValue("@entryDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@supplier", supplier);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Stok başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
