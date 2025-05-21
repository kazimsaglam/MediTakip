using iTextSharp.text;
using iTextSharp.text.pdf;
using MediTakipApp.Forms.PharmacyPanelContent;
using MediTakipApp.Utils;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;


namespace MediTakipApp.Forms
{
    public partial class PharmacyPanel : Form
    {
        string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";

        private readonly System.Drawing.Color menuBlue = System.Drawing.Color.FromArgb(25, 42, 86);
        private readonly System.Drawing.Color hoverBlue = System.Drawing.Color.FromArgb(52, 152, 219);
        private readonly System.Drawing.Color activeBlue = System.Drawing.Color.FromArgb(0, 122, 204);

        private Panel indicatorPanel;
        private Button activeButton;


        public PharmacyPanel()
        {
            InitializeComponent();
        }

        private void PharmacyPanel_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "💵 İlaç Satışı";
            LoadControl(new PharmacySellControl());
            lblPharmacistName.Text = LoggedUser.FullName;

            indicatorPanel = new Panel
            {
                Size = new Size(10, 70),
                BackColor = System.Drawing.Color.LimeGreen,
                Location = new Point(0, btnSell.Top),
                Visible = true
            };
            panelMenu.Controls.Add(indicatorPanel);
            indicatorPanel.BringToFront();

            SetHoverEffects(btnPrescriptions);
            SetHoverEffects(btnSell);
            SetHoverEffects(btnStockAndSupply);
            SetHoverEffects(btnZGenerate);

            SetActiveButton(btnSell);
        }

        private void btnZGenerate_Click(object sender, EventArgs e)
        {
            GenerateZReportDetailed(DateTime.Now);
            //GenerateZReportDetailedYapay(DateTime.Now);
        }

