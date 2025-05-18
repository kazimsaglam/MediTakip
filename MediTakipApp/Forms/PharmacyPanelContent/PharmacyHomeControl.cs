using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class PharmacyHomeControl : UserControl
    {
        private string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";

        public PharmacyHomeControl()
        {
            InitializeComponent();
            btnShowReport.Click += (s, e) =>
            {
                GenerateZReportMock(dtpReportDate.Value.Date);
            };
        }

        private void GenerateZReportMock(DateTime selectedDate)
        {
            string filename = $"ZRaporu_{selectedDate:yyyy-MM-dd_HH-mm-ss}_Mock.pdf";
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filepath, FileMode.Create));
                doc.Open();

                string fontPath = @"C:\Windows\Fonts\arial.ttf";
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var titleFont = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                var sectionFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                var rowFont = new iTextSharp.text.Font(baseFont, 11, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                // Başlık
                doc.Add(new Paragraph("📊 Günlük Z Raporu (Test Verisi)\n\n", titleFont) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph($"🗓️ Rapor Tarihi: {selectedDate:dd.MM.yyyy}\n\n", sectionFont));

                // Genel veriler (mock)
                int totalDrugs = 42;
                int lowStock = 6;
                int expiringSoon = 3;
                int dailyPrescriptions = 9;

                doc.Add(new Paragraph("📌 Genel İstatistikler", sectionFont));
                doc.Add(new Paragraph($"• Toplam İlaç Sayısı: {totalDrugs}", rowFont));
                doc.Add(new Paragraph($"• Düşük Stoklu Ürünler: {lowStock}", rowFont));
                doc.Add(new Paragraph($"• SKT Yaklaşan Ürünler: {expiringSoon}", rowFont));
                doc.Add(new Paragraph($"• Bugünkü Reçete Sayısı: {dailyPrescriptions}\n\n", rowFont));

                // Tedarik listesi
                var tedarikListesi = new List<(string IlacAdi, int Miktar, string Tedarikci, decimal Fiyat)>
        {
            ("Parol", 100, "EgeMed", 12.50m),
            ("Dolorex", 50, "İstanbul İlaç", 23.00m),
            ("Ventolin", 30, "Beta İlaç", 17.80m)
        };

                doc.Add(new Paragraph("🚚 Günlük Tedarik Listesi", sectionFont));
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 4f, 2f, 3f, 2f });

                string[] headers = { "İlaç Adı", "Miktar", "Tedarikçi", "Fiyat" };
                foreach (string h in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h, sectionFont)) { BackgroundColor = BaseColor.LIGHT_GRAY };
                    table.AddCell(cell);
                }

                int totalQty = 0;
                decimal totalCost = 0;

                foreach (var (ilac, miktar, tedarikci, fiyat) in tedarikListesi)
                {
                    table.AddCell(new Phrase(ilac, rowFont));
                    table.AddCell(new Phrase(miktar.ToString(), rowFont));
                    table.AddCell(new Phrase(tedarikci, rowFont));
                    table.AddCell(new Phrase($"{fiyat:C2}", rowFont));

                    totalQty += miktar;
                    totalCost += miktar * fiyat;
                }

                doc.Add(table);
                doc.Add(new Paragraph($"\n📦 Toplam Tedarik Miktarı: {totalQty} adet", sectionFont));
                doc.Add(new Paragraph($"💰 Toplam Maliyet: {totalCost:C2}", sectionFont));

                // Reçete listesi
                var receteler = new List<(string Hasta, string Ilac, int Adet, string Dozaj)>
        {
            ("Ayşe Demir", "Parol", 1, "Sabah-Akşam"),
            ("Ali Vural", "Dolorex", 2, "Yemekten sonra"),
            ("Fatma Çelik", "Ventolin", 1, "Günde 3 defa")
        };

                doc.Add(new Paragraph("\n📋 Günlük Reçete Detayları", sectionFont));

                PdfPTable receteTable = new PdfPTable(4);
                receteTable.WidthPercentage = 100;
                receteTable.SetWidths(new float[] { 3f, 4f, 1f, 3f });

                string[] receteHeaders = { "Hasta", "İlaç", "Adet", "Dozaj" };
                foreach (string h in receteHeaders)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h, sectionFont)) { BackgroundColor = BaseColor.LIGHT_GRAY };
                    receteTable.AddCell(cell);
                }

                foreach (var (hasta, ilac, adet, dozaj) in receteler)
                {
                    receteTable.AddCell(new Phrase(hasta, rowFont));
                    receteTable.AddCell(new Phrase(ilac, rowFont));
                    receteTable.AddCell(new Phrase(adet.ToString(), rowFont));
                    receteTable.AddCell(new Phrase(dozaj, rowFont));
                }

                doc.Add(receteTable);

                doc.Close();

                // Otomatik aç
                Process.Start(new ProcessStartInfo
                {
                    FileName = filepath,
                    UseShellExecute = true
                });

                MessageBox.Show("Z Raporu (Yapay Veri) başarıyla oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF oluşturulurken hata oluştu: " + ex.Message);
            }
        }


    }
}
