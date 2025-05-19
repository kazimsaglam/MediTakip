using Font = System.Drawing.Font;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class PharmacyHomeControl : UserControl
    {
        private readonly string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";

        private System.Windows.Forms.Timer timer1;
        private Label lblPharmacist;
        private Label lblClock;
        private Label lblTotalPrescriptions;
        private Label lblTotalSales;
        private FlowLayoutPanel flpTopDrugs;
        private FlowLayoutPanel flpLowStock;
        private Panel panelWrapper;
        private Panel statsPanel;
        private ComboBox cmbRafFilter;

        public PharmacyHomeControl()
        {
            InitializeComponent();
            this.BackColor = Color.WhiteSmoke;
            InitializeDynamicControls();
            CustomizeStyle();
            LoadDashboardData();

            timer1 = new System.Windows.Forms.Timer { Interval = 1000 };
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void InitializeDynamicControls()
        {
            panelWrapper = new Panel { Dock = DockStyle.Fill, BackColor = this.BackColor };
            this.Controls.Add(panelWrapper);

            lblPharmacist = new Label
            {
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Text = "Eczacı DENİZ – Görevli Eczacı"
            };
            panelWrapper.Controls.Add(lblPharmacist);

            lblClock = new Label
            {
                Location = new Point(900, 20),
                Font = new Font("Segoe UI", 10),
                AutoSize = true
            };
            panelWrapper.Controls.Add(lblClock);

            statsPanel = new Panel { Location = new Point(20, 60), Size = new Size(940, 120), BackColor = Color.White };
            panelWrapper.Controls.Add(statsPanel);

            lblTotalPrescriptions = CreateStatsCard("Günlük Reçete", "0", new Point(0, 0));
            lblTotalSales = CreateStatsCard("Günlük Satış", "0", new Point(240, 0));

            flpTopDrugs = CreateFlowPanel("En Çok Satılanlar", new Point(20, 220));
            panelWrapper.Controls.Add(flpTopDrugs);

            flpLowStock = CreateFlowPanel("Kritik Stok", new Point(500, 220));
            panelWrapper.Controls.Add(flpLowStock);

            // Yeni filtre paneli - Kritik Stok altına
            Panel pnlFilterBar = new Panel
            {
                Location = new Point(flpLowStock.Location.X, flpLowStock.Location.Y + flpLowStock.Height + 10),
                Size = new Size(flpLowStock.Width, 45),
                BackColor = Color.Gainsboro,
                BorderStyle = BorderStyle.FixedSingle
            };

            cmbRafFilter = new ComboBox
            {
                Width = 200,
                Height = 30,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(10, 7)
            };
            cmbRafFilter.Items.AddRange(new object[] { "Tümü", "A", "B", "C", "D", "E" });
            cmbRafFilter.SelectedIndex = 0;
            cmbRafFilter.SelectedIndexChanged += CmbRafFilter_SelectedIndexChanged;

            Button btnExportLowStockPdf = new Button
            {
                Text = "Kritik Stok PDF",
                Width = 150,
                Height = 30,
                BackColor = Color.LightCoral,
                ForeColor = Color.White,
                Location = new Point(cmbRafFilter.Right + 20, 7),
                FlatStyle = FlatStyle.Flat
            };
            btnExportLowStockPdf.Click += BtnExportLowStockPdf_Click;

            pnlFilterBar.Controls.Add(cmbRafFilter);
            pnlFilterBar.Controls.Add(btnExportLowStockPdf);
            panelWrapper.Controls.Add(pnlFilterBar);
        }

        private void CmbRafFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private Label CreateStatsCard(string title, string value, Point location)
        {
            Panel card = new Panel
            {
                Location = location,
                Size = new Size(220, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(10, 40),
                AutoSize = true
            };
            card.Controls.Add(lblValue);

            statsPanel.Controls.Add(card);
            return lblValue;
        }

        private void BtnExportLowStockPdf_Click(object? sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string rafFilter = cmbRafFilter?.SelectedItem?.ToString();
                    string filterCondition = "";
                    if (!string.IsNullOrEmpty(rafFilter) && rafFilter != "Tümü")
                        filterCondition = $"WHERE d.ShelfLocation LIKE '{rafFilter}%'";

                    SqlCommand cmd = new SqlCommand($@"
                        SELECT d.Name, ds.StockQuantity, d.ShelfLocation 
                        FROM DrugStocks ds 
                        JOIN Drugs d ON ds.DrugId = d.Id 
                        {filterCondition} 
                        ORDER BY ds.StockQuantity ASC", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    var doc = new Document();
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Kritik_Stok_Raporu.pdf");
                    PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();

                    var titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                    var normalFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL);

                    doc.Add(new Paragraph("Kritik Stoktaki İlaçlar", titleFont));
                    doc.Add(new Paragraph("\n"));

                    PdfPTable table = new PdfPTable(3);
                    table.AddCell("İlaç Adı");
                    table.AddCell("Stok Miktarı");
                    table.AddCell("Raf");

                    while (reader.Read())
                    {
                        int stock = Convert.ToInt32(reader["StockQuantity"]);
                        if (stock < 50)
                        {
                            table.AddCell(reader["Name"].ToString());
                            table.AddCell(stock.ToString());
                            table.AddCell(reader["ShelfLocation"].ToString());
                        }
                    }

                    reader.Close();
                    doc.Add(table);
                    doc.Close();

                    MessageBox.Show("PDF başarıyla oluşturuldu:\n" + filePath, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start("explorer", filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF oluşturulurken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private FlowLayoutPanel CreateFlowPanel(string title, Point location)
        {
            Label lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = location,
                AutoSize = true
            };
            panelWrapper.Controls.Add(lbl);

            FlowLayoutPanel flp = new FlowLayoutPanel
            {
                Location = new Point(location.X, location.Y + 30),
                Size = new Size(460, 400),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            return flp;
        }

        private Panel CreateDrugCard(string drugName, string detail, bool isCritical, string shelfInfo)
        {
            Panel card = new Panel
            {
                Size = new Size(200, 90),
                BackColor = isCritical ? Color.MistyRose : Color.Honeydew,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(8)
            };

            Label lblName = new Label
            {
                Text = drugName,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 8)
            };
            card.Controls.Add(lblName);

            Label lblDetail = new Label
            {
                Text = detail,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(10, 30)
            };
            card.Controls.Add(lblDetail);

            Label lblShelf = new Label
            {
                Text = $"Raf: {shelfInfo}",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.DarkSlateGray,
                AutoSize = true,
                Location = new Point(10, 55)
            };
            card.Controls.Add(lblShelf);

            if (isCritical)
            {
                PictureBox warningIcon = new PictureBox
                {
                    Image = SystemIcons.Warning.ToBitmap(),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(20, 20),
                    Location = new Point(170, 5)
                };
                card.Controls.Add(warningIcon);
            }

            return card;
        }

        private void LoadDashboardData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    lblPharmacist.Text = "Eczacı DENİZ – Görevli Eczacı";

                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Prescriptions WHERE CAST(PrescriptionDate AS DATE) = CAST(GETDATE() AS DATE)", conn);
                    lblTotalPrescriptions.Text = cmd1.ExecuteScalar().ToString();

                    SqlCommand cmd2 = new SqlCommand(@"SELECT SUM(pd.Quantity) FROM PrescriptionDetails pd JOIN Prescriptions p ON pd.PrescriptionId = p.PrescriptionId WHERE CAST(p.PrescriptionDate AS DATE) = CAST(GETDATE() AS DATE)", conn);
                    var sales = cmd2.ExecuteScalar();
                    lblTotalSales.Text = (sales == DBNull.Value ? "0" : sales.ToString());

                    string rafFilter = cmbRafFilter?.SelectedItem?.ToString();
                    string filterCondition = "";
                    if (!string.IsNullOrEmpty(rafFilter) && rafFilter != "Tümü")
                        filterCondition = $"WHERE d.ShelfLocation LIKE '{rafFilter}%'";

                    SqlCommand cmd3 = new SqlCommand($@"SELECT TOP 5 d.Name, SUM(pd.Quantity) AS TotalSold, d.ShelfLocation 
                        FROM PrescriptionDetails pd 
                        JOIN Drugs d ON pd.DrugId = d.Id 
                        {filterCondition} 
                        GROUP BY d.Name, d.ShelfLocation 
                        ORDER BY d.ShelfLocation", conn);
                    SqlDataReader reader = cmd3.ExecuteReader();
                    flpTopDrugs.Controls.Clear();
                    while (reader.Read())
                    {
                        var card = CreateDrugCard(reader["Name"]?.ToString() ?? "", $"{reader["TotalSold"]} adet", false, reader["ShelfLocation"]?.ToString() ?? "");
                        flpTopDrugs.Controls.Add(card);
                    }
                    reader.Close();

                    SqlCommand cmd4 = new SqlCommand($@"SELECT d.Name, ds.StockQuantity, d.ShelfLocation 
                        FROM DrugStocks ds 
                        JOIN Drugs d ON ds.DrugId = d.Id 
                        {filterCondition} 
                        ORDER BY d.ShelfLocation", conn);
                    SqlDataReader reader2 = cmd4.ExecuteReader();
                    flpLowStock.Controls.Clear();
                    while (reader2.Read())
                    {
                        bool isCritical = Convert.ToInt32(reader2["StockQuantity"]) < 50;
                        string detail = $"Stok: {reader2["StockQuantity"]}";
                        var card = CreateDrugCard(reader2["Name"]?.ToString() ?? "", detail, isCritical, reader2["ShelfLocation"]?.ToString() ?? "");
                        flpLowStock.Controls.Add(card);
                    }
                    reader2.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeStyle() { }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
