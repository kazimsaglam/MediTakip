using System.Data;
using System.Diagnostics;
using System.Text;
using MediTakipApp.Utils;
using Timer = System.Windows.Forms.Timer;

namespace MediTakipApp.Forms.DoctorPanelContent
{
    public partial class DrugsControl : UserControl
    {
        private Dictionary<Panel, Panel> drugsDetailMap = new Dictionary<Panel, Panel>();
        private Dictionary<Panel, Panel> prescriptionDetailMap = new Dictionary<Panel, Panel>();
        private List<PrescribedDrug> selectedDrugs = new List<PrescribedDrug>();
        private List<DrugDto> allDrugs = new();
        private HashSet<string> recommendedDrugNames = new HashSet<string>();
        private Timer detailHideTimer;
        private List<PrescriptionSummaryDto> prescriptionCache = new();



        public DrugsControl()
        {
            InitializeComponent();

            detailHideTimer = new Timer { Interval = 100 };
            detailHideTimer.Tick += (s, e) =>
            {
                if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition)))
                {
                    HideAllPrescriptionDetails();
                }
            };
            detailHideTimer.Start();
        }

        private async void DrugsControl_Load(object sender, EventArgs e)
        {
            if (SelectedPatient.Id == 0)
            {
                lblPatientInfo.Text = "👤 Seçilen Hasta: Yok";
                return;
            }

            lblPatientInfo.Text = $"Seçilen Hasta: {SelectedPatient.FullName} ({GetAge(SelectedPatient.BirthDate)} yaşında)";

            await LoadDrugsAsync();
            LoadPatientPrescriptions();
        }

        private async Task LoadDrugsAsync()
        {
            try
            {
                allDrugs = await ApiService.GetListAsync<DrugDto>("drug/list");

                flpDrugs.Controls.Clear();

                foreach (var drug in allDrugs)
                {
                    flpDrugs.Controls.Add(CreateDrugCard(drug));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HTTP ilaç yükleme hatası: " + ex.Message);
            }
        }

        private Panel CreateDrugCard(DrugDto drug)
        {
            Panel card = new Panel
            {
                Width = 300,
                Height = 200,
                BackColor = Color.White,
                Margin = new Padding(10, 10, 10, 30),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = drug,
                Cursor = Cursors.Hand
            };

            Panel detailPanel = new Panel
            {
                Width = 250,
                Height = 100,
                BackColor = Color.LightYellow,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                AutoScroll = true
            };

            Controls.Add(detailPanel);
            detailPanel.BringToFront();
            drugsDetailMap[card] = detailPanel;

            // Kart içeriği
            card.Controls.Add(new TransparentLabel { Text = $"💊 {drug.Name}", Font = new Font("Bahnschrift SemiCondensed", 14, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🔬 Etken Madde: {drug.ActiveIngredient}", Location = new Point(10, 40), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"👶 Kullanım Yaşı: {drug.UsageAge}+", Location = new Point(10, 60), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🏷️ Barkod: {drug.Barcode}", Location = new Point(10, 80), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"📦 Stok: {drug.TotalStock} adet", Location = new Point(10, 100), AutoSize = true });

            string prescriptionInfo = drug.IsPrescription ? "Reçeteli" : "Reçetesiz";
            Color presColor = drug.IsPrescription ? Color.Red : Color.DarkGreen;
            card.Controls.Add(new TransparentLabel { Text = $"🩺 {prescriptionInfo}", Location = new Point(10, 140), AutoSize = true, ForeColor = presColor });


            // ⚠️ Düşük Stok Uyarıları
            if (drug.TotalStock < 50)
            {
                card.Controls.Add(new TransparentLabel
                {
                    Text = "⚠️ Stok az!",
                    Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                    ForeColor = Color.DarkOrange,
                    Location = new Point(10, 165),
                    AutoSize = true
                });
            }

            if (recommendedDrugNames.Contains(drug.Name))
            {
                card.BackColor = Color.LightGoldenrodYellow;

                Label lblStar = new Label
                {
                    Text = "⭐️ Önerilen",
                    Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold),
                    ForeColor = Color.DarkOrange,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Location = new Point(160, 160)
                };

                card.Controls.Add(lblStar);
            }

            // Eventler
            card.Click += (s, e) => OnDrugCardClick(drug);
            card.MouseEnter += (s, e) => { card.BackColor = Color.LightGray; ShowDescriptionDetail(card); };
            card.MouseLeave += (s, e) => { card.BackColor = Color.White; HideDescriptionDetail(card); };

            return card;
        }

        private void ShowDescriptionDetail(Panel card)
        {
            if (drugsDetailMap.TryGetValue(card, out Panel detailPanel) && card.Tag is DrugDto drug)
            {
                detailPanel.Controls.Clear();

                Label lblDescription = new Label
                {
                    Text = string.IsNullOrWhiteSpace(drug.Description)
                           ? "🏷️ Açıklama bulunamadı."
                           : $"🏷️ {drug.Description}",
                    Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Regular),
                    ForeColor = Color.Black,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    AutoSize = false
                };

                detailPanel.Controls.Add(lblDescription);

                // Konum
                Point cardLocation = card.PointToScreen(Point.Empty);
                Point relativeLocation = this.PointToClient(cardLocation);
                int x = relativeLocation.X + card.Width + 5;
                if (x + detailPanel.Width > this.Width)
                    x = relativeLocation.X - detailPanel.Width - 5;

                detailPanel.Location = new Point(x, relativeLocation.Y);
                detailPanel.Visible = true;
                detailPanel.BringToFront();
            }
        }

        private void HideDescriptionDetail(Panel card)
        {
            if (drugsDetailMap.TryGetValue(card, out Panel detailPanel))
            {
                detailPanel.Visible = false;
            }
        }

        private void OnDrugCardClick(DrugDto drug)
        {
            if (selectedDrugs.Any(d => d.DrugId == drug.Id))
            {
                MessageBox.Show($"{drug.Name} zaten reçeteye eklenmiş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (DrugDetailForm form = new DrugDetailForm(drug.Name))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    selectedDrugs.Add(new PrescribedDrug
                    {
                        DrugId = drug.Id,
                        DrugName = drug.Name,
                        Quantity = form.Quantity,
                        Instructions = form.Dosage,
                        UsagePeriod = form.UsagePeriod,
                        SpecialInstructions = form.SpecialInstructions,
                        Price = drug.Price
                    });

                    RefreshSelectedDrugList();
                }
            }
        }

        private void RefreshSelectedDrugList()
        {
            flpSelectedDrugs.Controls.Clear();

            foreach (var drug in selectedDrugs)
            {
                Panel panel = new Panel
                {
                    Height = 80,
                    Width = flpSelectedDrugs.Width - 30,
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = drug
                };

                // İlaç adı ve miktarı
                Label lblName = new Label
                {
                    Text = $"💊 {drug.DrugName} x{drug.Quantity}",
                    Font = new Font("Bahnschrift SemiCondensed", 13F, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue,
                    Location = new Point(15, 10),
                    AutoSize = true
                };

                // Talimatlar
                Label lblDetails = new Label
                {
                    Text = $"📖 {drug.Instructions} | ⏳ {drug.UsagePeriod}",
                    Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Regular),
                    ForeColor = Color.DimGray,
                    Location = new Point(15, 40),
                    AutoSize = true
                };

                // Kaldır butonu
                Button btnRemove = new Button
                {
                    Text = "❌",
                    Size = new Size(30, 30),
                    Location = new Point(panel.Width - 40, 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.IndianRed,
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand
                };
                btnRemove.FlatAppearance.BorderSize = 0;
                btnRemove.Click += (s, e) =>
                {
                    if (drug.Quantity > 1)
                        drug.Quantity--;
                    else
                        selectedDrugs.Remove(drug);

                    RefreshSelectedDrugList();
                };

                panel.Controls.Add(lblName);
                panel.Controls.Add(lblDetails);
                panel.Controls.Add(btnRemove);
                flpSelectedDrugs.Controls.Add(panel);
            }
        }

        private async void BtnSavePrescription_Click(object sender, EventArgs e)
        {
            if (selectedDrugs.Count == 0 || SelectedPatient.Id == 0 || string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Lütfen hasta, teşhis ve ilaç seçimi yapınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var requestBody = new
            {
                PatientId = SelectedPatient.Id.ToString(), // API bunu string alıyor olabilir
                DoctorId = LoggedUser.Id.ToString(),       // aynı şekilde
                Diagnosis = txtDiagnosis.Text.Trim(),
                Drugs = selectedDrugs.Select(drug => new
                {
                    DrugId = drug.DrugId,
                    Dosage = drug.Instructions,
                    UsagePeriod = drug.UsagePeriod,
                    SpecialInstructions = string.IsNullOrWhiteSpace(drug.SpecialInstructions) ? "" : drug.SpecialInstructions,
                    Quantity = drug.Quantity
                }).ToList()
            };

            // Log amaçlı (dilersen bakabilirsin)
            string previewJson = System.Text.Json.JsonSerializer.Serialize(requestBody);
            Debug.WriteLine("Giden JSON:\n" + previewJson);

            var result = await ApiService.PostAsync<ApiResult<string>>("prescription/create", requestBody);

            if (result == null || !result.Success)
            {
                MessageBox.Show("❌ Reçete kaydedilemedi.\n" + (result?.Message ?? "Sunucu hatası"), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("✅ Reçete başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            selectedDrugs.Clear();
            txtDiagnosis.Clear();
            RefreshSelectedDrugList();
            await LoadPatientPrescriptions();
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            flpDrugs.Controls.Clear();

            foreach (var drug in allDrugs)
            {
                if (drug.Name.ToLower().Contains(keyword) || drug.Barcode.ToLower().Contains(keyword))
                {
                    flpDrugs.Controls.Add(CreateDrugCard(drug));
                }
            }
        }

        private async Task LoadPatientPrescriptions()
        {
            if (SelectedPatient.Id == 0)
                return;

            flpHistory.Controls.Clear();

            try
            {
                var result = await ApiService.GetAsync<ApiResult<List<PrescriptionSummaryDto>>>("prescription/list");
                var allPrescriptions = result?.Data ?? new List<PrescriptionSummaryDto>();

                // Hasta ID'sine göre filtrele
                var prescriptions = allPrescriptions
                    .Where(p => p.PatientId == SelectedPatient.Id)
                    .OrderByDescending(p => p.PrescriptionDate)
                    .ToList();


                foreach (var pres in prescriptions)
                {
                    int cardHeight = 150;

                    Panel card = new Panel
                    {
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(10),
                        Padding = new Padding(10),
                        Width = flpHistory.Width - 25,
                        Height = cardHeight,
                        Cursor = Cursors.Hand,
                        Tag = pres.PrescriptionId
                    };

                    Panel detailPanel = new Panel
                    {
                        Width = 350,
                        Height = 300,
                        BackColor = Color.LightCyan,
                        BorderStyle = BorderStyle.FixedSingle,
                        Visible = false,
                        AutoScroll = true
                    };
                    this.Controls.Add(detailPanel);
                    detailPanel.BringToFront();
                    prescriptionDetailMap[card] = detailPanel;

                    // Kart içerikleri
                    card.Controls.Add(new TransparentLabel
                    {
                        Text = $"📅 {pres.PrescriptionDate:dd.MM.yyyy HH:mm}",
                        Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                        ForeColor = Color.DarkSlateGray,
                        Location = new Point(10, 10),
                        AutoSize = true
                    });

                    card.Controls.Add(new TransparentLabel
                    {
                        Text = $"📝 {pres.Diagnosis}",
                        Font = new Font("Bahnschrift SemiCondensed", 12F),
                        Location = new Point(10, 35),
                        AutoSize = true
                    });

                    card.Controls.Add(new TransparentLabel
                    {
                        Text = $"💊 {pres.DrugCount} ilaç",
                        Font = new Font("Bahnschrift SemiCondensed", 12F),
                        ForeColor = Color.DarkSlateGray,
                        Location = new Point(10, 60),
                        AutoSize = true
                    });

                    Button btnAddWholePrescription = new Button
                    {
                        Text = "↩ Bu Reçeteyi Kopyala",
                        Width = card.Width - 20,
                        Height = 35,
                        Location = new Point(10, cardHeight - 45),
                        BackColor = Color.MediumSlateBlue,
                        ForeColor = Color.White,
                        Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Tag = pres.PrescriptionCode
                    };
                    btnAddWholePrescription.FlatAppearance.BorderSize = 0;

                    btnAddWholePrescription.Click += async (s, e) =>
                    {
                        if (selectedDrugs.Count > 0)
                        {
                            MessageBox.Show("Önceki ilaçları temizlemeden bu reçeteyi ekleyemezsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var response = await ApiService.GetAsync<ApiResult<List<PrescriptionDrugDetailDto>>>($"prescription/list/{pres.PrescriptionCode}");
                        if (response == null || !response.Success || response.Data == null)
                        {
                            MessageBox.Show("Reçete detayları alınamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        foreach (var d in response.Data)
                        {
                            selectedDrugs.Add(new PrescribedDrug
                            {
                                DrugId = d.DrugId,
                                DrugName = d.Name,
                                Quantity = d.Quantity,
                                Instructions = d.Dosage,
                                UsagePeriod = d.UsagePeriod
                            });
                        }

                        txtDiagnosis.Text = pres.Diagnosis;
                        RefreshSelectedDrugList();
                        MessageBox.Show("Reçetedeki ilaçlar başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };

                    card.Controls.Add(btnAddWholePrescription);

                    card.MouseEnter += (s, e) =>
                    {
                        card.BackColor = Color.AliceBlue;
                        ShowPrescriptionDetail(card);
                    };

                    card.MouseLeave += (s, e) =>
                    {
                        card.BackColor = Color.White;
                        HidePrescriptionDetail(card);
                    };

                    flpHistory.Controls.Add(card);
                    prescriptionCache = prescriptions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HTTP reçete yükleme hatası: " + ex.Message);
            }
        }

        private async void ShowPrescriptionDetail(Panel card)
        {
            if (!prescriptionDetailMap.TryGetValue(card, out Panel detailPanel) || card.Tag is not int prescriptionId)
                return;

            detailPanel.Controls.Clear();
            detailPanel.AutoScroll = true;
            detailPanel.BackColor = Color.White;
            detailPanel.BorderStyle = BorderStyle.FixedSingle;
            detailPanel.Padding = new Padding(5);

            // Prescription listesinden ilgili reçeteyi bul
            var selectedPrescription = prescriptionCache.FirstOrDefault(p => p.PrescriptionId == prescriptionId);

            if (selectedPrescription == null) return;

            var detailResult = await ApiService.GetAsync<ApiResult<List<PrescriptionDrugDetailDto>>>($"prescription/list/{selectedPrescription.PrescriptionCode}");
            var details = detailResult?.Data ?? new List<PrescriptionDrugDetailDto>();
            if (details == null) return;

            // Header Panel
            Panel headerPanel = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Lavender,
                Dock = DockStyle.Top,
                Padding = new Padding(8),
                Margin = new Padding(0, 0, 0, 10)
            };

            headerPanel.Controls.Add(new Label
            {
                Text = $"📝 Teşhis: {selectedPrescription.Diagnosis}",
                Font = new Font("Bahnschrift SemiCondensed", 12F),
                Dock = DockStyle.Top,
                Height = 25
            });

            headerPanel.Controls.Add(new Label
            {
                Text = $"📅 Tarih: {selectedPrescription.PrescriptionDate:dd.MM.yyyy}",
                Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Italic),
                Dock = DockStyle.Top,
                Height = 20
            });

            headerPanel.Controls.Add(new Label
            {
                Text = $"📋 Reçete Kodu: {selectedPrescription.PrescriptionCode}",
                Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 25
            });

            detailPanel.Controls.Add(headerPanel);

            // Detayları listele
            foreach (var drug in details)
            {
                Panel drugPanel = new Panel
                {
                    Height = 80,
                    Dock = DockStyle.Top,
                    Padding = new Padding(8),
                    BackColor = Color.WhiteSmoke,
                    Margin = new Padding(0, 0, 0, 10),
                    BorderStyle = BorderStyle.FixedSingle
                };

                drugPanel.Controls.Add(new Label
                {
                    Text = $"💊 {drug.Name} x{drug.Quantity}",
                    Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 25
                });

                drugPanel.Controls.Add(new Label
                {
                    Text = $"📖 Doz: {drug.Dosage}",
                    Font = new Font("Bahnschrift SemiCondensed", 10F),
                    Dock = DockStyle.Top,
                    Height = 20
                });

                drugPanel.Controls.Add(new Label
                {
                    Text = $"⏳ Süre: {drug.UsagePeriod}",
                    Font = new Font("Bahnschrift SemiCondensed", 10F),
                    Dock = DockStyle.Top,
                    Height = 20
                });

                // Ekle butonu
                Button btnAddAgain = new Button
                {
                    Text = "↩ Ekle",
                    Width = 60,
                    Height = 25,
                    BackColor = Color.LightBlue,
                    ForeColor = Color.Black,
                    Font = new Font("Bahnschrift SemiCondensed", 9F, FontStyle.Bold),
                    Location = new Point(drugPanel.Width - 70, 10)
                };

                string drugName = drug.Name;
                int quantity = drug.Quantity;
                string dosage = drug.Dosage;
                string usage = drug.UsagePeriod;

                btnAddAgain.Click += (s, e) =>
                {
                    selectedDrugs.Add(new PrescribedDrug
                    {
                        DrugId = drug.DrugId,
                        DrugName = drugName,
                        Quantity = quantity,
                        Instructions = dosage,
                        UsagePeriod = usage
                    });
                    RefreshSelectedDrugList();
                };

                drugPanel.Controls.Add(btnAddAgain);
                detailPanel.Controls.Add(drugPanel);
            }

            // Konum ve yükseklik ayarla
            int totalHeight = detailPanel.Controls.Cast<Control>().Sum(c => c.Height + c.Margin.Top + c.Margin.Bottom) + 20;
            int maxHeight = (int)(this.Height * 0.75);
            detailPanel.Height = Math.Min(totalHeight, maxHeight);

            Point cardLocation = card.PointToScreen(Point.Empty);
            Point relativeLocation = this.PointToClient(cardLocation);

            int x = relativeLocation.X + card.Width + 5;
            if (x + detailPanel.Width > this.Width)
                x = relativeLocation.X - detailPanel.Width - 5;

            int y = Math.Max(relativeLocation.Y, 0);
            if (y + detailPanel.Height > this.Height)
                y = this.Height - detailPanel.Height - 10;

            detailPanel.Location = new Point(x, y);
            detailPanel.Visible = true;
            detailPanel.BringToFront();
        }

        private void HidePrescriptionDetail(Panel card)
        {
            if (prescriptionDetailMap.TryGetValue(card, out Panel detailPanel))
            {
                detailPanel.Visible = false;
            }
        }

        private void HideAllPrescriptionDetails()
        {
            foreach (var panel in prescriptionDetailMap.Values)
            {
                panel.Visible = false;
            }
        }


        private int GetAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear) age--;
            return age;
        }

        private async void BtnRecommend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Lütfen önce bir teşhis giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int age = GetAge(SelectedPatient.BirthDate);

            var drugsList = allDrugs.Select(drug => new
            {
                drug.Name,
                drug.UsageAge,
                drug.ActiveIngredient
            }).ToList();

            var requestBody = new
            {
                diagnosis = txtDiagnosis.Text,
                age = age,
                drugs = drugsList
            };

            btnRecommend.Enabled = false;
            btnRecommend.Text = "⏳ Öneriler alınıyor...";

            try
            {
                var response = await ApiService.PostAsync<ApiResult<string>>("drug/recommend", requestBody);

                if (response == null || !response.Success || string.IsNullOrWhiteSpace(response.Data))
                {
                    MessageBox.Show("⚠️ API geçerli bir cevap döndürmedi.");
                    return;
                }

                // Kod bloğu markdown'ını temizle: ```json\n[...]\n```
                string rawJson = response.Data
                    .Replace("```json", "")
                    .Replace("```", "")
                    .Trim();

                Debug.WriteLine("Temizlenmiş JSON: " + rawJson);

                List<string> suggestions;
                try
                {
                    suggestions = System.Text.Json.JsonSerializer.Deserialize<List<string>>(rawJson);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("⚠️ AI cevabı geçersiz formatta: " + ex.Message);
                    return;
                }

                if (suggestions == null || suggestions.Count == 0)
                {
                    MessageBox.Show("⚠️ Model geçerli öneri döndürmedi.");
                    return;
                }

                recommendedDrugNames = new HashSet<string>(suggestions);

                var sortedDrugs = allDrugs
                    .OrderByDescending(d => recommendedDrugNames.Contains(d.Name))
                    .ToList();

                flpDrugs.Controls.Clear();
                foreach (var drug in sortedDrugs)
                    flpDrugs.Controls.Add(CreateDrugCard(drug));

                MessageBox.Show("✅ Öneriler alındı ve vurgulandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRecommend.Enabled = true;
                btnRecommend.Text = "🔮 İlaç Öner";
            }
        }

    }

    public class PrescribedDrug
    {
        public int DrugId { get; set; }
        public required string DrugName { get; set; }
        public int Quantity { get; set; } = 1;
        public string UsagePeriod { get; set; } = "7 gün";
        public string Instructions { get; set; } = "Günde 2 kez";
        public string? SpecialInstructions { get; set; }
        public decimal Price { get; set; }
    }
}
