using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class SupplyForm : Form
    {
        private string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";
        private int selectedDrugId = 0;
        private bool isNewDrug = false;
        private System.Windows.Forms.Timer pollingTimer;
        private bool autoFilled = false;

        public SupplyForm()
        {
            InitializeComponent();
            LoadSuppliers();
        }

        private async void SupplyForm_Load(object sender, EventArgs e)
        {
            await DeleteAllBarcodes(connStr);
            StartPollingForMobileInput();
            SetDrugFieldsEnabled(false);
        }

        private async void SupplyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pollingTimer?.Stop();
            pollingTimer?.Dispose();
            await DeleteAllBarcodes(connStr);
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
            pollingTimer.Interval = 1000;
            pollingTimer.Tick += async (s, e) =>
            {
                if (autoFilled) return;

                try
                {
                    string query = "SELECT TOP 1 Id, Barcode FROM MobileBarcodeRead ORDER BY Id DESC";

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        await conn.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!await reader.ReadAsync()) return;

                            string barcode = reader.GetString(1).Trim();
                            if (string.IsNullOrEmpty(barcode)) return;

                            await DeleteAllBarcodes(connStr);
                            DataRow drug = GetDrugByBarcode(barcode);

                            if (drug == null)
                            {
                                isNewDrug = true;
                                EnableNewDrugEntry(barcode);
                                autoFilled = true;
                                pollingTimer.Stop();
                                MessageBox.Show($"Yeni ilaç kaydı başlatıldı. Barkod: {barcode}", "Yeni Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            isNewDrug = false;
                            selectedDrugId = Convert.ToInt32(drug["Id"]);

                            txtBarcode.Text = drug["Barcode"].ToString();
                            txtName.Text = drug["Name"].ToString();
                            txtIngredient.Text = drug["ActiveIngredient"].ToString();
                            txtUsageAge.Text = drug["UsageAge"].ToString();
                            chkPrescription.Checked = Convert.ToBoolean(drug["IsPrescription"]);
                            txtPrice.Text = Convert.ToDecimal(drug["Price"]).ToString("C2");

                            SetDrugFieldsEnabled(false);
                            autoFilled = true;
                            pollingTimer.Stop();

                            MessageBox.Show($"İlaç bilgisi yüklendi: {drug["Name"]}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL polling hatası: " + ex.Message);
                }
            };

            pollingTimer.Start();
        }

        private void SetDrugFieldsEnabled(bool enabled)
        {
            txtBarcode.ReadOnly = !enabled;
            txtName.ReadOnly = !enabled;
            txtIngredient.ReadOnly = !enabled;
            txtUsageAge.ReadOnly = !enabled;
            txtPrice.ReadOnly = !enabled;
            chkPrescription.Enabled = enabled;
        }

        private void EnableNewDrugEntry(string barcode)
        {
            txtBarcode.Text = barcode;
            txtName.Text = "";
            txtIngredient.Text = "";
            txtUsageAge.Text = "";
            txtPrice.Text = "";
            chkPrescription.Checked = false;
            SetDrugFieldsEnabled(true);
        }

        private async Task DeleteAllBarcodes(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    await conn.OpenAsync();
                    string deleteQuery = "DELETE FROM MobileBarcodeRead";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Tablo temizleme hatası: " + ex.Message);
            }
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

        private bool ValidateNewDrugInputs(out int usageAge, out decimal price)
        {
            usageAge = 0;
            price = 0;

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtBarcode.Text) ||
                string.IsNullOrWhiteSpace(txtIngredient.Text) ||
                string.IsNullOrWhiteSpace(txtUsageAge.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Lütfen tüm ilaç bilgilerini doldurunuz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtUsageAge.Text, out usageAge) || usageAge < 0)
            {
                MessageBox.Show("Lütfen geçerli bir kullanım yaşı giriniz!", "Hatalı Veri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out price) || price <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Hatalı Fiyat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (isNewDrug)
            {
                if (!ValidateNewDrugInputs(out int usageAge, out decimal parsedPrice))
                    return;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmdInsert = new SqlCommand(@"
                        INSERT INTO Drugs (Name, Barcode, ActiveIngredient, UsageAge, IsPrescription, Price)
                        VALUES (@name, @barcode, @ingredient, @age, @rx, @price);
                        SELECT SCOPE_IDENTITY();", conn);

                    cmdInsert.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@barcode", txtBarcode.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@ingredient", txtIngredient.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@age", usageAge);
                    cmdInsert.Parameters.AddWithValue("@rx", chkPrescription.Checked);
                    cmdInsert.Parameters.AddWithValue("@price", parsedPrice);

                    selectedDrugId = Convert.ToInt32(cmdInsert.ExecuteScalar());
                }
            }

            if (selectedDrugId == 0)
            {
                MessageBox.Show("İlaç seçilmedi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudQuantity.Value <= 0 || string.IsNullOrWhiteSpace(cmbSupplier.Text))
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
                cmd.Parameters.AddWithValue("@qty", (int)nudQuantity.Value);
                cmd.Parameters.AddWithValue("@expiry", dtpExpiry.Value);
                cmd.Parameters.AddWithValue("@supplier", cmbSupplier.Text.Trim());

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

