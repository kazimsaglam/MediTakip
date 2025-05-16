using System;
using System.Data;
using MediTakipApp.Utils;
using Timer = System.Windows.Forms.Timer;

namespace MediTakipApp.Forms
{
    public partial class HomeControl : UserControl
    {
        //private string connStr = @"Server=202.61.227.225;Database=metidata;Trusted_Connection=True;TrustServerCertificate=True;";
        private List<PatientDto> allPatients = new();
        private Dictionary<Panel, Panel> cardDetailMap = new();
        private Panel? selectedPatientCard = null;
        private Label? currentToast = null;
        private Timer detailHideTimer = new();

        // Sayfalama
        private int currentPage = 1;
        private int itemsPerPage = 20;
        private int totalPages = 1;
        private int selectedPatientIdForDetails = -1;



        public HomeControl()
        {
            InitializeComponent();
            InitializeDetailPanel();
            InitializeTimer();
        }

        private void InitializeDetailPanel()
        {
            foreach (var panel in cardDetailMap.Values)
            {
                panel.Dispose();
            }
            cardDetailMap.Clear();
        }

        private void InitializeTimer()
        {
            if (detailHideTimer == null)
            {
                detailHideTimer = new Timer { Interval = 100 };
                detailHideTimer.Tick += (s, e) =>
                {
                    if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition)))
                    {
                        HideAllDetails();
                    }
                };
                detailHideTimer.Start();
            }
        }

        private void HomeControl_Load(object sender, EventArgs e)
        {
            CreateDashboardCard(cardPatient, lblPatientTitle, lblPatientCount, "Toplam Hastalar", Color.MediumSeaGreen);
            CreateDashboardCard(cardPrescription, lblPrescriptionTitle, lblPrescriptionCount, "Toplam Reçeteler", Color.SteelBlue);
            CreateDashboardCard(cardDrug, lblDrugTitle, lblDrugCount, "Toplam İlaçlar", Color.DarkOrange);

            LoadPatients();
            UpdateDashboardCounts();

            AddDashboardHoverEffects(cardPatient);
            AddDashboardHoverEffects(cardPrescription);
            AddDashboardHoverEffects(cardDrug);

            ResetSelectedPatient();
        }

        private async void LoadPatients()
        {
            ResetSelectedPatient();

            foreach (var panel in cardDetailMap.Values)
                if (!panel.IsDisposed) panel.Dispose();
            cardDetailMap.Clear();

            try
            {
                var response = await ApiService.GetAsync<ApiResult<List<PatientDto>>>("doctor/patient/list");

                if (response == null || !response.Success || response.Data == null)
                {
                    MessageBox.Show("Hasta listesi alınamadı.");
                    return;
                }

                allPatients = response.Data;

                int totalRecords = allPatients.Count;
                totalPages = (int)Math.Ceiling(totalRecords / (double)itemsPerPage);
                int offset = (currentPage - 1) * itemsPerPage;

                var paginated = allPatients.Skip(offset).Take(itemsPerPage).ToList();

                flpPatients.SuspendLayout();
                flpPatients.Controls.Clear();

                if (paginated.Count == 0)
                {
                    flpPatients.Controls.Add(new Label
                    {
                        Text = "Hasta bulunamadı.",
                        Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Padding = new Padding(10),
                        Margin = new Padding(10)
                    });
                }
                else
                {
                    foreach (var patient in paginated)
                    {
                        var card = CreatePatientCard(patient);
                        flpPatients.Controls.Add(card);
                    }
                }

                flpPatients.ResumeLayout();
                UpdatePaginationLabel();

                btnPaginationPrev.Enabled = currentPage > 1;
                btnPaginationNext.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show("HTTP hasta yükleme hatası: " + ex.Message);
            }
        }

        private void BtnPaginationPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPatients();
            }
        }

        private void BtnPaginationNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadPatients();
            }
        }

        private void UpdatePaginationLabel()
        {
            lblPaginationInfo.Text = $"Sayfa {currentPage} / {totalPages}";
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            flpPatients.SuspendLayout();
            flpPatients.Controls.Clear();

            if (string.IsNullOrEmpty(keyword))
            {
                currentPage = 1;
                LoadPatients();
                flpPatients.ResumeLayout();
                return;
            }

            List<PatientDto> fullList = await LoadAllPatientsForSearch();

            var filtered = fullList
                .Where(p => ($"{p.FirstName} {p.LastName}".ToLower().Contains(keyword)
                          || p.TcNo.ToLower().Contains(keyword)))
                .ToList();

            if (filtered.Count == 0)
            {
                flpPatients.Controls.Add(new Label
                {
                    Text = "Arama sonucu bulunamadı.",
                    Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Padding = new Padding(10),
                    Margin = new Padding(10)
                });
            }
            else
            {
                foreach (var patient in filtered)
                {
                    var card = CreatePatientCard(patient);
                    flpPatients.Controls.Add(card);
                }
            }

            flpPatients.ResumeLayout();
            btnPaginationPrev.Enabled = false;
            btnPaginationNext.Enabled = false;
        }

        private async Task<List<PatientDto>> LoadAllPatientsForSearch()
        {
            try
            {
                var response = await ApiService.GetAsync<ApiResult<List<PatientDto>>>("doctor/patient/list");
                return response?.Success == true && response.Data != null ? response.Data : new List<PatientDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama verisi alınamadı: " + ex.Message);
                return new List<PatientDto>();
            }
        }

        private Panel CreatePatientCard(PatientDto patient)
        {
            Panel card = new Panel
            {
                Width = 320,
                Height = 190,
                BackColor = Color.White,
                Margin = new Padding(10, 10, 10, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = patient,
                Cursor = Cursors.Hand,
                Padding = new Padding(15, 20, 10, 20)
            };

            Panel detailPanel = new Panel
            {
                Width = 300,
                Height = 400,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                AutoScroll = true
            };

            this.Controls.Add(detailPanel);
            detailPanel.BringToFront();
            cardDetailMap[card] = detailPanel;

            card.Controls.Add(new TransparentLabel
            {
                Text = $"👤 {patient.FirstName} {patient.LastName}",
                Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            });

            card.Controls.Add(new TransparentLabel
            {
                Text = $"🆔 TC: {patient.TcNo}",
                Location = new Point(10, 50),
                Font = new Font("Bahnschrift SemiCondensed", 12F),
                AutoSize = true
            });

            card.Controls.Add(new TransparentLabel
            {
                Text = $"🎂 Doğum: {patient.BirthDate:dd.MM.yyyy}",
                Font = new Font("Bahnschrift SemiCondensed", 12F),
                Location = new Point(10, 75),
                AutoSize = true
            });

            card.Controls.Add(new TransparentLabel
            {
                Text = $"⚥ Cinsiyet: {patient.Gender}",
                Font = new Font("Bahnschrift SemiCondensed", 12F),
                Location = new Point(10, 100),
                AutoSize = true
            });

            card.Controls.Add(new TransparentLabel
            {
                Text = $"📞 Tel: {patient.Phone}",
                Location = new Point(10, 125),
                Font = new Font("Bahnschrift SemiCondensed", 12F),
                AutoSize = true
            });

            card.Controls.Add(new TransparentLabel
            {
                Text = $"🏥 Sigorta: {patient.Insurance}",
                Location = new Point(10, 155),
                Font = new Font("Bahnschrift SemiCondensed", 12F),
                AutoSize = true
            });

            card.Click += (s, e) => SelectPatientCard(card);
            card.MouseEnter += (s, e) => { card.BackColor = Color.Gainsboro; HideAllDetails(); ShowPatientDetails(card); };
            card.MouseLeave += (s, e) =>
            {
                if (card != selectedPatientCard)
                    card.BackColor = Color.White;
                HideAllDetails();
            };

            return card;
        }

        private async Task ShowPatientDetails(Panel card)
        {
            if (!cardDetailMap.TryGetValue(card, out Panel detailPanel) || card.Tag is not PatientDto patient)
                return;

            if (detailPanel.Visible && selectedPatientIdForDetails == patient.Id && !string.IsNullOrEmpty(patient.LastPrescriptionDate))
                return;

            selectedPatientIdForDetails = patient.Id;

            detailPanel.Controls.Clear();

            Label lblTitle = new Label()
            {
                Text = "Hasta Detayları",
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Margin = new Padding(0)
            };

            Panel contentPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                AutoScroll = true
            };

            int yPos = 10;
            int labelWidth = 120;
            int valueWidth = 160;
            int rowHeight = 35;

            AddDetailLabel(contentPanel, "Ad Soyad:", $"{patient.FirstName} {patient.LastName}", ref yPos, labelWidth, valueWidth, rowHeight);
            AddDetailLabel(contentPanel, "TC No:", patient.TcNo, ref yPos, labelWidth, valueWidth, rowHeight);
            AddDetailLabel(contentPanel, "Doğum Tarihi:", patient.BirthDate.ToShortDateString(), ref yPos, labelWidth, valueWidth, rowHeight);
            AddDetailLabel(contentPanel, "Cinsiyet:", patient.Gender, ref yPos, labelWidth, valueWidth, rowHeight);
            AddDetailLabel(contentPanel, "Telefon:", patient.Phone, ref yPos, labelWidth, valueWidth, rowHeight);
            AddDetailLabel(contentPanel, "Şehir/İlçe:", $"{patient.City}/{patient.District}", ref yPos, labelWidth, valueWidth, rowHeight);
            AddDetailLabel(contentPanel, "Sigorta:", patient.Insurance, ref yPos, labelWidth, valueWidth, rowHeight);

            string lastDate;
            if (string.IsNullOrEmpty(patient.LastPrescriptionDate))
            {
                lastDate = await GetLastPrescriptionDate(patient.Id);
                patient.LastPrescriptionDate = lastDate;
            }
            else
            {
                lastDate = patient.LastPrescriptionDate;
            }

            AddDetailLabel(contentPanel, "Son Muayene:", lastDate, ref yPos, labelWidth, valueWidth, rowHeight);

            detailPanel.Controls.Add(contentPanel);
            detailPanel.Controls.Add(lblTitle);

            Point cardLocation = card.PointToScreen(Point.Empty);
            Point relativeLocation = this.PointToClient(cardLocation);

            int maxBottom = this.ClientSize.Height - detailPanel.Height - 10;
            int y = Math.Min(relativeLocation.Y, Math.Max(0, maxBottom));
            int x = relativeLocation.X + card.Width + 5;

            if (x + detailPanel.Width > this.Width)
                x = relativeLocation.X - detailPanel.Width - 5;

            detailPanel.Location = new Point(x, y);
            detailPanel.Visible = true;
            detailPanel.BringToFront();
        }

        private void AddDetailLabel(Panel parent, string labelText, string valueText, ref int yPos, int labelWidth, int valueWidth, int rowHeight)
        {
            Label lbl = new Label()
            {
                Text = labelText,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Location = new Point(10, yPos),
                Size = new Size(labelWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblValue = new Label()
            {
                Text = valueText,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(10 + labelWidth, yPos),
                Size = new Size(valueWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleLeft
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(lblValue);

            yPos += rowHeight + 5;
        }

        private void HidePatientDetails(Panel card)
        {
            if (cardDetailMap.TryGetValue(card, out Panel detailPanel))
            {
                detailPanel.Visible = false;
            }
        }

        private void HideAllDetails()
        {
            foreach (var panel in cardDetailMap.Values)
            {
                panel.Visible = false;
            }
        }

        private async void SelectPatientCard(Panel card)
        {
            if (selectedPatientCard != null)
            {
                selectedPatientCard.BackColor = Color.White;
                if (cardDetailMap.TryGetValue(selectedPatientCard, out Panel oldDetail))
                    oldDetail.Visible = false;
            }

            selectedPatientCard = card;
            card.BackColor = Color.LightBlue;

            if (card.Tag is PatientDto patient)
            {
                SelectedPatient.Id = patient.Id;
                SelectedPatient.FirstName = patient.FirstName;
                SelectedPatient.LastName = patient.LastName;
                SelectedPatient.TcNo = patient.TcNo;
                SelectedPatient.Insurance = patient.Insurance;
                SelectedPatient.BirthDate = patient.BirthDate;
                SelectedPatient.Gender = patient.Gender;
                SelectedPatient.City = patient.City;
                SelectedPatient.District = patient.District;
                SelectedPatient.Phone = patient.Phone;

                await ShowPatientDetails(card);
                ShowToast($"{SelectedPatient.FirstName} seçildi");
            }
        }


        private async Task<string> GetLastPrescriptionDate(int patientId)
        {
            try
            {
                var response = await ApiService.GetAsync<ApiResult<List<PrescriptionDto>>>("prescription/list");

                if (response == null || !response.Success || response.Data == null)
                    return "Kayıt Yok";

                var last = response.Data
                    .Where(p => p.PatientId == patientId)
                    .OrderByDescending(p => p.PrescriptionDate)
                    .FirstOrDefault();

                return last != null ? last.PrescriptionDate.ToShortDateString() : "Kayıt Yok";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Son reçete hatası: " + ex.Message);
                return "Kayıt Yok";
            }
        }


        private async void UpdateDashboardCounts()
        {
            try
            {
                var patientsTask = ApiService.GetAsync<ApiResult<List<PatientDto>>>("doctor/patient/list");
                var prescriptionsTask = ApiService.GetAsync<ApiResult<List<PrescriptionDto>>>("prescription/list");
                var drugsTask = ApiService.GetAsync<ApiResult<List<DrugDto>>>("drug/list");

                await Task.WhenAll(patientsTask, prescriptionsTask, drugsTask);

                var patients = await patientsTask;
                var prescriptions = await prescriptionsTask;
                var drugs = await drugsTask;

                lblPatientCount.Text = patients?.Data?.Count.ToString() ?? "0";
                lblPrescriptionCount.Text = prescriptions?.Data?.Count.ToString() ?? "0";
                lblDrugCount.Text = drugs?.Data?.Count.ToString() ?? "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dashboard güncelleme hatası: " + ex.Message);
            }
        }


        private void BtnAddPatient_Click(object sender, EventArgs e)
        {
            if (selectedPatientCard != null)
            {
                selectedPatientCard.BackColor = Color.White;
                selectedPatientCard = null;
            }

            PatientForm form = new PatientForm();
            form.DoctorId = LoggedUser.Id;
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
                UpdateDashboardCounts();
                ResetSelectedPatient();
            }
        }

        private void BtnUpdatePatient_Click(object sender, EventArgs e)
        {
            if (selectedPatientCard == null)
            {
                MessageBox.Show("Önce bir hasta seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PatientForm form = new PatientForm();
            form.IsUpdateMode = true;
            form.PatientId = SelectedPatient.Id;
            form.FirstName = SelectedPatient.FirstName;
            form.LastName = SelectedPatient.LastName;
            form.TcNo = SelectedPatient.TcNo;
            form.Insurance = SelectedPatient.Insurance;
            form.BirthDate = SelectedPatient.BirthDate;
            form.Gender = SelectedPatient.Gender;
            form.City = SelectedPatient.City;
            form.District = SelectedPatient.District;
            form.Phone = SelectedPatient.Phone;

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
                UpdateDashboardCounts();
                ResetSelectedPatient();
            }
        }

        private async void BtnDeletePatient_Click(object sender, EventArgs e)
        {
            if (selectedPatientCard == null)
            {
                MessageBox.Show("Önce bir hasta seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"{SelectedPatient.FirstName} {SelectedPatient.LastName} adlı hastayı ve tüm reçetelerini silmek istiyor musunuz?",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                string endpoint = $"doctor/patient/delete/{SelectedPatient.Id}";

                var response = await ApiService.DeleteAsync<ApiResult<string>>(endpoint);

                if (response != null && response.Success)
                {
                    MessageBox.Show("Hasta ve ilişkili reçeteler başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPatients();
                    UpdateDashboardCounts();
                    selectedPatientCard = null;
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarısız: " + response?.Message ?? "Bilinmeyen hata", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HTTP silme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ShowToast(string message)
        {
            if (currentToast != null && !currentToast.IsDisposed)
                currentToast.Dispose();

            Label lblToast = new Label
            {
                Text = message,
                AutoSize = true,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                Padding = new Padding(10),
                Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Form parentForm = FindForm();
            if (parentForm != null)
            {
                lblToast.Location = new Point((parentForm.Width - lblToast.Width) / 2, 30);
                parentForm.Controls.Add(lblToast);
                lblToast.BringToFront();

                currentToast = lblToast;

                Timer timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (s, e) =>
                {
                    lblToast?.Dispose();
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void AddDashboardHoverEffects(Panel panel)
        {
            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = Color.FromArgb(240, 240, 240);
                panel.BorderStyle = BorderStyle.Fixed3D;
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = Color.White;
                panel.BorderStyle = BorderStyle.FixedSingle;
            };
        }

        private void ResetSelectedPatient()
        {
            SelectedPatient.Id = 0;
            selectedPatientCard = null;
        }
    }
}