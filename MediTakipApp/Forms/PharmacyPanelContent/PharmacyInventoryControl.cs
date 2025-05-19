using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MediTakipApp.Utils;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class PharmacyInventoryControl : UserControl
    {
        private string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";
        private int selectedDrugId = 0;
        private int selectedSupplierMaxStock = 0;


        public PharmacyInventoryControl()
        {
            InitializeComponent();
        }

        private void PharmacyInventoryControl_Load(object sender, EventArgs e)
        {
            panelSupplyList.Width = (int)(this.Width * 0.62);

            btnTabStock.Click += (s, e) =>
            {
                ShowPanel(panelStockList);
                LoadStockCards();
            };

            btnTabSupply.Click += (s, e) =>
            {
                ShowPanel(panelSupply);
                selectedDrugId = 0;
                LoadSupplyCards();
            };

            btnTabHistory.Click += (s, e) =>
            {
                ShowPanel(panelHistory);
                LoadStockHistory();
                LoadAllSupplyTrackingGrid();
            };

            btnTabStock.PerformClick();
            cmbStockFilter.SelectedIndexChanged += (s, e) => LoadStockCards();
            txtSearch.TextChanged += (s, e) => LoadStockCards();
            txtSearchSupply.TextChanged += (s, e) => LoadSupplyCards(txtSearchSupply.Text);
            cmbSupplyFilter.SelectedIndexChanged += (s, e) => LoadSupplyCards(txtSearchSupply.Text, cmbSupplyFilter.SelectedItem.ToString());

            btnOpenSupplyForm.Click += (s, e) =>
            {
                var form = new SupplyForm();
                form.ShowDialog();
                LoadSupplyCards();
            };

        }

        private void LoadStockCards()
        {
            string query = @"
            SELECT 
                D.Id, D.Name, D.Barcode, D.ActiveIngredient, D.UsageAge, D.IsPrescription, D.Price,
                ISNULL(SUM(S.StockQuantity), 0) AS TotalStock,
                MIN(CASE WHEN S.StockQuantity > 0 THEN S.ExpirationDate ELSE NULL END) AS EarliestExpiry,
                COUNT(CASE WHEN S.ExpirationDate < GETDATE() AND S.StockQuantity > 0 THEN 1 ELSE NULL END) AS ExpiredPartCount
            FROM Drugs D
            LEFT JOIN DrugStocks S ON D.Id = S.DrugId
            GROUP BY D.Id, D.Name, D.Barcode, D.ActiveIngredient, D.UsageAge, D.IsPrescription, D.Price";


            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            // === Dashboard sadece ilk kez eklenir ===
            if (!panelStockList.Controls.Contains(dashboardPanel))
            {
                dashboardPanel.Height = 90;
                dashboardPanel.Dock = DockStyle.Top;
                dashboardPanel.BackColor = Color.WhiteSmoke;
                dashboardPanel.BorderStyle = BorderStyle.FixedSingle;
                dashboardPanel.Controls.Clear();

                int totalDrugs = dt.Rows.Count;
                int lowStock = dt.AsEnumerable().Count(r => Convert.ToInt32(r["TotalStock"]) < 50);
                int expiringSoon = dt.AsEnumerable().Count(r =>
                {
                    if (r["EarliestExpiry"] == DBNull.Value) return false;
                    DateTime expiry = Convert.ToDateTime(r["EarliestExpiry"]);
                    return expiry <= DateTime.Today.AddMonths(1);
                });

                TableLayoutPanel layout = new TableLayoutPanel
                {
                    RowCount = 2,
                    ColumnCount = 1,
                    Dock = DockStyle.Fill,
                    BackColor = Color.WhiteSmoke
                };
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

                Panel row1 = new Panel { Dock = DockStyle.Fill };
                Panel row2 = new Panel { Dock = DockStyle.Fill };

                row1.Controls.Add(CreateDashboardLabel($"💊 Toplam İlaç: {totalDrugs}", new Point(10, 10)));
                row1.Controls.Add(CreateDashboardLabel($"⚠️ Düşük Stok: {lowStock}", new Point(250, 10)));
                row1.Controls.Add(CreateDashboardLabel($"⏳ SKT Yaklaşan: {expiringSoon}", new Point(480, 10)));

                Label lblFilter = new Label
                {
                    Text = "📂 Filtrele:",
                    Location = new Point(10, 15),
                    AutoSize = true,
                    Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
                };
                cmbStockFilter.Location = new Point(110, 12);
                cmbStockFilter.Size = new Size(180, 28);

                Label lblSearch = new Label
                {
                    Text = "🔍 Ara (İsim / Barkod):",
                    Location = new Point(310, 15),
                    AutoSize = true,
                    Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
                };
                txtSearch.Location = new Point(500, 12);
                txtSearch.Size = new Size(180, 28);

                row2.Controls.Add(lblFilter);
                row2.Controls.Add(cmbStockFilter);
                row2.Controls.Add(lblSearch);
                row2.Controls.Add(txtSearch);

                layout.Controls.Add(row1, 0, 0);
                layout.Controls.Add(row2, 0, 1);
                dashboardPanel.Controls.Add(layout);
                panelStockList.Controls.Add(dashboardPanel);
            }

            // === Panel container hazırlığı ===
            if (panelCards == null)
            {
                panelCards = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    AutoScroll = true
                };

                panelStockList.Controls.Add(panelCards);
            }

            // === FlowLayoutPanel temizlenir veya oluşturulur ===
            FlowLayoutPanel flp = null;
            flp = panelCards.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            flp?.Controls.Clear();

            if (flp == null)
            {
                flp = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    Padding = new Padding(10),
                    WrapContents = true
                };
                panelCards.Controls.Add(flp);
            }


            // === Filtre ve arama ===
            string selectedFilter = cmbStockFilter?.SelectedItem?.ToString() ?? "Tümü";
            string search = txtSearch?.Text.ToLower() ?? "";

            foreach (DataRow row in dt.Rows)
            {
                string name = row["Name"].ToString().ToLower();
                string barcode = row["Barcode"].ToString().ToLower();

                if (!string.IsNullOrWhiteSpace(search) && !name.Contains(search) && !barcode.Contains(search))
                    continue;

                int stock = Convert.ToInt32(row["TotalStock"]);
                DateTime? expiry = row["EarliestExpiry"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["EarliestExpiry"]);
                bool isPrescription = Convert.ToBoolean(row["IsPrescription"]);

                if (selectedFilter == "Stok Az (<50)" && stock >= 50)
                    continue;
                if (selectedFilter == "SKT Geçmiş" && (expiry == null || expiry > DateTime.Today))
                    continue;
                if (selectedFilter == "SKT Yaklaşan (1 ay)" &&
                    (expiry == null || expiry > DateTime.Today.AddMonths(1) || expiry <= DateTime.Today))
                    continue;
                if (selectedFilter == "Sadece Reçeteli" && !isPrescription)
                    continue;
                if (selectedFilter == "Sadece Reçetesiz" && isPrescription)
                    continue;

                flp.Controls.Add(CreateStockCard(row));
            }
                lblNoResultsStock.Visible = flp.Controls.Count == 0;
        }

        private Panel CreateStockCard(DataRow row)
        {
            Panel card = new Panel
            {
                Width = 300,
                Height = 280,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            string name = row["Name"].ToString();
            string ingredient = row["ActiveIngredient"].ToString();
            string barcode = row["Barcode"].ToString();
            int usageAge = Convert.ToInt32(row["UsageAge"]);
            decimal price = Convert.ToDecimal(row["Price"]);
            int stock = row["TotalStock"] == DBNull.Value ? 0 : Convert.ToInt32(row["TotalStock"]);
            DateTime? expiry = row["EarliestExpiry"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["EarliestExpiry"]);
            bool isPrescription = Convert.ToBoolean(row["IsPrescription"]);

            int drugId = Convert.ToInt32(row["Id"]);

            // === Arka Plan Renkleri ===
            if (stock == 0)
                card.BackColor = Color.FromArgb(255, 230, 230); // Açık kırmızı
            else if (expiry.HasValue &&
                     expiry.Value > DateTime.Today &&
                     expiry.Value <= DateTime.Today.AddMonths(1))
                card.BackColor = Color.FromArgb(255, 245, 200); // Açık turuncu

            card.Controls.Add(new TransparentLabel { Text = $"💊 {name}", Font = new Font("Bahnschrift", 14, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🔬 {ingredient}", Location = new Point(10, 40), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🏷️ Barkod: {barcode}", Location = new Point(10, 60), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"👶 Yaş: {usageAge}+", Location = new Point(10, 80), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"💰 Fiyat: {price:C2}", Location = new Point(10, 100), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"📦 Stok: {stock}", Location = new Point(10, 120), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"⏳ SKT: {(expiry == null ? "-" : expiry.Value.ToShortDateString())}", Location = new Point(10, 140), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = isPrescription ? "🩺 Reçeteli" : "🆓 Reçetesiz", ForeColor = isPrescription ? Color.Maroon : Color.ForestGreen, Location = new Point(10, 160), AutoSize = true });

            int expiredPartCount = row["ExpiredPartCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["ExpiredPartCount"]);
            int verticalOffset = 180;

            if (expiredPartCount > 0)
            {
                card.BackColor = Color.FloralWhite;
                card.Controls.Add(new TransparentLabel
                {
                    Text = "⛔ SKT geçmiş parti mevcut!",
                    ForeColor = Color.Maroon,
                    Font = new Font("Bahnschrift SemiCondensed", 10, FontStyle.Bold),
                    Location = new Point(10, verticalOffset),
                    AutoSize = true
                });
                verticalOffset += 20;
            }

            if (stock < 50)
            {
                card.Controls.Add(new TransparentLabel
                {
                    Text = stock == 0 ? "❌ Stok Tükenmiş!" : "⚠️ Stok az!",
                    Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                    ForeColor = stock == 0 ? Color.DarkRed : Color.DarkOrange,
                    Location = new Point(10, verticalOffset),
                    AutoSize = true
                });
            }

            Button btnSupply = new Button
            {
                Text = "➕ Tedarik Et",
                Size = new Size(270, 40),
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                Location = new Point(10, 225),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSupply.Click += (s, e) =>
            {
                btnTabSupply.PerformClick();
                selectedDrugId = Convert.ToInt32(row["Id"]);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Drugs WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedDrugId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                        ShowSupplyForm(dt.Rows[0]);
                }
            };


            card.Controls.Add(btnSupply);
            return card;
        }

        private Label CreateDashboardLabel(string text, Point location)
        {
            return new TransparentLabel
            {
                Text = text,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = location,
                AutoSize = true
            };
        }

        //----------------------------------------------------------------------------------

        private void LoadSupplyCards(string search = "", string filter = "Tümü")
        {
            panelSupplyList.Controls.Clear();
            panelSupplyList.VerticalScroll.Value = 0;
            panelSupplyList.AutoScrollPosition = new Point(0, 0);

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Barcode, ActiveIngredient, UsageAge, IsPrescription, Price FROM Drugs", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            FlowLayoutPanel flp = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                AutoSize = false,
                Padding = new Padding(10),
                WrapContents = true
            };

            foreach (DataRow row in dt.Rows)
            {
                string name = row["Name"].ToString().ToLower();
                string barcode = row["Barcode"].ToString().ToLower();
                bool isPrescription = Convert.ToBoolean(row["IsPrescription"]);

                if (!string.IsNullOrWhiteSpace(search) && !name.Contains(search.ToLower()) && !barcode.Contains(search.ToLower()))
                    continue;

                if (filter == "Sadece Reçeteli" && !isPrescription)
                    continue;
                if (filter == "Sadece Reçetesiz" && isPrescription)
                    continue;

                flp.Controls.Add(CreateSupplyCard(row));
            }

            lblNoResultsSupplyStock.Visible = flp.Controls.Count == 0;
            flp.Controls.Add(lblNoResultsSupplyStock);

            panelSupplyList.Controls.Add(flp);
        }

        private Panel CreateSupplyCard(DataRow row)
        {
            Panel card = new Panel
            {
                Width = 300,
                Height = 230,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            int drugId = Convert.ToInt32(row["Id"]);
            string name = row["Name"].ToString();
            string barcode = row["Barcode"].ToString();
            string ingredient = row["ActiveIngredient"].ToString();
            int usageAge = Convert.ToInt32(row["UsageAge"]);
            decimal price = Convert.ToDecimal(row["Price"]);
            bool isPrescription = Convert.ToBoolean(row["IsPrescription"]);

            card.Controls.Add(new TransparentLabel() { Text = $"💊 {name}", Location = new Point(10, 10), AutoSize = true });
            card.Controls.Add(new TransparentLabel() { Text = $"🔬 {ingredient}", Location = new Point(10, 35), AutoSize = true });
            card.Controls.Add(new TransparentLabel() { Text = $"🏷️ Barkod: {barcode}", Location = new Point(10, 60), AutoSize = true });
            card.Controls.Add(new TransparentLabel() { Text = $"👶 Yaş: {usageAge}+", Location = new Point(10, 85), AutoSize = true });
            card.Controls.Add(new TransparentLabel() { Text = $"💰 Fiyat: {price:C2}", Location = new Point(10, 110), AutoSize = true });

            TransparentLabel lblRecete = new TransparentLabel()
            {
                Text = isPrescription ? "🩺 Reçeteli" : "🆓 Reçetesiz",
                ForeColor = isPrescription ? Color.Maroon : Color.ForestGreen,
                Location = new Point(10, 135),
                AutoSize = true
            };
            card.Controls.Add(lblRecete);

            Button btnSupply = new Button()
            {
                Text = "➕ Tedarik Et",
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(10, 165),
                Size = new Size(270, 40)
            };
            btnSupply.FlatAppearance.BorderSize = 0;

            btnSupply.Click += (s, e) =>
            {
                selectedDrugId = drugId;
                ShowSupplyForm(row);
            };

            card.Controls.Add(btnSupply);
            return card;
        }

        private void ShowSupplyForm(DataRow row)
        {
            panelSupplyForm.Controls.Clear();

            GroupBox grpSuppliers = new GroupBox
            {
                Text = "🌍 Uygun Tedarikçiler",
                Font = new Font("Bahnschrift SemiCondensed", 13F, FontStyle.Bold),
                Location = new Point(10, 50),
                Size = new Size(600, 200),
                BackColor = Color.White
            };

            TableLayoutPanel tblSuppliers = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                AutoScroll = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoSize = false,
            };

            tblSuppliers.RowStyles.Clear();
            tblSuppliers.RowCount = 1;
            tblSuppliers.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            tblSuppliers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tblSuppliers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tblSuppliers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tblSuppliers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tblSuppliers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

            tblSuppliers.Controls.Add(new Label { Text = "Tedarikçi", Font = new Font("Bahnschrift", 12F, FontStyle.Bold), AutoSize = true });
            tblSuppliers.Controls.Add(new Label { Text = "Bölge", Font = new Font("Bahnschrift", 12F, FontStyle.Bold), AutoSize = true });
            tblSuppliers.Controls.Add(new Label { Text = "Fiyat", Font = new Font("Bahnschrift", 12F, FontStyle.Bold), AutoSize = true });
            tblSuppliers.Controls.Add(new Label { Text = "Stok", Font = new Font("Bahnschrift", 12F, FontStyle.Bold), AutoSize = true });
            tblSuppliers.Controls.Add(new Label { Text = "Seç", Font = new Font("Bahnschrift", 12F, FontStyle.Bold), AutoSize = true });

            bool hasSupplier = false;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT S.Name, S.Region, DS.UnitPrice, DS.StockAvailable
            FROM DrugSuppliers DS
            JOIN Suppliers S ON S.Id = DS.SupplierId
            WHERE DS.DrugId = @drugId", conn);
                cmd.Parameters.AddWithValue("@drugId", selectedDrugId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hasSupplier = true;

                        string supplierName = reader.GetString(0);
                        string region = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        int stock = reader.GetInt32(3);

                        tblSuppliers.RowCount += 1;
                        tblSuppliers.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

                        tblSuppliers.Controls.Add(new Label { Text = supplierName, Anchor = AnchorStyles.Left, AutoSize = true, Padding = new Padding(5, 5, 0, 0) });
                        tblSuppliers.Controls.Add(new Label { Text = region, Anchor = AnchorStyles.Left, AutoSize = true, Padding = new Padding(5, 5, 0, 0) });
                        tblSuppliers.Controls.Add(new Label { Text = $"{price:C2}", Anchor = AnchorStyles.Left, AutoSize = true, Padding = new Padding(5, 5, 0, 0) });
                        tblSuppliers.Controls.Add(new Label { Text = stock.ToString(), Anchor = AnchorStyles.Left, AutoSize = true, Padding = new Padding(5, 5, 0, 0) });

                        Button btnSelect = new Button
                        {
                            Text = "➕",
                            Anchor = AnchorStyles.Left,
                            BackColor = Color.RoyalBlue,
                            ForeColor = Color.White,
                            Font = new Font("Bahnschrift", 10F, FontStyle.Bold),
                            Size = new Size(70, 30),
                            FlatStyle = FlatStyle.Flat
                        };
                        btnSelect.FlatAppearance.BorderSize = 0;

                        btnSelect.Click += (s, e) =>
                        {
                            foreach (Control control in tblSuppliers.Controls)
                            {
                                if (control is Button btn)
                                    btn.BackColor = Color.RoyalBlue;
                            }

                            btnSelect.BackColor = Color.Green;

                            var found = panelSupplyForm.Controls.Find("txtSupplier", true).FirstOrDefault();
                            if (found is TextBox txt)
                            {
                                txt.Text = supplierName;
                                selectedSupplierMaxStock = stock;
                            }

                            // SKT otomatik ata (DefaultExpiryDate üzerinden)
                            var dtp = panelSupplyForm.Controls.Find("dtpExpiry", true).FirstOrDefault() as DateTimePicker;
                            if (dtp != null)
                            {
                                using (SqlConnection conn2 = new SqlConnection(connStr))
                                {
                                    conn2.Open();
                                    SqlCommand cmd2 = new SqlCommand(@"
                                SELECT DefaultExpiryDate 
                                FROM DrugSuppliers DS
                                JOIN Suppliers S ON S.Id = DS.SupplierId
                                WHERE DS.DrugId = @drugId AND S.Name = @supplierName", conn2);

                                    cmd2.Parameters.AddWithValue("@drugId", selectedDrugId);
                                    cmd2.Parameters.AddWithValue("@supplierName", supplierName);

                                    object result = cmd2.ExecuteScalar();
                                    if (result != null && result != DBNull.Value)
                                        dtp.Value = Convert.ToDateTime(result);
                                }
                            }

                        };

                        tblSuppliers.Controls.Add(btnSelect);
                    }
                }
            }

            if (!hasSupplier)
            {
                tblSuppliers.Controls.Add(new Label
                {
                    Text = "❌ Uygun tedarikçi bulunamadı.",
                    AutoSize = true,
                    ForeColor = Color.DarkRed,
                    Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Italic),
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(5),
                    Padding = new Padding(5)
                });
            }

            grpSuppliers.Controls.Add(tblSuppliers);
            panelSupplyForm.Controls.Add(grpSuppliers);

            // === İlaç Başlığı ===
            Label lblTitle = new Label
            {
                Text = $"💊 {row["Name"]} - Tedarik Girişi",
                Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.MidnightBlue,
                Location = new Point(10, 10)
            };
            panelSupplyForm.Controls.Add(lblTitle);


            // === Giriş Formu Kutusu ===
            GroupBox grpForm = new GroupBox
            {
                Text = "📥 Tedarik Bilgileri",
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                Location = new Point(10, 270),
                Size = new Size(600, 230),
                BackColor = Color.White
            };

            // 🔢 Miktar
            Label lblQuantity = new Label { Text = "🔢 Miktar:", Location = new Point(20, 40), AutoSize = true };
            NumericUpDown numQuantity = new NumericUpDown
            {
                Name = "numQuantity",
                Location = new Point(150, 35),
                Width = 100,
                Minimum = 1,
                Maximum = 10000
            };

            // 📅 SKT
            Label lblExpiry = new Label { Text = "📅 SKT:", Location = new Point(20, 80), AutoSize = true };
            DateTimePicker dtpExpiry = new DateTimePicker
            {
                Name = "dtpExpiry",
                Location = new Point(150, 75),
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd MMM yyyy",
                ShowUpDown = false,
                Enabled = false,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black
            };

            // 🏭 Tedarikçi
            Label lblSupplier = new Label { Text = "🏭 Tedarikçi:", Location = new Point(20, 120), AutoSize = true };
            TextBox txtSupplier = new TextBox
            {
                Name = "txtSupplier",
                Location = new Point(150, 115),
                Width = 300,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.DarkSlateGray
            };

            // 💾 Buton
            Button btnSave = new Button
            {
                Text = "💾 Tedarik Et ve Kaydet",
                Location = new Point(20, 170),
                Width = 550,
                Height = 45,
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;

            btnSave.Click += (s, e) =>
            {
                int qty = (int)numQuantity.Value;
                DateTime expiry = dtpExpiry.Value;
                string supplier = txtSupplier.Text.Trim();

                if (qty <= 0 || string.IsNullOrWhiteSpace(supplier))
                {
                    MessageBox.Show("Lütfen geçerli miktar ve tedarikçi girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (qty > selectedSupplierMaxStock)
                {
                    MessageBox.Show($"Seçtiğiniz tedarikçide sadece {selectedSupplierMaxStock} adet stok var!",
                        "Stok Yetersiz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                INSERT INTO DrugStocks (DrugId, StockQuantity, ExpirationDate, EntryDate, Supplier)
                VALUES (@drugId, @qty, @expiry, GETDATE(), @supplier)", conn);

                    cmd.Parameters.AddWithValue("@drugId", selectedDrugId);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@expiry", expiry);
                    cmd.Parameters.AddWithValue("@supplier", supplier);

                    cmd.ExecuteNonQuery();
                    SaveSupplyTracking(selectedDrugId, qty, supplier);
                }

                MessageBox.Show("İlaç başarıyla tedarik edildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSupplyCards();
            };

            grpForm.Controls.Add(lblQuantity);
            grpForm.Controls.Add(numQuantity);
            grpForm.Controls.Add(lblExpiry);
            grpForm.Controls.Add(dtpExpiry);
            grpForm.Controls.Add(lblSupplier);
            grpForm.Controls.Add(txtSupplier);
            grpForm.Controls.Add(btnSave);
            panelSupplyForm.Controls.Add(grpForm);
        }

        //----------------------------------------------------------------------------------

        private void LoadStockHistory()
        {
            if (cachedHistoryData == null)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    D.Name AS [İlaç Adı],
                    S.StockQuantity AS [Miktar],
                    FORMAT(S.ExpirationDate, 'dd.MM.yyyy') AS [SKT],
                    FORMAT(S.EntryDate, 'dd.MM.yyyy') AS [Giriş Tarihi],
                    S.Supplier AS [Tedarikçi]
                FROM DrugStocks S
                INNER JOIN Drugs D ON D.Id = S.DrugId
                ORDER BY S.EntryDate DESC", conn);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cachedHistoryData = new DataTable();
                    da.Fill(cachedHistoryData);
                }

                CreateHistoryLayout();
            }

            ApplyHistoryFilter();
        }

        private void CreateHistoryLayout()
        {
            panelHistory.Controls.Clear();

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 65));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 35));

            Panel topBar = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 50,
                Padding = new Padding(10),
                BackColor = Color.WhiteSmoke
            };

            TransparentLabel lblTitle = new TransparentLabel()
            {
                Text = "📜 Tüm Tedarik Girişleri",
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            Label lblSearch = new Label()
            {
                Text = "🔍 İlaç Adı:",
                Location = new Point(270, 15),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };

            txtSearchHistory = new TextBox()
            {
                Location = new Point(370, 12),
                Size = new Size(200, 26),
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };
            txtSearchHistory.TextChanged += (s, e) => ApplyHistoryFilter();

            Button btnRefresh = new Button()
            {
                Text = "🔁 Yenile",
                Location = new Point(580, 10),
                Size = new Size(100, 30),
                BackColor = Color.LightSteelBlue,
                FlatStyle = FlatStyle.Flat
            };
            btnRefresh.Click += (s, e) =>
            {
                cachedHistoryData = null;
                LoadStockHistory();
                LoadAllSupplyTrackingGrid();
            };

            Button btnExport = new Button()
            {
                Text = "📄 Dışa Aktar",
                Location = new Point(700, 10),
                Size = new Size(150, 30),
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExport.Click += (s, e) => ExportToCsv(dgvHistory.DataSource as DataTable, dgvGroupedHistory.DataSource as DataTable);

            int totalStock = cachedHistoryData.AsEnumerable()
                .Sum(r => Convert.ToInt32(r["Miktar"]));

            var uniqueDrugs = cachedHistoryData.AsEnumerable()
                .Select(r => r["İlaç Adı"].ToString())
                .Distinct()
                .Count();

            var topDrug = cachedHistoryData.AsEnumerable()
                .GroupBy(r => r["İlaç Adı"].ToString())
                .OrderByDescending(g => g.Sum(r => Convert.ToInt32(r["Miktar"])))
                .Select(g => g.Key)
                .FirstOrDefault() ?? "-";

            TransparentLabel lblTotalStock = new TransparentLabel()
            {
                Text = $"🔢 Toplam Stok: {totalStock} adet",
                Location = new Point(900, 12),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };

            TransparentLabel lblTotalDrugs = new TransparentLabel()
            {
                Text = $"💊 Toplam İlaç: {uniqueDrugs}",
                Location = new Point(1150, 12),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };

            TransparentLabel lblTopDrug = new TransparentLabel()
            {
                Text = $"🏆 En Çok: {topDrug}",
                Location = new Point(1350, 12),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };

            topBar.Controls.Add(lblTotalStock);
            topBar.Controls.Add(lblTotalDrugs);
            topBar.Controls.Add(lblTopDrug);
            topBar.Controls.Add(lblTitle);
            topBar.Controls.Add(btnRefresh);
            topBar.Controls.Add(btnExport);
            topBar.Controls.Add(lblSearch);
            topBar.Controls.Add(txtSearchHistory);

            dgvHistory = new DataGridView()
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvHistory.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            // === İmha Et Butonu ===
            DataGridViewButtonColumn btnDeleteColumn = new DataGridViewButtonColumn
            {
                HeaderText = "İşlem",
                Text = "🗑️ İmha Et",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvHistory.Columns.Add(btnDeleteColumn);
            dgvHistory.CellContentClick += DgvHistory_CellContentClick;

            dgvGroupedHistory = new DataGridView()
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvGroupedHistory.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F);
            dgvGroupedHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold);
            dgvGroupedHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
            dgvGroupedHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGroupedHistory.EnableHeadersVisualStyles = false;
            dgvGroupedHistory.DefaultCellStyle.SelectionBackColor = Color.LightGreen;

            dgvSupplyTracking = new DataGridView()
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvSupplyTracking.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F);
            dgvSupplyTracking.ColumnHeadersDefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold);
            dgvSupplyTracking.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
            dgvSupplyTracking.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSupplyTracking.EnableHeadersVisualStyles = false;
            dgvSupplyTracking.DefaultCellStyle.SelectionBackColor = Color.LightGreen;

            dgvSupplyTracking.Columns.Add("DrugName", "İlaç");
            dgvSupplyTracking.Columns.Add("EntryDate", "Tarih");
            dgvSupplyTracking.Columns.Add("Quantity", "Miktar");
            dgvSupplyTracking.Columns.Add("Supplier", "Tedarikçi");
            dgvSupplyTracking.Columns.Add("Status", "Durum");

            layout.Controls.Add(topBar, 0, 0);
            layout.SetColumnSpan(topBar, 1);
            layout.Controls.Add(dgvHistory, 0, 1);

            TableLayoutPanel bottomRow = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            bottomRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            bottomRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            bottomRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            bottomRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            Label lblGroupedTitle = new Label
            {
                Text = "📦 Gruplu Tedarik Miktarı",
                Font = new Font("Bahnschrift SemiCondensed", 13F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            Label lblTrackingTitle = new Label
            {
                Text = "🚚 Tedarik Süreci",
                Font = new Font("Bahnschrift SemiCondensed", 13F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            bottomRow.Controls.Add(lblGroupedTitle, 0, 0);
            bottomRow.Controls.Add(lblTrackingTitle, 1, 0);
            bottomRow.Controls.Add(dgvGroupedHistory, 0, 1);
            bottomRow.Controls.Add(dgvSupplyTracking, 1, 1);

            layout.Controls.Add(bottomRow, 0, 2);

            panelHistory.Controls.Add(layout);
        }

        private void DgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHistory.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                string ilacAdi = dgvHistory.Rows[e.RowIndex].Cells["İlaç Adı"].Value.ToString();
                string tarih = dgvHistory.Rows[e.RowIndex].Cells["Giriş Tarihi"].Value.ToString();
                string tedarikci = dgvHistory.Rows[e.RowIndex].Cells["Tedarikçi"].Value.ToString();
                string skt = dgvHistory.Rows[e.RowIndex].Cells["SKT"].Value.ToString();

                DialogResult result = MessageBox.Show($"\"{ilacAdi}\" ilacına ait bu parti silinecek. Emin misiniz?", "İmha Et", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(@"
                    DELETE FROM DrugStocks
                    WHERE 
                        DrugId = (SELECT Id FROM Drugs WHERE Name = @name)
                        AND FORMAT(EntryDate, 'dd.MM.yyyy') = @entryDate
                        AND FORMAT(ExpirationDate, 'dd.MM.yyyy') = @expiryDate
                        AND Supplier = @supplier", conn);

                        cmd.Parameters.AddWithValue("@name", ilacAdi);
                        cmd.Parameters.AddWithValue("@entryDate", tarih);
                        cmd.Parameters.AddWithValue("@expiryDate", skt);
                        cmd.Parameters.AddWithValue("@supplier", tedarikci);

                        int affected = cmd.ExecuteNonQuery();
                        if (affected > 0)
                        {
                            MessageBox.Show("İlaç partisi başarıyla imha edildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cachedHistoryData = null;
                            LoadStockHistory();
                        }
                        else
                        {
                            MessageBox.Show("Silme işlemi başarısız oldu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void SaveSupplyTracking(int drugId, int qty, string supplier, string status = "🕓 Sipariş Alındı...")
        {
            const string sql = @"
        INSERT INTO SupplyTracking
            (DrugId, EntryDate, Quantity, Supplier, Status)
        VALUES
            (@drugId, GETDATE(), @qty, @supplier, @status)";
            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@drugId", drugId);
            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.Parameters.AddWithValue("@supplier", supplier);
            cmd.Parameters.AddWithValue("@status", status);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        private void LoadAllSupplyTrackingGrid()
        {
            if (dgvSupplyTracking.Columns.Count == 0)
            {
                dgvSupplyTracking.Columns.Add("DrugName", "İlaç");
                dgvSupplyTracking.Columns.Add("EntryDate", "Tarih");
                dgvSupplyTracking.Columns.Add("Quantity", "Miktar");
                dgvSupplyTracking.Columns.Add("Supplier", "Tedarikçi");
                dgvSupplyTracking.Columns.Add("Status", "Durum");
            }

            dgvSupplyTracking.Rows.Clear();

            const string sql = @"
        SELECT D.Name     AS DrugName,
               ST.EntryDate,
               ST.Quantity,
               ST.Supplier,
               ST.Status
        FROM SupplyTracking ST
        JOIN Drugs D ON ST.DrugId = D.Id
        ORDER BY ST.EntryDate DESC";

            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dgvSupplyTracking.Rows.Add(
                    reader.GetString(reader.GetOrdinal("DrugName")),
                    Convert.ToDateTime(reader["EntryDate"]).ToShortDateString(),
                    reader.GetInt32(reader.GetOrdinal("Quantity")),
                    reader.GetString(reader.GetOrdinal("Supplier")),
                    reader.GetString(reader.GetOrdinal("Status"))
                );
            }
        }

        private void ApplyHistoryFilter()
        {
            string keyword = txtSearchHistory.Text.ToLower();
            var filtered = cachedHistoryData.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                filtered = filtered.Where(r =>
                    r["İlaç Adı"].ToString().ToLower().Contains(keyword));
            }

            var dt = filtered.Any() ? filtered.CopyToDataTable() : cachedHistoryData.Clone();
            dgvHistory.DataSource = dt;

            // grup hesapla
            var dtGrouped = new DataTable();
            dtGrouped.Columns.Add("İlaç Adı");
            dtGrouped.Columns.Add("Toplam Miktar", typeof(int));

            var grouped = dt.AsEnumerable()
                .GroupBy(r => r["İlaç Adı"].ToString())
                .Select(g => new { Name = g.Key, Total = g.Sum(r => Convert.ToInt32(r["Miktar"])) });

            foreach (var g in grouped)
                dtGrouped.Rows.Add(g.Name, g.Total);

            dgvGroupedHistory.DataSource = dtGrouped;
        }

        private void ExportToCsv(DataTable dt, DataTable dtGrouped)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "CSV dosyası (*.csv)|*.csv";
                saveFile.FileName = "TedarikRaporu.csv";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFile.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("📜 Tüm Tedarik Girişleri");
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sw.Write(dt.Columns[i].ColumnName);
                            if (i < dt.Columns.Count - 1)
                                sw.Write(";");
                        }
                        sw.WriteLine();

                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                string cell = row[i].ToString().Replace(";", " ");
                                sw.Write(cell);
                                if (i < dt.Columns.Count - 1)
                                    sw.Write(";");
                            }
                            sw.WriteLine();
                        }

                        sw.WriteLine();

                        sw.WriteLine("💊 İlaç Bazında Toplamlar");
                        for (int i = 0; i < dtGrouped.Columns.Count; i++)
                        {
                            sw.Write(dtGrouped.Columns[i].ColumnName);
                            if (i < dtGrouped.Columns.Count - 1)
                                sw.Write(";");
                        }
                        sw.WriteLine();

                        foreach (DataRow row in dtGrouped.Rows)
                        {
                            for (int i = 0; i < dtGrouped.Columns.Count; i++)
                            {
                                string cell = row[i].ToString().Replace(";", " ");
                                sw.Write(cell);
                                if (i < dtGrouped.Columns.Count - 1)
                                    sw.Write(";");
                            }
                            sw.WriteLine();
                        }
                    }

                    MessageBox.Show("CSV olarak dışa aktarıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowPanel(Panel panelToShow)
        {
            foreach (Control ctrl in panelMainContent.Controls)
                ctrl.Visible = false;

            panelToShow.Visible = true;
        }

    }
}