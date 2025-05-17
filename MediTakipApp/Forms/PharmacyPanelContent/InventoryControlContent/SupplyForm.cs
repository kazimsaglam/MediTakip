using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class SupplyForm : Form
    {
        private string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";
        private int selectedDrugId = 0;
        private System.Windows.Forms.Timer pollingTimer;
        private bool autoFilled = false;

        public SupplyForm()
        {
            InitializeComponent();
            LoadSuppliers();
            StartPollingForMobileInput();
        }

        private void LoadSuppliers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Supplier FROM DrugStocks ORDER BY Supplier", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmbSupplier.Items.Add(reader.GetString(0));
                }
            }
        }

        private void StartPollingForMobileInput()
        {
            pollingTimer = new System.Windows.Forms.Timer();
            pollingTimer.Interval = 2000;
            pollingTimer.Tick += async (s, e) =>
            {
                if (autoFilled) return;

                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync("drug/supply");
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            if (!string.IsNullOrWhiteSpace(json) && json != "null")
                            {
                                dynamic data = JsonConvert.DeserializeObject(json);
                                string barcode = data.barcode.ToString();

                                DataRow drug = GetDrugByBarcode(barcode);
                                if (drug == null)
                                {
                                    var result = MessageBox.Show(
                                        $"Bu barkod sistemde kayıtlı değil.\nYeni ilaç olarak eklensin mi?\n\nBarkod: {barcode}",
                                        "İlaç Bulunamadı",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        MessageBox.Show("Yeni ilaç bilgileri formdan manuel girilmeli.");
                                        return;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Tedarik işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        pollingTimer.Stop();
                                        return;
                                    }
                                }

                                selectedDrugId = Convert.ToInt32(drug["Id"]);
                                string drugName = drug["Name"].ToString();
                                lblDrugName.Text = "İlaç: " + drugName;

                                autoFilled = true;
                                pollingTimer.Stop();

                                MessageBox.Show($"📦 Barkod okundu: {barcode}\nİlaç: {drugName}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Polling hatası: " + ex.Message);
                }
            };

            pollingTimer.Start();
        }

        private DataRow GetDrugByBarcode(string barcode)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Drugs WHERE Barcode = @barcode", conn);
                cmd.Parameters.AddWithValue("@barcode", barcode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (selectedDrugId == 0)
            {
                MessageBox.Show("İlaç seçilmedi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantity = (int)nudQuantity.Value;
            DateTime expiry = dtpExpiry.Value;
            string supplier = cmbSupplier.Text.Trim();

            if (quantity <= 0 || string.IsNullOrWhiteSpace(supplier))
            {
                MessageBox.Show("Miktar ve tedarikçi alanları zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO DrugStocks (DrugId, StockQuantity, ExpirationDate, EntryDate, Supplier)
                    VALUES (@drugId, @qty, @expiry, GETDATE(), @supplier)", conn);

                cmd.Parameters.AddWithValue("@drugId", selectedDrugId);
                cmd.Parameters.AddWithValue("@qty", quantity);
                cmd.Parameters.AddWithValue("@expiry", expiry);
                cmd.Parameters.AddWithValue("@supplier", supplier);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Tedarik başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

