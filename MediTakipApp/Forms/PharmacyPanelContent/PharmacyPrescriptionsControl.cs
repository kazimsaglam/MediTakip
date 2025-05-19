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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextFont = iTextSharp.text.Font;


namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class PharmacyPrescriptionsControl : UserControl
    {
        string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";

        public PharmacyPrescriptionsControl()
        {
            InitializeComponent();
            this.dgvPrescriptions.CellClick += dgvPrescriptions_CellClick;
        }




        // 🔹 1. İLAÇ TABLOSUNUN KOLONLARINI AYARLAR
        private void SetupPrescriptionDetailsGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("DrugName", "İlaç Adı");
            dataGridView1.Columns.Add("Dosage", "Doz");
            dataGridView1.Columns.Add("Quantity", "Adet");
            dataGridView1.Columns.Add("UsageInstructions", "Kullanım Bilgisi");


            DataGridViewButtonColumn btnDeliver = new DataGridViewButtonColumn();
            btnDeliver.HeaderText = "Teslim";
            btnDeliver.Text = "Teslim Et";
            btnDeliver.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnDeliver);

           
            dataGridView1.AllowUserToAddRows = false;
        }

        private void SetupPaymentGrid()
        {
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add("DrugName", "İlaç Adı");
            dataGridView2.Columns.Add("Price", "Fiyat (₺)");
            dataGridView2.Columns.Add("Insurance", "Sigorta");
            dataGridView2.Columns.Add("PatientPays", "Ödenecek Tutar (₺)");

            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.HeaderText = "İşlem";
            deleteBtn.Text = "❌";
            deleteBtn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(deleteBtn);

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

           
            dataGridView2.AllowUserToAddRows = false;
        }





        private void LoadPrescriptions(string tcFilter, string codeFilter)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Prescriptions WHERE 1=1";

                if (!string.IsNullOrEmpty(tcFilter))
                    query += " AND PatientId IN (SELECT Id FROM Patients WHERE TcNo = @tc)";

                if (!string.IsNullOrEmpty(codeFilter))
                    query += " AND PrescriptionCode = @code";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(tcFilter))
                        cmd.Parameters.AddWithValue("@tc", tcFilter);

                    if (!string.IsNullOrEmpty(codeFilter))
                        cmd.Parameters.AddWithValue("@code", codeFilter);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPrescriptions.DataSource = dt;


                    
                    if (dgvPrescriptions.Columns.Contains("PrescriptionId"))
                        dgvPrescriptions.Columns["PrescriptionId"].HeaderText = "Reçete No";

                    if (dgvPrescriptions.Columns.Contains("PrescriptionCode"))
                        dgvPrescriptions.Columns["PrescriptionCode"].HeaderText = "Kod";

                    if (dgvPrescriptions.Columns.Contains("PatientId"))
                        dgvPrescriptions.Columns["PatientId"].HeaderText = "Hasta";

                    if (dgvPrescriptions.Columns.Contains("DoctorId"))
                        dgvPrescriptions.Columns["DoctorId"].HeaderText = "Doktor";

                    if (dgvPrescriptions.Columns.Contains("Diagnosis"))
                        dgvPrescriptions.Columns["Diagnosis"].HeaderText = "Tanı";

                    if (dgvPrescriptions.Columns.Contains("PrescriptionDate"))
                        dgvPrescriptions.Columns["PrescriptionDate"].HeaderText = "Tarih";
                }
            }
        }


        // 🔹 2. REÇETELERİ TC VE KODLA GETİRİR      
        private void LoadPrescriptionDetails(int prescriptionId)
        {
            SetupPrescriptionDetailsGrid();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT D.Name AS DrugName, PD.Dosage, PD.Quantity, PD.UsagePeriod, PD.SpecialInstructions
            FROM PrescriptionDetails PD
            JOIN Drugs D ON PD.DrugId = D.Id
            WHERE PD.PrescriptionId = @prescriptionId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@prescriptionId", prescriptionId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                            reader["DrugName"].ToString(),
                            reader["Dosage"].ToString(),
                            reader["Quantity"].ToString(),
                            reader["UsagePeriod"] + " - " + reader["SpecialInstructions"]
);
                        }
                    }
                }
            }
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string tc = txtSearch.Text.Trim();
            string code = txtPrescriptionCode.Text.Trim();

            
            if (string.IsNullOrWhiteSpace(tc) && string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Lütfen en az TC kimlik numarası veya Reçete Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(tc) && tc.Length != 11)
            {
                MessageBox.Show("TC kimlik numarası 11 haneli olmalıdır.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            LoadPrescriptions(tc, code);
        }


        private void PharmacyPrescriptionsControl_Load(object sender, EventArgs e)
        {
            LoadPrescriptions("", "");
            Button btnFatura = new Button
            {
                Text = "Fiş Yazdır",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Size = new Size(100, 35),
                Location = new Point(btnPay.Right + 10, btnPay.Top),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnFatura.Click += btnFatura_Click;

            this.Controls.Add(btnFatura);

            UpdatePayButtonText(); 
        }

        private void btnFatura_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Fişe eklenecek ilaç bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string invoiceNumber = "FTR-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"IlacFis_{invoiceNumber}.pdf");

            Document doc = new Document();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                string fontPath = Path.Combine(Application.StartupPath, "Resources", "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextFont headerFont = new iTextFont(baseFont, 16, iTextFont.BOLD);
                iTextFont cellFont = new iTextFont(baseFont, 12);

                Paragraph header = new Paragraph("Reçeteli İlaç Satış Faturası", headerFont);
                header.Alignment = Element.ALIGN_CENTER;
                doc.Add(header);
                doc.Add(new Paragraph(" ")); // boşluk

                PdfPTable table = new PdfPTable(dataGridView2.Columns.Count - 1); // ❌ buton hariç
                table.WidthPercentage = 100;

                // Başlıklar
                for (int i = 0; i < dataGridView2.Columns.Count - 1; i++)
                {
                    table.AddCell(new Phrase(dataGridView2.Columns[i].HeaderText, cellFont));
                }

                // Satırlar
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.IsNewRow) continue;
                    for (int i = 0; i < dataGridView2.Columns.Count - 1; i++)
                    {
                        table.AddCell(new Phrase(row.Cells[i].Value?.ToString(), cellFont));
                    }
                }

                doc.Add(table);

                doc.Add(new Paragraph("\nTarih: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"), cellFont));
                doc.Add(new Paragraph("Fatura No: " + invoiceNumber, cellFont));
                doc.Add(new Paragraph(" "));

                MessageBox.Show("Fatura PDF başarıyla oluşturuldu:\n" + path, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF oluşturulurken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
        }




        private void txtPrescriptionCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvPrescriptions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int prescriptionId = Convert.ToInt32(dgvPrescriptions.Rows[e.RowIndex].Cells["PrescriptionId"].Value);
                LoadPrescriptionDetails(prescriptionId);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                
                if (dataGridView2.Columns.Count == 0)
                    SetupPaymentGrid();

                string drugName = dataGridView1.Rows[e.RowIndex].Cells["DrugName"].Value?.ToString();
                string tcNo = txtSearch.Text.Trim();
                int prescriptionId = GetSelectedPrescriptionId();

                decimal price = GetDrugPrice(drugName);
                string insurance = GetPatientInsurance(tcNo);
                decimal patientPays = CalculateCopayment(price, insurance);

                
                bool alreadyExists = false;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["DrugName"].Value?.ToString() == drugName)
                    {
                        alreadyExists = true;
                        break;
                    }
                }
                if (alreadyExists) return;

                dataGridView2.Rows.Add(drugName, price.ToString("0.00"), insurance, patientPays.ToString("0.00"));
                UpdatePayButtonText();

                MessageBox.Show(
                  $"İlaç: {drugName}\nFiyat: {price.ToString("0.00")} ₺\nSigorta Türü: {insurance}\nHastanın Ödeyeceği: {patientPays.ToString("0.00")} ₺",
                  "Ödeme Bilgisi",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);

                
            }
        }





        private void dgvPrescriptionDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPrescriptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }





        private decimal CalculateCopayment(decimal price, string insurance)
        {
            switch (insurance.ToLower())
            {
                case "sgk":
                    return price * 0.2m; // %20
                case "bağkur":
                    return price * 0.3m;
                case "özel":
                    return price * 0.5m;
                default:
                    return price; // sigortasız
            }
        }

        private int GetSelectedPrescriptionId()
        {
            if (dgvPrescriptions.CurrentRow != null)
            {
                return Convert.ToInt32(dgvPrescriptions.CurrentRow.Cells["PrescriptionId"].Value);
            }
            return -1;
        }

        private decimal GetDrugPrice(string drugName)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Price FROM Drugs WHERE Name = @name";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", drugName);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        private string GetPatientInsurance(string tcNo)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Insurance FROM Patients WHERE TcNo = @tc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tc", tcNo);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "bilinmiyor";
                }
            }
        }

        private decimal CalculatePatientShare(decimal price, string insurance)
        {
            return CalculateCopayment(price, insurance);
        }

        public void ReduceStock(int drugId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                while (quantity > 0)
                {
                    SqlCommand cmd = new SqlCommand(@"
                     SELECT TOP 1 StockId, StockQuantity 
                     FROM DrugStocks 
                     WHERE DrugId = @drugId AND StockQuantity > 0 
    ORDER BY ExpirationDate ASC", conn);
                    cmd.Parameters.AddWithValue("@drugId", drugId);

                        var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int partId = reader.GetInt32(0);          // StockId
                        int available = reader.GetInt32(1);       // StockQuantity
                        reader.Close();

                        int used = Math.Min(available, quantity);
                        SqlCommand updateCmd = new SqlCommand(
                            "UPDATE DrugStocks SET StockQuantity = StockQuantity - @used WHERE StockId = @partId", conn);
                        updateCmd.Parameters.AddWithValue("@used", used);
                        updateCmd.Parameters.AddWithValue("@partId", partId);
                        updateCmd.ExecuteNonQuery();

                        quantity -= used;
                    }

                    else
                    {
                        reader.Close();
                        MessageBox.Show("Yeterli stok yok!", "Stok Hatası");
                        break;
                    }
                }
            }
        }

        public int GetDrugIdByName(string drugName)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Drugs WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", drugName);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        private void ShowPaymentDetails(int prescriptionId)
        {
            SetupPaymentGrid();
            dataGridView2.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string patientQuery = @"
                SELECT P.Insurance
                FROM Prescriptions PR
                JOIN Patients P ON PR.PatientId = P.Id
                WHERE PR.PrescriptionId = @prescriptionId";


                string insurance = "bilinmiyor";
                using (SqlCommand cmd = new SqlCommand(patientQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@prescriptionId", prescriptionId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        insurance = reader["Insurance"].ToString();
                    }
                    reader.Close();
                }

                string drugQuery = @"
                SELECT D.Name, D.Price
                FROM PrescriptionDetails PD
                JOIN Drugs D ON PD.DrugId = D.Id
                WHERE PD.PrescriptionId = @prescriptionId";

                using (SqlCommand cmd = new SqlCommand(drugQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@prescriptionId", prescriptionId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string drugName = reader["Name"].ToString();
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        decimal patientPays = CalculateCopayment(price, insurance);

                        dataGridView2.Rows.Add(drugName, price.ToString("0.00"), insurance, patientPays.ToString("0.00"));
                    }
                }
            }
        }
          

        private void UpdatePayButtonText()
        {
            decimal toplam = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["PatientPays"].Value != null &&
                    decimal.TryParse(row.Cells["PatientPays"].Value.ToString(), out decimal val))
                {
                    toplam += val;
                }
            }

            btnPay.Text = toplam > 0
                ? $"Toplam: {toplam.ToString("0.00")} TL - Öde"
                : "Öde";
        }

        private void SomeAction()
        {
            UpdatePayButtonText(); 
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                string drugName = row.Cells["DrugName"].Value.ToString();
                int quantity = 1; 
                int drugId = GetDrugIdByName(drugName);

                ReduceStock(drugId, quantity);
            }

            MessageBox.Show("Ödeme alındı. Geçmiş olsun!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
            for (int i = dataGridView2.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView2.Rows.RemoveAt(i);
            }

            UpdatePayButtonText();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