        private void btnPrescriptions_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "📜 Reçeteler";
            LoadControl(new PharmacyPrescriptionsControl());
            SetActiveButton(btnPrescriptions);
        }


        private void btnSell_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "💵 İlaç Satışı";
            LoadControl(new PharmacySellControl());
            SetActiveButton(btnSell);
        }


        private void btnStockAndSupply_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "💊 Stok Yönetimi";
            LoadControl(new PharmacyInventoryControl());
            SetActiveButton(btnStockAndSupply);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Uygulamayı kapatmak istiyor musunuz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void LoadControl(UserControl control)
        {
            control.Size = new Size((int)(panelMain.Width * 0.8), (int)(panelMain.Height * 0.8));
            control.Location = new Point(
                panelMain.Width / 2 - control.Width / 2,
                panelMain.Height / 2 - control.Height / 2
            );

            control.Visible = false;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);

            float scale = 0.8f;
            Timer timer = new Timer { Interval = 10 };
            timer.Tick += (s, args) =>
            {
                scale += 0.02f;
                int newWidth = (int)(panelMain.Width * scale);
                int newHeight = (int)(panelMain.Height * scale);

                control.Size = new Size(newWidth, newHeight);
                control.Location = new Point(
                    panelMain.Width / 2 - control.Width / 2,
                    panelMain.Height / 2 - control.Height / 2
                );

                if (scale >= 1f)
                {
                    control.Size = panelMain.Size;
                    control.Location = new Point(0, 0);
                    control.Visible = true;
                    timer.Stop();
                }
            };
            timer.Start();
        }

        private void SetActiveButton(Button button)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = menuBlue;
                activeButton.ForeColor = System.Drawing.Color.White;
            }

            activeButton = button;
            activeButton.BackColor = activeBlue;
            activeButton.ForeColor = System.Drawing.Color.White;

            indicatorPanel.Top = button.Top;
            indicatorPanel.BringToFront();
        }

        private void SetHoverEffects(Button button)
        {
            button.MouseEnter += (s, e) =>
            {
                if (button != activeButton)
                    button.BackColor = hoverBlue;
            };

            button.MouseLeave += (s, e) =>
            {
                if (button != activeButton)
                    button.BackColor = menuBlue;
            };
        }



        public class RoundedCell : IPdfPCellEvent
        {
            private readonly BaseColor _fillColor;
            private readonly float _radius;
            private readonly float _margin;

            public RoundedCell(BaseColor fillColor, float radius = 6f, float margin = 1f)
            {
                _fillColor = fillColor;
                _radius = radius;
                _margin = margin;
            }

            public void CellLayout(PdfPCell cell, iTextSharp.text.Rectangle rect, PdfContentByte[] canvases)
            {
                var canvas = canvases[PdfPTable.BACKGROUNDCANVAS];
                canvas.SaveState();
                canvas.SetColorFill(_fillColor);

                float x = rect.Left + _margin;
                float y = rect.Bottom + _margin;
                float w = rect.Width - 2 * _margin;
                float h = rect.Height - 2 * _margin;
                canvas.RoundRectangle(x, y, w, h, _radius);
                canvas.Fill();
                canvas.RestoreState();
            }
        }

        private void GenerateZReportDetailed(DateTime selectedDate)
        {
            string filename = $"ZRaporu_{selectedDate:yyyy-MM-dd}.pdf";
            string filepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                filename
            );
            var doc = new Document(PageSize.A4, 36, 36, 54, 36);

            try
            {
                // Watermark vb. ayarlar
                string logoPath = @"C:\Users\Kazim\OneDrive\Belgeler\MediTakipApp\Resources\Meditakip_Logo.png";
                var writer = PdfWriter.GetInstance(doc, new FileStream(filepath, FileMode.Create));
                writer.PageEvent = new PdfPageEvents(logoPath);
                doc.Open();

                // Fontlar
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string fontPath = Path.Combine(baseDir, "Resources", "DejaVuSansCondensed.ttf");
                BaseFont hf = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                var titleFont = new iTextSharp.text.Font(hf, 24, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));
                var sectionFont = new iTextSharp.text.Font(hf, 16, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));
                var tableHeader = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                var tableCell = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                var cardLabel = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));
                var cardValue = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));


                // --- ÜST BİLGİLER ---
                string logoPharmacyPath = @"C:\Users\Kazim\OneDrive\Belgeler\MediTakipApp\Resources\Eczane_Logo.png";

                // ➊ Kullanıcının Id’si
                int userId = LoggedUser.Id;

                // ➋ DB’den eczane bilgilerini çek (Artık OwnerUserId üzerinden)
                string pharmacyName = "", pharmacyAddr = "", pharmacyPhone = "", pharmacyEmail = "";
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(@"
                    SELECT Name, Address, Phone, Email
                    FROM Pharmacies
                    WHERE OwnerUserId = @userId", conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pharmacyName = reader.GetString(0);
                            pharmacyAddr = reader.GetString(1);
                            pharmacyPhone = reader.GetString(2);
                            pharmacyEmail = reader.GetString(3);
                        }
                    }
                }

                var headerTable = new PdfPTable(2)
                {
                    WidthPercentage = 60,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                headerTable.SetWidths(new float[] { 1.2f, 4f });

                // Logo
                if (File.Exists(logoPharmacyPath))
                {
                    var logo = iTextSharp.text.Image.GetInstance(logoPharmacyPath);
                    logo.ScaleToFit(80f, 80f);
                    var cellLogo = new PdfPCell(logo)
                    {
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        PaddingBottom = 5f,
                        PaddingRight = 10f
                    };
                    headerTable.AddCell(cellLogo);
                }
                else headerTable.AddCell(new PdfPCell(new Phrase("")) { Border = iTextSharp.text.Rectangle.NO_BORDER });

                // Eczane bilgisi
                var info = new Paragraph
                {
                    new Chunk(pharmacyName + "\n", new iTextSharp.text.Font(hf, 20, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130))),
                    new Chunk(pharmacyAddr + "\n", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(20, 20, 100))),
                    new Chunk(pharmacyPhone + "\n", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(20, 20, 100))),
                    new Chunk(pharmacyEmail + "\n\n", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(20, 20, 100)))
                };
                info.Alignment = Element.ALIGN_LEFT;
                var cellInfo = new PdfPCell(info)
                {
                    Border = iTextSharp.text.Rectangle.NO_BORDER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingLeft = 10f
                };
                headerTable.AddCell(cellInfo);
                doc.Add(headerTable);


                // --- Başlık ---
                var hdr = new Paragraph($"Günlük Z Raporu — {selectedDate:dd.MM.yyyy HH:mm}\n\n", titleFont)
                { Alignment = Element.ALIGN_CENTER };
                doc.Add(hdr);


                // ➊ Bugünün tarihi
                DateTime today = selectedDate.Date;

                // ➋ Veritabanından metrikleri çek
                int totalDrugs = 0;
                int dailyPrescriptions = 0, lowStock = 0, expiringSoon = 0, expiredCount = 0;
                decimal dailySalesAmount = 0m, dailySalesItems = 0m, dailySalesPresc = 0m;

                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // — Toplam İlaç sayısı
                    using (var cmdTotal = new SqlCommand("SELECT COUNT(*) FROM Drugs", conn))
                        totalDrugs = (int)cmdTotal.ExecuteScalar();

                    // a) Bugünkü reçete sayısı
                    using (var cmd = new SqlCommand(@"
        SELECT COUNT(*) 
        FROM Prescriptions 
        WHERE CAST(PrescriptionDate AS DATE) = @date", conn))
                    {
                        cmd.Parameters.AddWithValue("@date", today);
                        dailyPrescriptions = (int)cmd.ExecuteScalar();
                    }

                    // b) Stok miktarı düşük (adet<25) ürün sayısı
                    using (var cmd = new SqlCommand(@"
        SELECT COUNT(*) 
        FROM DrugStocks 
        WHERE StockQuantity < 25", conn))
                    {
                        lowStock = (int)cmd.ExecuteScalar();
                    }

                    // c) SKT 30 gün içinde (yaklaşan)
                    using (var cmd = new SqlCommand(@"
        SELECT COUNT(*) 
        FROM DrugStocks 
        WHERE DATEDIFF(day, GETDATE(), ExpirationDate) BETWEEN 0 AND 30", conn))
                    {
                        expiringSoon = (int)cmd.ExecuteScalar();
                    }

                    // d) SKT geçmiş ürünler
                    using (var cmd = new SqlCommand(@"
        SELECT COUNT(*) 
        FROM DrugStocks 
        WHERE ExpirationDate < GETDATE()", conn))
                    {
                        expiredCount = (int)cmd.ExecuteScalar();
                    }

                    // e) Günlük satış özetleri
                    using (var cmdSales = new SqlCommand(@"
        SELECT 
            ISNULL(TotalAmount,0), 
            ISNULL(TotalItems,0),
            ISNULL(PrescriptionCount,0)
        FROM DailySales
        WHERE SaleDate = @date", conn))
                    {
                        cmdSales.Parameters.AddWithValue("@date", today);
                        using (var rdr = cmdSales.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                dailySalesAmount = rdr.GetDecimal(0);
                                dailySalesItems = rdr.GetInt32(1);
                                dailySalesPresc = rdr.GetInt32(2);
                            }
                        }
                    }
                }

                // --- Bölüm 1: Özet Kartlar  ---
                int cardCount = 8;
                int columns = 4;

                // 1) PdfPTable’ı oluştur
                var cards = new PdfPTable(columns)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 10f,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                // Her sütuna eşit genişlik (1f)
                cards.SetWidths(Enumerable.Repeat(1f, columns).ToArray());

                // 2) Kart ekleme fonksiyonu
                void AddCard(string label, string value, BaseColor bg)
                {
                    var inner = new PdfPTable(1) { WidthPercentage = 80 };
                    inner.AddCell(new PdfPCell(new Phrase(label, cardLabel))
                    {
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        Padding = 5f,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });
                    inner.AddCell(new PdfPCell(new Phrase(value, cardValue))
                    {
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        Padding = 5f,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });

                    var outer = new PdfPCell(inner)
                    {
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        Padding = 4f,
                        CellEvent = new RoundedCell(bg, radius: 8f, margin: 6f)
                    };
                    cards.AddCell(outer);
                }

                // 3) Kartları ekle (“Toplam İlaç” en başta)
                AddCard("Toplam İlaç", totalDrugs.ToString(), new BaseColor(180, 225, 255));
                AddCard("Bugünkü Reçete", dailyPrescriptions.ToString(), new BaseColor(102, 178, 255));
                AddCard("Düşük Stok", lowStock.ToString(), new BaseColor(204, 229, 255));
                AddCard("SKT Yaklaşan", expiringSoon.ToString(), new BaseColor(153, 204, 255));
                AddCard("SKT Geçmiş", expiredCount.ToString(), new BaseColor(255, 200, 200));
                AddCard("Günlük Ciro", dailySalesAmount.ToString("C2"), new BaseColor(102, 255, 178));
                AddCard("Satış Adedi", dailySalesItems.ToString(), new BaseColor(204, 255, 229));
                AddCard("Reçeteli Satış", dailySalesPresc.ToString(), new BaseColor(153, 255, 204));

                // 4) Satır dolmadıysa boş hücre ekle
                int remainder = cardCount % columns;
                if (remainder != 0)
                {
                    for (int i = remainder; i < columns; i++)
                    {
                        cards.AddCell(new PdfPCell(new Phrase("", cardValue))
                        {
                            Border = iTextSharp.text.Rectangle.NO_BORDER,
                            BackgroundColor = BaseColor.WHITE
                        });
                    }
                }

                // 5) PDF’e ekle
                doc.Add(cards);


                // --- Bölüm 2A: Günlük Tedarik Listesi ---
                doc.Add(new Paragraph("Günlük Tedarik Listesi", sectionFont));

                var sup = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 6f,
                    SpacingAfter = 10f
                };
                sup.SetWidths(new float[] { 4f, 2f, 3f, 2f });

                string[] supHeaders = { "İlaç Adı", "Miktar", "Tedarikçi", "Birim Fiyat" };
                foreach (var h in supHeaders)
                    sup.AddCell(new PdfPCell(new Phrase(h, tableHeader))
                    {
                        BackgroundColor = new BaseColor(10, 70, 130),
                        Padding = 6f,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });

                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand(@"
        SELECT D.Name, S.StockQuantity, S.Supplier, D.Price
        FROM DrugStocks S
        JOIN Drugs      D ON S.DrugId = D.Id
        WHERE CONVERT(date, S.EntryDate) = @date
        ORDER BY D.Name", conn))
                    {
                        cmd.Parameters.AddWithValue("@date", selectedDate.Date);
                        using (var reader = cmd.ExecuteReader())
                        {
                            int row = 0;
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                int qty = reader.GetInt32(1);
                                string supplier = reader.GetString(2);
                                decimal price = reader.GetDecimal(3);

                                BaseColor bg = (row % 2 == 0)
                                    ? new BaseColor(245, 245, 245)
                                    : BaseColor.WHITE;

                                sup.AddCell(new PdfPCell(new Phrase(name, tableCell)) { BackgroundColor = bg, Padding = 4f });
                                sup.AddCell(new PdfPCell(new Phrase(qty.ToString(), tableCell))
                                {
                                    BackgroundColor = bg,
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Padding = 4f
                                });
                                sup.AddCell(new PdfPCell(new Phrase(supplier, tableCell)) { BackgroundColor = bg, Padding = 4f });
                                sup.AddCell(new PdfPCell(new Phrase($"{price:C2}", tableCell))
                                {
                                    BackgroundColor = bg,
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Padding = 4f
                                });

                                row++;
                            }
                        }
                    }

                    doc.Add(sup);

                    // --- Bölüm 2B: SKT Durum Tablosu ---
                    doc.Add(new Paragraph("SKT Durum Tablosu", sectionFont));

                    var exp = new PdfPTable(4)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 6f,
                        SpacingAfter = 10f
                    };
                    exp.SetWidths(new float[] { 3f, 2f, 3f, 2.5f });

                    string[] expHeaders = { "İlaç Adı", "Stok Miktarı", "SKT Kalan Süre", "Durum" };
                    foreach (var h in expHeaders)
                        exp.AddCell(new PdfPCell(new Phrase(h, tableHeader))
                        {
                            BackgroundColor = new BaseColor(10, 70, 130),
                            Padding = 6f,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                    using (var cmd2 = new SqlCommand(@"
    SELECT 
        D.Name, 
        S.StockQuantity, 
        DATEDIFF(day, GETDATE(), S.ExpirationDate) AS DaysLeft
    FROM DrugStocks S
    JOIN Drugs D ON S.DrugId = D.Id
    WHERE 
        DATEDIFF(day, GETDATE(), S.ExpirationDate) <= 30
        OR (S.StockQuantity <= 25 AND DATEDIFF(day, GETDATE(), S.ExpirationDate) >= 0)
    ORDER BY DaysLeft ASC", conn))
                    {
                        using (var reader2 = cmd2.ExecuteReader())
                        {
                            int row2 = 0;
                            while (reader2.Read())
                            {
                                string name = reader2.GetString(0);
                                int qty = reader2.GetInt32(1);
                                int daysLeft = reader2.GetInt32(2);

                                // --- SKT Kalan Süre metni ---
                                string daysText;
                                if (qty == 0)
                                {
                                    // Stok bitmişse SKT Kalan Süre yerine "-"
                                    daysText = "-------------------";
                                }
                                else if (daysLeft < 0)
                                {
                                    // SKT geçmiş partiler için: "X gün geçti"
                                    daysText = $"{Math.Abs(daysLeft)} gün geçti";
                                }
                                else if (daysLeft == 0)
                                {
                                    // Bugün son günü
                                    daysText = "Bugün son günü";
                                }
                                else
                                {
                                    // 1 günden 30’a kadar kalan gün sayısı
                                    daysText = $"{daysLeft} gün kaldı";
                                }

                                // --- Durum ve arkaplan rengi ---
                                string status;
                                BaseColor bg2;
                                if (daysLeft < 0)
                                {
                                    status = "SKT Geçti";
                                    bg2 = new BaseColor(255, 200, 200);
                                }
                                else if (qty == 0)
                                {
                                    status = "❌ Stok Bitmiş";
                                    bg2 = new BaseColor(255, 200, 200);
                                }
                                else if (qty <= 25)
                                {
                                    status = "⚠️ Stok Az";
                                    bg2 = new BaseColor(255, 245, 205);
                                }
                                else if (daysLeft == 0)
                                {
                                    status = "SON GÜN";
                                    bg2 = new BaseColor(255, 230, 200);
                                }
                                else
                                {
                                    status = "⏳ Yaklaşıyor";
                                    bg2 = new BaseColor(245, 245, 245);
                                }

                                exp.AddCell(new PdfPCell(new Phrase(name, tableCell)) { BackgroundColor = bg2, Padding = 4f });
                                exp.AddCell(new PdfPCell(new Phrase(qty.ToString(), tableCell))
                                {
                                    BackgroundColor = bg2,
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Padding = 4f
                                });
                                exp.AddCell(new PdfPCell(new Phrase(daysText, tableCell))
                                {
                                    BackgroundColor = bg2,
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Padding = 4f
                                });
                                exp.AddCell(new PdfPCell(new Phrase(status, tableCell))
                                {
                                    BackgroundColor = bg2,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    Padding = 4f
                                });

                                row2++;
                            }
                        }
                    }

                    doc.Add(exp);

                    // --- Bölüm 2C: Satılan İlaçlar ---
                    doc.Add(new Paragraph("Satilan Ilaçlar", sectionFont));

                    var soldTbl = new PdfPTable(4)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 6f,
                        SpacingAfter = 10f,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };
                    soldTbl.SetWidths(new float[] { 4f, 2f, 2f, 2f });

                    string[] soldHdr = { "İlaç Adı", "Adet", "Birim Fiyat", "Tutar" };
                    foreach (var h in soldHdr)
                        soldTbl.AddCell(new PdfPCell(new Phrase(h, tableHeader))
                        {
                            BackgroundColor = new BaseColor(10, 70, 130),
                            Padding = 6f,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                    // Toplamları biriktirecek değişkenler
                    int totalQty = 0;
                    decimal totalValue = 0m;

                    using (var cmd = new SqlCommand(@"
SELECT 
    D.Name,
    SUM(SD.Quantity) AS Quantity,
    AVG(SD.UnitPrice) AS UnitPrice,
    SUM(SD.Quantity * SD.UnitPrice) AS Total
FROM SaleDetails SD
JOIN Drugs D ON SD.DrugId = D.Id
WHERE CAST(SD.SaleDate AS DATE) = @date
GROUP BY D.Name
ORDER BY D.Name", conn))
                    {
                        cmd.Parameters.AddWithValue("@date", selectedDate.Date);

                        using (var reader = cmd.ExecuteReader())
                        {
                            int row = 0;
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                int qty = reader.GetInt32(1);
                                decimal price = reader.GetDecimal(2);
                                decimal tot = reader.GetDecimal(3);

                                // Toplamları güncelle
                                totalQty += qty;
                                totalValue += tot;

                                BaseColor bg = (row % 2 == 0)
                                    ? new BaseColor(245, 245, 245)
                                    : BaseColor.WHITE;

                                soldTbl.AddCell(new PdfPCell(new Phrase(name, tableCell)) { BackgroundColor = bg, Padding = 4f });
                                soldTbl.AddCell(new PdfPCell(new Phrase(qty.ToString(), tableCell))
                                {
                                    BackgroundColor = bg,
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Padding = 4f
                                });
                                soldTbl.AddCell(new PdfPCell(new Phrase($"{price:C2}", tableCell))
                                {
                                    BackgroundColor = bg,
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Padding = 4f
                                });
                                soldTbl.AddCell(new PdfPCell(new Phrase($"{tot:C2}", tableCell))
                                {
                                    BackgroundColor = bg,
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Padding = 4f
                                });

                                row++;
                            }
                        }
                    }


                    // --- Alt toplam satırı ---
                    var footerBg = new BaseColor(220, 235, 245);
                    soldTbl.AddCell(new PdfPCell(new Phrase("Toplam", tableHeader))
                    {
                        Colspan = 1,
                        BackgroundColor = footerBg,
                        Padding = 6f,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // Boş hücre (eğer istersen Quantity toplamını ayrı gösterebilirsin)
                    soldTbl.AddCell(new PdfPCell(new Phrase(totalQty.ToString(), tableHeader))
                    {
                        BackgroundColor = footerBg,
                        Padding = 6f,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    });
                    // İkinci boş hücre ya da birim fiyat toplamı gösterimsiz
                    soldTbl.AddCell(new PdfPCell(new Phrase("", tableHeader))
                    {
                        Colspan = 1,
                        BackgroundColor = footerBg,
                        Padding = 6f
                    });
                    soldTbl.AddCell(new PdfPCell(new Phrase($"{totalValue:C2}", tableHeader))
                    {
                        BackgroundColor = footerBg,
                        Padding = 6f,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    });

                    doc.Add(soldTbl);

                }


                // --- Bölüm 3: Gelişmiş Kural Tabanlı Öneri Sistemi ---
                doc.Add(new Paragraph("🤖 Öneri Sistemi", sectionFont) { SpacingAfter = 8f });

                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // ➊ Reçete Trendi: Dün vs Bugün
                    int yesterdayPresc = 0;
                    using (var cmd = new SqlCommand(@"
        SELECT COUNT(*) 
        FROM Prescriptions
        WHERE CAST(PrescriptionDate AS DATE) = DATEADD(day, -1, @date)", conn))
                    {
                        cmd.Parameters.AddWithValue("@date", selectedDate.Date);
                        yesterdayPresc = (int)cmd.ExecuteScalar();
                    }
                    double prescChange = yesterdayPresc > 0
                        ? (dailyPrescriptions - yesterdayPresc) * 100.0 / yesterdayPresc
                        : 0;

                    // ➋ Ortalama Satış Karşılaştırması(son 7 gün)
                                decimal avgDailySales = 0m;
                    using (var cmd = new SqlCommand(@"
                    SELECT AVG(CAST(TotalItems AS DECIMAL(18,2)))
                    FROM DailySales
                    WHERE SaleDate BETWEEN DATEADD(day, -7, @date) AND @date", conn))
                    {
                        cmd.Parameters.AddWithValue("@date", selectedDate.Date);
                        object result = cmd.ExecuteScalar();
                        avgDailySales = result != null
                            ? Convert.ToDecimal(result)
                            : 0m;
                    }

                    // dailySalesItems zaten decimal
                    bool aboveAvgSales = dailySalesItems > avgDailySales;
                    decimal diffSales = dailySalesItems - avgDailySales;


                    // ➌ Kritik Stok (<10), Tükenen (0)
                    var outOfStock = new List<string>();
                    var criticalStock = new List<string>();
                    using (var cmd = new SqlCommand(@"
                    SELECT D.Name, SUM(S.StockQuantity) AS Qty
                    FROM DrugStocks S
                    JOIN Drugs D ON S.DrugId = D.Id
                    GROUP BY D.Name", conn))
                    using (var rdr = cmd.ExecuteReader())
                        while (rdr.Read())
                        {
                            string name = rdr.GetString(0);
                            int qty = rdr.GetInt32(1);
                            if (qty == 0) outOfStock.Add(name);
                            else if (qty < 10) criticalStock.Add(name);
                        }

                    //            // ➍ SKT Yaklaşan (7 gün içinde) ve Geçmiş
                    var expiringSoonList = new List<string>();
                    var expired = new List<string>();
                    using (var cmd = new SqlCommand(@"
                    SELECT D.Name, DATEDIFF(day, GETDATE(), S.ExpirationDate) AS DaysLeft
                    FROM DrugStocks S
                    JOIN Drugs D ON S.DrugId = D.Id", conn))
                    using (var rdr = cmd.ExecuteReader())
                        while (rdr.Read())
                        {
                            string name = rdr.GetString(0);
                            int days = rdr.GetInt32(1);
                            if (days < 0) expired.Add(name);
                            else if (days <= 7) expiringSoonList.Add($"{name} ({days} gün)");
                        }

                    

                    // Şimdi tüm yorumları toplayıp, öncelik sırasına göre ekleyelim
                    void AddComment(string text, BaseColor bg)
                    {
                        var cell = new PdfPCell
                        {
                            Border = iTextSharp.text.Rectangle.BOX,
                            BackgroundColor = bg,
                            Padding = 8f
                        };
                        cell.AddElement(new Paragraph(text,
                            new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY)));
                        var tbl = new PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 4f };
                        tbl.AddCell(cell);
                        doc.Add(tbl);
                    }

                    // 1. Mutlak kritik: tükendi
                    if (outOfStock.Any())
                        AddComment($"🚨 Stok Tükenen İlaçlar: {string.Join(", ", outOfStock)}", new BaseColor(255, 200, 200));
                    //// 2. SKT geçmiş
                    if (expired.Any())
                        AddComment($"⛔ SKT Geçen İlaçlar: {string.Join(", ", expired)}", new BaseColor(255, 220, 200));
                    //// 3. Kritik stok
                    if (criticalStock.Any())
                        AddComment($"⚠️ Crit. Stoğu Az İlaçlar: {string.Join(", ", criticalStock)}", new BaseColor(255, 245, 205));
                    //// 4. SKT yaklaşan
                    if (expiringSoonList.Any())
                        AddComment($"⏳ SKT Yaklaşıyor: {string.Join(", ", expiringSoon)}", new BaseColor(245, 245, 245));
                    //// 6. Reçete trendi
                    AddComment($"📈 Reçete Trendi: {dailyPrescriptions} ({(prescChange >= 0 ? "▲" : "▼")}{Math.Abs(prescChange):0.#}% önceki güne göre)",
                               prescChange < 0 ? new BaseColor(255, 230, 200) : new BaseColor(230, 255, 230));
                    // 7. Günlük satış hızı
                    AddComment(
    $"💰 Günlük Satış: {dailySalesItems:0} adet " +
    $"({(diffSales >= 0 ? "▲" : "▼")}{Math.Abs(diffSales):0.#} ortalamaya göre)",
    aboveAvgSales
        ? new BaseColor(230, 255, 230)
        : new BaseColor(255, 230, 200)
    );


                } // conn kapanış


                doc.Close();
                Process.Start(new ProcessStartInfo { FileName = filepath, UseShellExecute = true });
                MessageBox.Show("Detaylı Z Raporu oluşturuldu!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF oluşturma hatası: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GenerateZReportDetailedYapay(DateTime selectedDate)
        {
            string filename = $"ZRaporu_{selectedDate:yyyy-MM-dd}.pdf";
            string filepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                filename
            );
            var doc = new Document(PageSize.A4, 36, 36, 54, 36);

            try
            {
                // Watermark vb. ayarlar
                string logoPath = @"C:\Users\Kazim\OneDrive\Belgeler\MediTakipApp\Resources\Meditakip_Logo.png";
                var writer = PdfWriter.GetInstance(doc, new FileStream(filepath, FileMode.Create));
                writer.PageEvent = new PdfPageEvents(logoPath);
                doc.Open();

                // Fontlar
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string fontPath = Path.Combine(baseDir, "Resources", "DejaVuSansCondensed.ttf");
                BaseFont hf = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                var titleFont = new iTextSharp.text.Font(hf, 24, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));
                var sectionFont = new iTextSharp.text.Font(hf, 16, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));
                var tableHeader = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                var tableCell = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                var cardLabel = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));
                var cardValue = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130));


                // --- ÜST BİLGİLER ---
                string logoPharmacyPath = @"C:\Users\Kazim\OneDrive\Belgeler\MediTakipApp\Resources\Eczane_Logo.png";

                // ➊ Kullanıcının Id’si
                int userId = LoggedUser.Id;

                // ➋ DB’den eczane bilgilerini çek (Artık OwnerUserId üzerinden)
                string pharmacyName = "", pharmacyAddr = "", pharmacyPhone = "", pharmacyEmail = "";
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(@"
                    SELECT Name, Address, Phone, Email
                    FROM Pharmacies
                    WHERE OwnerUserId = @userId", conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pharmacyName = reader.GetString(0);
                            pharmacyAddr = reader.GetString(1);
                            pharmacyPhone = reader.GetString(2);
                            pharmacyEmail = reader.GetString(3);
                        }
                    }
                }

                var headerTable = new PdfPTable(2)
                {
                    WidthPercentage = 60,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                headerTable.SetWidths(new float[] { 1.2f, 4f });

                // Logo
                if (File.Exists(logoPharmacyPath))
                {
                    var logo = iTextSharp.text.Image.GetInstance(logoPharmacyPath);
                    logo.ScaleToFit(80f, 80f);
                    var cellLogo = new PdfPCell(logo)
                    {
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        PaddingBottom = 5f,
                        PaddingRight = 10f
                    };
                    headerTable.AddCell(cellLogo);
                }
                else headerTable.AddCell(new PdfPCell(new Phrase("")) { Border = iTextSharp.text.Rectangle.NO_BORDER });

                // Eczane bilgisi
                var info = new Paragraph
                {
                    new Chunk(pharmacyName + "\n", new iTextSharp.text.Font(hf, 20, iTextSharp.text.Font.BOLD, new BaseColor(10, 70, 130))),
                    new Chunk(pharmacyAddr + "\n", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(20, 20, 100))),
                    new Chunk(pharmacyPhone + "\n", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(20, 20, 100))),
                    new Chunk(pharmacyEmail + "\n\n", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(20, 20, 100)))
                };
                info.Alignment = Element.ALIGN_LEFT;
                var cellInfo = new PdfPCell(info)
                {
                    Border = iTextSharp.text.Rectangle.NO_BORDER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingLeft = 10f
                };
                headerTable.AddCell(cellInfo);
                doc.Add(headerTable);


                // --- Başlık ---
                var hdr = new Paragraph($"Günlük Z Raporu — {selectedDate:dd.MM.yyyy HH:mm}\n\n", titleFont)
                { Alignment = Element.ALIGN_CENTER };
                doc.Add(hdr);


                // --- METRİKLER (mock) ---
                DateTime today = selectedDate.Date;
                int totalDrugs = 125;
                int dailyPrescriptions = 73;
                int lowStock = 4;
                int expiringSoon = 4;
                int expiredCount = 1;
                decimal dailySalesAmount = 8925.40m;
                decimal dailySalesItems = 215;
                decimal dailySalesPresc = dailyPrescriptions;

                // --- Bölüm 1: Özet Kartlar ---
                var cards = new PdfPTable(4) { WidthPercentage = 100, SpacingAfter = 10f, HorizontalAlignment = Element.ALIGN_CENTER };
                cards.SetWidths(Enumerable.Repeat(1f, 4).ToArray());
                void AddCard(string label, string value, BaseColor bg)
                {
                    var inner = new PdfPTable(1) { WidthPercentage = 80 };
                    inner.AddCell(new PdfPCell(new Phrase(label, cardLabel)) { Border = iTextSharp.text.Rectangle.NO_BORDER, Padding = 5f, HorizontalAlignment = Element.ALIGN_CENTER });
                    inner.AddCell(new PdfPCell(new Phrase(value, cardValue)) { Border = iTextSharp.text.Rectangle.NO_BORDER, Padding = 5f, HorizontalAlignment = Element.ALIGN_CENTER });
                    cards.AddCell(new PdfPCell(inner) { Border = iTextSharp.text.Rectangle.NO_BORDER, Padding = 4f, CellEvent = new RoundedCell(bg, 8f, 6f) });
                }
                AddCard("Toplam İlaç", totalDrugs.ToString(), new BaseColor(180, 225, 255));
                AddCard("Bugünkü Reçete", dailyPrescriptions.ToString(), new BaseColor(102, 178, 255));
                AddCard("Düşük Stok", lowStock.ToString(), new BaseColor(204, 229, 255));
                AddCard("SKT Yaklaşan", expiringSoon.ToString(), new BaseColor(153, 204, 255));
                AddCard("SKT Geçmiş", expiredCount.ToString(), new BaseColor(255, 200, 200));
                AddCard("Günlük Ciro", dailySalesAmount.ToString("C2"), new BaseColor(102, 255, 178));
                AddCard("Satış Adedi", dailySalesItems.ToString(), new BaseColor(204, 255, 229));
                AddCard("Reçeteli Satış", dailySalesPresc.ToString(), new BaseColor(153, 255, 204));
                doc.Add(cards);



                // --- Bölüm 2A: Günlük Tedarik Listesi (mock) ---
                doc.Add(new Paragraph("Günlük Tedarik Listesi", sectionFont));
                var sup = new PdfPTable(4) { WidthPercentage = 100, SpacingBefore = 6f, SpacingAfter = 10f };
                sup.SetWidths(new float[] { 4f, 2f, 3f, 2f });
                foreach (var h in new[] { "İlaç Adı", "Miktar", "Tedarikçi", "Birim Fiyat" })
                    sup.AddCell(new PdfPCell(new Phrase(h, tableHeader)) { BackgroundColor = new BaseColor(10, 70, 130), Padding = 6f, HorizontalAlignment = Element.ALIGN_CENTER });

                var mockSupply = new List<(string Name, int Qty, string Supplier, decimal Price)>()
        {
            ("Parol",      55, "EgeMed",      5.50m),
            ("Dolorex",    45, "AnadoluFarma", 8.75m),
            ("Profenid",   30, "MaviSağlık",    12.30m),
            ("Vitamin C",  125, "Egeİlaç",  3.20m),
            ("Omeprazol",  75, "BetaMed",  4.80m)
        };
                int rowSup = 0;
                foreach (var itm in mockSupply)
                {
                    BaseColor bg = (rowSup++ % 2 == 0) ? new BaseColor(245, 245, 245) : BaseColor.WHITE;
                    sup.AddCell(new PdfPCell(new Phrase(itm.Name, tableCell)) { BackgroundColor = bg, Padding = 4f });
                    sup.AddCell(new PdfPCell(new Phrase(itm.Qty.ToString(), tableCell)) { BackgroundColor = bg, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
                    sup.AddCell(new PdfPCell(new Phrase(itm.Supplier, tableCell)) { BackgroundColor = bg, Padding = 4f });
                    sup.AddCell(new PdfPCell(new Phrase($"{itm.Price:C2}", tableCell)) { BackgroundColor = bg, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
                }
                doc.Add(sup);

                // --- Bölüm 2B: SKT Durum Tablosu (mock) ---
                doc.Add(new Paragraph("SKT Durum Tablosu", sectionFont));
                var exp = new PdfPTable(4) { WidthPercentage = 100, SpacingBefore = 6f, SpacingAfter = 10f };
                exp.SetWidths(new float[] { 3f, 2f, 3f, 2.5f });
                foreach (var h in new[] { "İlaç Adı", "Stok Miktarı", "SKT Kalan Süre", "Durum" })
                    exp.AddCell(new PdfPCell(new Phrase(h, tableHeader)) { BackgroundColor = new BaseColor(10, 70, 130), Padding = 6f, HorizontalAlignment = Element.ALIGN_CENTER });

                var mockExpiry = new List<(string Name, int Qty, int DaysLeft)>()
        {
            ("Parol",     20, 15),
            ("Dolorex",   15,  5),
            ("Profenid",  30, -2),
            ("Vitamin C", 10,  0),
            ("Omeprazol", 25, 28)
        };
                foreach (var itm in mockExpiry)
                {
                    // gün metni
                    string daysText = itm.Qty == 0 ? "-------"
                        : itm.DaysLeft < 0 ? $"{Math.Abs(itm.DaysLeft)} gün geçti"
                        : itm.DaysLeft == 0 ? "Bugün son günü"
                        : $"{itm.DaysLeft} gün kaldı";
                    // durum rengi
                    string status;
                    BaseColor bg2;
                    if (itm.DaysLeft < 0) { status = "SKT Geçti"; bg2 = new BaseColor(255, 200, 200); }
                    else if (itm.Qty == 0) { status = "❌ Bitti"; bg2 = new BaseColor(255, 200, 200); }
                    else if (itm.Qty <= 25) { status = "⚠️ Az Stok"; bg2 = new BaseColor(255, 245, 205); }
                    else if (itm.DaysLeft == 0) { status = "SON GÜN"; bg2 = new BaseColor(255, 230, 200); }
                    else { status = "⏳ Yaklaşıyor"; bg2 = new BaseColor(245, 245, 245); }

                    exp.AddCell(new PdfPCell(new Phrase(itm.Name, tableCell)) { BackgroundColor = bg2, Padding = 4f });
                    exp.AddCell(new PdfPCell(new Phrase(itm.Qty.ToString(), tableCell)) { BackgroundColor = bg2, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
                    exp.AddCell(new PdfPCell(new Phrase(daysText, tableCell)) { BackgroundColor = bg2, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
                    exp.AddCell(new PdfPCell(new Phrase(status, tableCell)) { BackgroundColor = bg2, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4f });
                }
                doc.Add(exp);

                // --- Bölüm 2C: Satılan İlaçlar (mock) ---
                doc.Add(new Paragraph("Satılan İlaçlar", sectionFont));
                var soldTbl = new PdfPTable(4) { WidthPercentage = 100, SpacingBefore = 6f, SpacingAfter = 10f, HorizontalAlignment = Element.ALIGN_LEFT };
                soldTbl.SetWidths(new float[] { 4f, 2f, 2f, 2f });
                foreach (var h in new[] { "İlaç Adı", "Adet", "Birim Fiyat", "Tutar" })
                    soldTbl.AddCell(new PdfPCell(new Phrase(h, tableHeader)) { BackgroundColor = new BaseColor(10, 70, 130), Padding = 6f, HorizontalAlignment = Element.ALIGN_CENTER });

                var mockSold = new List<(string Name, int Qty, decimal UnitPrice)>()
        {
            ("Parol",    12, 5.50m),
            ("Dolorex",   8, 8.75m),
            ("Profenid", 15,12.30m),
            ("Vitamin C",10, 3.20m)
        };
                int totalQty = 0; decimal totalValue = 0m; int r = 0;
                foreach (var itm in mockSold)
                {
                    decimal tot = itm.Qty * itm.UnitPrice;
                    totalQty += itm.Qty;
                    totalValue += tot;
                    BaseColor bg = (r++ % 2 == 0) ? new BaseColor(245, 245, 245) : BaseColor.WHITE;
                    soldTbl.AddCell(new PdfPCell(new Phrase(itm.Name, tableCell)) { BackgroundColor = bg, Padding = 4f });
                    soldTbl.AddCell(new PdfPCell(new Phrase(itm.Qty.ToString(), tableCell)) { BackgroundColor = bg, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
                    soldTbl.AddCell(new PdfPCell(new Phrase($"{itm.UnitPrice:C2}", tableCell)) { BackgroundColor = bg, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
                    soldTbl.AddCell(new PdfPCell(new Phrase($"{tot:C2}", tableCell)) { BackgroundColor = bg, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
                }
                // footer
                var footerBg = new BaseColor(220, 235, 245);
                soldTbl.AddCell(new PdfPCell(new Phrase("Toplam", tableHeader)) { BackgroundColor = footerBg, Padding = 6f, HorizontalAlignment = Element.ALIGN_LEFT });
                soldTbl.AddCell(new PdfPCell(new Phrase(totalQty.ToString(), tableHeader)) { BackgroundColor = footerBg, Padding = 6f, HorizontalAlignment = Element.ALIGN_RIGHT });
                soldTbl.AddCell(new PdfPCell(new Phrase("", tableHeader)) { BackgroundColor = footerBg, Padding = 6f });
                soldTbl.AddCell(new PdfPCell(new Phrase($"{totalValue:C2}", tableHeader)) { BackgroundColor = footerBg, Padding = 6f, HorizontalAlignment = Element.ALIGN_RIGHT });
                doc.Add(soldTbl);


                // --- Bölüm 3: Öneri Sistemi (mock) ---
                doc.Add(new Paragraph("🤖 Öneri Sistemi", sectionFont) { SpacingAfter = 8f });

                int yesterdayPresc = dailyPrescriptions - 5;  // örnek
                double prescChange = yesterdayPresc > 0 ? (dailyPrescriptions - yesterdayPresc) * 100.0 / yesterdayPresc : 0;
                decimal avgDailySales = 180m; // örnek
                bool aboveAvgSales = dailySalesItems > avgDailySales;
                decimal diffSales = dailySalesItems - avgDailySales;

                var outOfStock = new List<string> { "Vitamin D" };
                var criticalStock = new List<string> { "Vitamin C" };
                var expired = new List<string> { "Profenid" };
                var expiringSoonList = new List<string> { "Dolorex (5 gün)", "Omeprazol (3 gün)" };

                void AddComment(string text, BaseColor bg)
                {
                    var cell = new PdfPCell { Border = iTextSharp.text.Rectangle.BOX, BackgroundColor = bg, Padding = 8f };
                    cell.AddElement(new Paragraph(text, new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY)));
                    var tbl = new PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 4f };
                    tbl.AddCell(cell);
                    doc.Add(tbl);
                }

                if (outOfStock.Any())
                    AddComment($"🚨 Stok Tükenen İlaçlar: {string.Join(", ", outOfStock)}", new BaseColor(255, 200, 200));
                if (expired.Any())
                    AddComment($"⛔ SKT Geçen İlaçlar: {string.Join(", ", expired)}", new BaseColor(255, 220, 200));
                if (criticalStock.Any())
                    AddComment($"⚠️ Kritik Stok: {string.Join(", ", criticalStock)}", new BaseColor(255, 245, 205));
                if (expiringSoonList.Any())
                    AddComment($"⏳ SKT Yaklaşıyor: {string.Join(", ", expiringSoonList)}", new BaseColor(245, 245, 245));

                AddComment($"📈 Reçete Trendi: {dailyPrescriptions} ({(prescChange >= 0 ? "▲" : "▼")}{Math.Abs(prescChange):0.#}% önceki güne göre)",
                           prescChange < 0 ? new BaseColor(255, 230, 200) : new BaseColor(230, 255, 230));
                AddComment($"💰 Günlük Satış: {dailySalesItems:0} adet ({(diffSales >= 0 ? "▲" : "▼")}{Math.Abs(diffSales):0.#} ortalamaya göre)",
                           aboveAvgSales ? new BaseColor(230, 255, 230) : new BaseColor(255, 230, 200));

                doc.Close();
                Process.Start(filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}");
            }
        }



        public class PdfPageEvents : PdfPageEventHelper
        {
            private readonly iTextSharp.text.Image _watermark;
            private readonly PdfGState _gstate = new PdfGState { FillOpacity = 0.2f, StrokeOpacity = 0.1f };

            public PdfPageEvents(string logoPath)
            {
                if (File.Exists(logoPath))
                {
                    _watermark = iTextSharp.text.Image.GetInstance(logoPath);
                    _watermark.ScaleToFit(400f, 400f);
                }
                else
                {
                    _watermark = null;
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                if (_watermark != null)
                {
                    var under = writer.DirectContentUnder;
                    under.SaveState();
                    under.SetGState(_gstate);
                    float x = (document.PageSize.Width - _watermark.ScaledWidth) / 2;
                    float y = (document.PageSize.Height - _watermark.ScaledHeight) / 2;
                    _watermark.SetAbsolutePosition(x, y);
                    under.AddImage(_watermark);
                    under.RestoreState();
                }

                // Sayfa numarası
                var cb = writer.DirectContent;
                cb.BeginText();
                var bf = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 9);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                    $"- Sayfa {writer.PageNumber} -",
                    document.Right - 36,
                    document.Bottom - 20, 0);
                cb.EndText();
            }
        }

    }
}
