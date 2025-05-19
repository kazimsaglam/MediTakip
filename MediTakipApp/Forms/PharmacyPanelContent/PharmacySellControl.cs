using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharpText = iTextSharp.text;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class PharmacySellControl : UserControl
    {
        private string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";
        private TextBox txtSearch;
        private Button btnSearch;
        private DataTable allDrugsTable = new DataTable();
        private Panel searchPanel;
        public PharmacySellControl()
        {
            InitializeComponent();
            Load += PharmacySellControl_Load;
        }

        private void PharmacySellControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            Button btnExportPdf = new Button
            {
                Text = "Fatura Yazdır",
                Font = new Font("Bahnschrift", 12, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(btnPay.Left, btnPay.Bottom + 10),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExportPdf.Click += BtnExportPdf_Click;
            this.Controls.Add(btnExportPdf);

            // 🔍 Arama paneli
            searchPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 10, 10)
            };

            txtSearch = new TextBox
            {
                PlaceholderText = "İlaç adı giriniz...",
                Font = new System.Drawing.Font("Segoe UI", 12),
                Width = 250,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
                Location = new Point(20, 10)
            };

            btnSearch = new Button
            {
                Text = "Ara",
                Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(70, 32),
                Location = new Point(260, 10),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            Button btnClear = new Button
            {
                Text = "Temizle",
                Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(80, 32),
                Location = new Point(340, 10), // Sağda dursun
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClear.Click += BtnClear_Click;

            btnSearch.Click += BtnSearch_Click;

            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(btnClear);
            this.Controls.Add(searchPanel);

            flpCards.Dock = DockStyle.Fill;
            flpCards.AutoScroll = true;
            flpCards.WrapContents = true;
            flpCards.FlowDirection = FlowDirection.LeftToRight;
            flpCards.BackColor = Color.White;
            flpCards.Margin = new Padding(0);
            flpCards.MaximumSize = new Size(1320, 0);
            flpCards.Padding = new Padding(10, 60, 10, 10);

            this.Controls.Add(flpCards); // panelden sonra eklenmeli

            // 🧾 Ödeme tablosu
            dgvPayment.BackgroundColor = Color.White;
            dgvPayment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayment.AllowUserToAddRows = false;
            dgvPayment.RowHeadersVisible = false;
            dgvPayment.Columns.Clear();

            dgvPayment.Columns.Add(new DataGridViewTextBoxColumn { Name = "DrugName", HeaderText = "İlaç Adı" });
            dgvPayment.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Fiyat (₺)" });
            dgvPayment.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Adet" });
            dgvPayment.Columns.Add(new DataGridViewTextBoxColumn { Name = "PatientPays", HeaderText = "Ödenecek Tutar (₺)" });
            dgvPayment.Columns.Add(new DataGridViewButtonColumn { Name = "İşlem", HeaderText = "İşlem", Text = "❌", UseColumnTextForButtonValue = true });

            btnPay.Click += BtnPay_Click;

            LoadNonPrescriptionDrugs();
        }

        private void LoadNonPrescriptionDrugs()
        {
            flpCards.Controls.Clear();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
                SELECT D.Id, D.Name, D.Barcode, D.ActiveIngredient, D.UsageAge, D.Price,
                       ISNULL(SUM(S.StockQuantity), 0) AS TotalStock,
                       MIN(CASE WHEN S.StockQuantity > 0 THEN S.ExpirationDate ELSE NULL END) AS EarliestExpiry
                FROM Drugs D
                LEFT JOIN DrugStocks S ON D.Id = S.DrugId
                WHERE D.IsPrescription = 0
                GROUP BY D.Id, D.Name, D.Barcode, D.ActiveIngredient, D.UsageAge, D.Price";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                allDrugsTable.Clear();
                allDrugsTable = dt.Copy();
                //DisplayDrugCards(allDrugsTable);

                foreach (DataRow row in dt.Rows)
                {
                    Panel card = new Panel
                    {
                        Width = 295,
                        Height = 260,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(5)
                    };

                    string name = row["Name"].ToString();
                    string barcode = row["Barcode"].ToString();
                    string ingredient = row["ActiveIngredient"].ToString();
                    int usageAge = Convert.ToInt32(row["UsageAge"]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    int stock = Convert.ToInt32(row["TotalStock"]);
                    DateTime? expiry = row["EarliestExpiry"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["EarliestExpiry"]);

                    int y = 10;
                    card.Controls.Add(new Label { Text = $"💊 {name}", Font = new System.Drawing.Font("Bahnschrift", 13, FontStyle.Bold), Location = new Point(10, y), AutoSize = true }); y += 25;
                    card.Controls.Add(new Label { Text = $"🔬 {ingredient}", Location = new Point(10, y), AutoSize = true }); y += 20;
                    card.Controls.Add(new Label { Text = $"🏷️ Barkod: {barcode}", Location = new Point(10, y), AutoSize = true }); y += 20;
                    card.Controls.Add(new Label { Text = $"👶 Yaş: {usageAge}+", Location = new Point(10, y), AutoSize = true }); y += 20;
                    card.Controls.Add(new Label { Text = $"💰 Fiyat: {price:C2}", Location = new Point(10, y), AutoSize = true }); y += 20;
                    card.Controls.Add(new Label { Text = $"📦 Stok: {stock}", Location = new Point(10, y), AutoSize = true }); y += 20;
                    card.Controls.Add(new Label { Text = $"⏳ SKT: {(expiry.HasValue ? expiry.Value.ToShortDateString() : "-")}", Location = new Point(10, y), AutoSize = true }); y += 20;
                    card.Controls.Add(new Label { Text = "🆓 Reçetesiz", ForeColor = Color.ForestGreen, Location = new Point(10, y), AutoSize = true }); y += 25;

                    // Closure bug fix için değişkenleri kopyala
                    string localName = name;
                    decimal localPrice = price;

                    Button btnAdd = new Button
                    {
                        Text = "➕ Sepete Ekle",
                        Size = new Size(270, 40),
                        Location = new Point(10, 210),
                        BackColor = Color.SeaGreen,
                        ForeColor = Color.White,
                        Font = new System.Drawing.Font("Bahnschrift", 11, FontStyle.Bold)
                    };
                    btnAdd.Click += (s, e) => AddToCart(name, price); // 👈 direkt ekle
                    card.Controls.Add(btnAdd);

                    flpCards.Controls.Add(card);
                }
            }
        }

        private void AddToCart(string drugName, decimal price)
        {
            decimal patientPays = price;

            foreach (DataGridViewRow row in dgvPayment.Rows)
            {
                if (row.Cells["DrugName"].Value?.ToString() == drugName)
                {
                    int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    currentQty++;
                    row.Cells["Quantity"].Value = currentQty;

                    row.Cells["PatientPays"].Value = (currentQty * price).ToString("0.00");

                    UpdatePayButtonText();
                    return;
                }
            }

            int rowIndex = dgvPayment.Rows.Add();
            dgvPayment.Rows[rowIndex].Cells["DrugName"].Value = drugName;
            dgvPayment.Rows[rowIndex].Cells["Price"].Value = price.ToString("0.00");
            dgvPayment.Rows[rowIndex].Cells["Quantity"].Value = 1;
            dgvPayment.Rows[rowIndex].Cells["PatientPays"].Value = price.ToString("0.00");
            dgvPayment.Rows[rowIndex].Cells["İşlem"].Value = "❌";

            UpdatePayButtonText();
        }

        private void dgvPayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPayment.Columns["İşlem"].Index && e.RowIndex >= 0)
            {
                dgvPayment.Rows.RemoveAt(e.RowIndex);
                UpdatePayButtonText();
            }
        }


        private void DisplayDrugCards(DataTable table)
        {
            flpCards.Controls.Clear();

            foreach (DataRow row in table.Rows)
            {
                string name = row["Name"].ToString();
                string barcode = row["Barcode"].ToString();
                string ingredient = row["ActiveIngredient"].ToString();
                int usageAge = Convert.ToInt32(row["UsageAge"]);
                decimal price = Convert.ToDecimal(row["Price"]);
                int stock = Convert.ToInt32(row["TotalStock"]);
                DateTime? expiry = row["EarliestExpiry"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["EarliestExpiry"]);

                Panel card = new Panel
                {
                    Width = 280,
                    Height = 260,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10),
                    Padding = new Padding(12),
                    MaximumSize = new Size(280, 260)
                };

                int y = 10;
                card.Controls.Add(new Label { Text = $"💊 {name}", Font = new System.Drawing.Font("Bahnschrift", 13, FontStyle.Bold), Location = new Point(10, y), AutoSize = true }); y += 25;
                card.Controls.Add(new Label { Text = $"🔬 {ingredient}", Location = new Point(10, y), AutoSize = true }); y += 20;
                card.Controls.Add(new Label { Text = $"🏷️ Barkod: {barcode}", Location = new Point(10, y), AutoSize = true }); y += 20;
                card.Controls.Add(new Label { Text = $"👶 Yaş: {usageAge}+", Location = new Point(10, y), AutoSize = true }); y += 20;
                card.Controls.Add(new Label { Text = $"💰 Fiyat: {price:C2}", Location = new Point(10, y), AutoSize = true }); y += 20;
                card.Controls.Add(new Label { Text = $"📦 Stok: {stock}", Location = new Point(10, y), AutoSize = true }); y += 20;
                card.Controls.Add(new Label { Text = $"⏳ SKT: {(expiry.HasValue ? expiry.Value.ToShortDateString() : "-")}", Location = new Point(10, y), AutoSize = true }); y += 20;
                card.Controls.Add(new Label { Text = "🆓 Reçetesiz", ForeColor = Color.ForestGreen, Location = new Point(10, y), AutoSize = true }); y += 25;

                string localName = name;
                decimal localPrice = price;

                Button btnAdd = new Button
                {
                    Text = "➕ Sepete Ekle",
                    Size = new Size(270, 40),
                    Location = new Point(10, 210),
                    BackColor = Color.SeaGreen,
                    ForeColor = Color.White,
                    Font = new Font("Bahnschrift", 11, FontStyle.Bold)
                };
                btnAdd.Click += (s, e) => AddToCart(localName, localPrice);
                card.Controls.Add(btnAdd);

                flpCards.Controls.Add(card);
            }
        }

        private void UpdatePayButtonText()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvPayment.Rows)
            {
                if (decimal.TryParse(row.Cells["PatientPays"].Value?.ToString(), out decimal val))
                    total += val;
            }
            btnPay.Text = total > 0 ? $"Toplam: {total:0.00} TL - Öde" : "Öde";
        }


        private void BtnPay_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                foreach (DataGridViewRow row in dgvPayment.Rows)
                {
                    string drugName = row.Cells["DrugName"].Value.ToString();
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                    // 🟩 İlaç ID’sini veritabanından çek
                    int drugId = -1;
                    using (SqlCommand getIdCmd = new SqlCommand("SELECT Id FROM Drugs WHERE Name = @name", conn))
                    {
                        getIdCmd.Parameters.AddWithValue("@name", drugName);
                        var result = getIdCmd.ExecuteScalar();
                        if (result != null)
                        {
                            drugId = Convert.ToInt32(result);
                        }
                    }

                    if (drugId > 0)
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand(@"
                        WITH cte AS (
                            SELECT TOP (1) *
                            FROM DrugStocks
                            WHERE DrugId = @drugId AND StockQuantity > 0
                            ORDER BY ExpirationDate
                        )
                        UPDATE cte
                        SET StockQuantity = StockQuantity - 1", conn))
                            {
                                cmd.Parameters.AddWithValue("@drugId", drugId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Ödeme alındı ve stok güncellendi. İyi günler!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvPayment.Rows.Clear();
                UpdatePayButtonText();
            }
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayDrugCards(allDrugsTable); // tüm ilaçları göster
            }
            else
            {
                DataView dv = allDrugsTable.DefaultView;

                // 🔍 Hem ilaç adına hem etken maddeye göre filtre uygula
                dv.RowFilter = $"Convert(Name, 'System.String') LIKE '%{searchTerm.Replace("'", "''")}%' OR " +
                               $"Convert(ActiveIngredient, 'System.String') LIKE '%{searchTerm.Replace("'", "''")}%'";

                DisplayDrugCards(dv.ToTable()); // filtreli listele
            }
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            string invoiceNumber = "FTR-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"IlacFis_{invoiceNumber}.pdf");


            if (dgvPayment.Rows.Count == 0)
            {
                MessageBox.Show("Sepet boş. Yazdırılacak fatura yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            iTextSharpText.Document doc = new iTextSharpText.Document();
            string path1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "IlacFis.pdf");

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                string fontPath = Path.Combine(Application.StartupPath, "Resources", "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharpText.Font headerFont = new iTextSharpText.Font(baseFont, 16, iTextSharpText.Font.BOLD);
                iTextSharpText.Font cellFont = new iTextSharpText.Font(baseFont, 12);

                iTextSharpText.Paragraph header = new iTextSharpText.Paragraph("İlaç Satış Faturası", headerFont);

                header.Alignment = iTextSharpText.Element.ALIGN_CENTER;
                doc.Add(header);
                doc.Add(new iTextSharpText.Paragraph(" ")); // boşluk

                PdfPTable table = new PdfPTable(dgvPayment.Columns.Count - 1); // ❌ butonu hariç
                table.WidthPercentage = 100;

                for (int i = 0; i < dgvPayment.Columns.Count - 1; i++)
                {
                    table.AddCell(new iTextSharpText.Phrase(dgvPayment.Columns[i].HeaderText, cellFont)); // ✅ DOĞRU
                }


                // Satırlar
                foreach (DataGridViewRow row in dgvPayment.Rows)
                {
                    for (int i = 0; i < dgvPayment.Columns.Count - 1; i++)
                    {
                        table.AddCell(new iTextSharpText.Phrase(row.Cells[i].Value?.ToString()));
                    }
                }

                doc.Add(table);

                doc.Add(new iTextSharpText.Paragraph("\nTarih: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm")));
                doc.Add(new iTextSharpText.Paragraph("Fatura No: " + invoiceNumber, cellFont));
                doc.Add(new iTextSharpText.Paragraph(" "));

                MessageBox.Show("PDF fatura masaüstüne kaydedildi:\n" + path, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF oluşturulurken hata: " + ex.Message);
            }
            finally
            {
                doc.Close();
            }
        }


        private void flpCards_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PharmacySellControl_Load_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            DisplayDrugCards(allDrugsTable); // tüm reçetesiz ilaçları yeniden göster
        }

        private void picHeader_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}