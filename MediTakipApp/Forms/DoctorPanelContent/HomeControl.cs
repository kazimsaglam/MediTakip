using MediTakipApp.Utils;
using MetiDataTsApi;
using MetiDataTsApi.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;


namespace MediTakipApp.Forms
{
    public partial class HomeControl : UserControl
    {
        private List<Patient> allPatients = new();
        private Dictionary<Panel, Panel> cardDetailMap = new();
        private Panel? selectedPatientCard = null;
        private Label? currentToast = null;
        private Timer detailHideTimer = new();

        // Sayfalama
        private int currentPage = 1;
        private int itemsPerPage = 25;
        private int totalPages = 1;


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
                detailHideTimer = new Timer { Interval = 200 };
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
            ApiLoadPatients();

            ResetSelectedPatient();

            cmbFilterDoctor.SelectedIndex = 0;
        }

        private async void ApiLoadPatients()
        {
            ResetSelectedPatient();

            foreach (var panel in cardDetailMap.Values)
            {
                if (!panel.IsDisposed) panel.Dispose();
            }
            cardDetailMap.Clear();

            try
            {

                var api = new ApiClient();
                var response = await api.GetPatientListAsync();
                var fullList = response.Data ?? new List<Patient>();

                if (cmbFilterDoctor.SelectedIndex == 1)
                    allPatients = fullList.Where(p => p.DoctorId == LoggedUser.Id).ToList();
                else
                    allPatients = fullList;

                UpdateDashboardCounts();
                if (!response.Success || response.Data == null || response.Data.Count == 0)
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

                    int totalRecords = response.Data.Count;
                    totalPages = (int)Math.Ceiling(totalRecords / (double)itemsPerPage);

                    int offset = (currentPage - 1) * itemsPerPage;
                    var pagedPatients = response.Data.Skip(offset).Take(itemsPerPage).ToList();

                    flpPatients.SuspendLayout();
                    flpPatients.Controls.Clear();

                    foreach (var patient in pagedPatients)
                    {
                        Panel patientCard = CreatePatientCard(patient);
                        flpPatients.Controls.Add(patientCard);
                    }
                }

                flpPatients.ResumeLayout();
                UpdatePaginationLabel();

                // Disable/Enable pagination buttons
                btnPaginationPrev.Enabled = currentPage > 1;
                btnPaginationNext.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta yükleme hatası: " + ex.Message);
            }
        }

        private async void LoadPatients()
        {
            ResetSelectedPatient();

            foreach (var panel in cardDetailMap.Values)
            {
                if (!panel.IsDisposed) panel.Dispose();
            }
            cardDetailMap.Clear();

            try
            {
                {

                    int totalRecords = allPatients.Count;
                    totalPages = (int)Math.Ceiling(totalRecords / (double)itemsPerPage);

                    int offset = (currentPage - 1) * itemsPerPage;
                    var pagedPatients = allPatients.Skip(offset).Take(itemsPerPage).ToList();

                    flpPatients.SuspendLayout();
                    flpPatients.Controls.Clear();

                    foreach (var patient in pagedPatients)
                    {
                        Panel patientCard = CreatePatientCard(patient);
                        flpPatients.Controls.Add(patientCard);
                    }
                }

                flpPatients.ResumeLayout();
                UpdatePaginationLabel();

                // Disable/Enable pagination buttons
                btnPaginationPrev.Enabled = currentPage > 1;
                btnPaginationNext.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta yükleme hatası: " + ex.Message);
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
            totalPages = (int)Math.Ceiling(allPatients.Count / (double)itemsPerPage);
            lblPaginationInfo.Text = $"Sayfa {currentPage} / {totalPages}";

        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
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

            List<Patient> fullList = LoadAllPatientsForSearch();

            var filtered = fullList.AsEnumerable()
                .Where(r => (r.FirstName + " " + r.LastName).ToString().ToLower().Contains(keyword)
                         || r.TcNo.ToString().ToLower().Contains(keyword))
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
                foreach (var row in filtered)
                {
                    Patient patient = new Patient
                    {
                        Id = Convert.ToInt32(row.Id),
                        FirstName = row.FirstName.ToString(),
                        LastName = row.LastName.ToString(),
                        TcNo = row.TcNo.ToString(),
                        Insurance = row.Insurance.ToString(),
                        BirthDate = Convert.ToDateTime(row.BirthDate),
                        Gender = row.Gender.ToString(),
                        City = row.City.ToString(),
                        District = row.District.ToString(),
                        Phone = row.Phone.ToString()
                    };

                    Panel card = CreatePatientCard(patient);
                    flpPatients.Controls.Add(card);
                }
            }

            flpPatients.ResumeLayout();
            btnPaginationPrev.Enabled = false;
            btnPaginationNext.Enabled = false;
        }

        private List<Patient> LoadAllPatientsForSearch()
        {
            List<Patient> all = new();
            try
            {
                all = allPatients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama verisi yüklenemedi: " + ex.Message);
            }

            return all;
        }


        private Panel CreatePatientCard(Patient patient)
        {
            Panel card = new Panel()
            {
                Width = 306,
                Height = 185,
                BackColor = Color.White,
                Margin = new Padding(10, 10, 10, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = patient,
                Cursor = Cursors.Hand,
                Padding = new Padding(10)
            };

            Panel detailPanel = new Panel()
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

            // Etiketler
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
                Text = $"🎂 Doğum: {Convert.ToDateTime(patient.BirthDate).ToString("dd.MM.yyyy")}",
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

            // Eventler
            card.Click += (s, e) => SelectPatientCard(card);
            card.MouseEnter += (s, e) => { card.BackColor = Color.Gainsboro; ShowPatientDetails(card); };
            card.MouseLeave += (s, e) =>
            {
                if (card != selectedPatientCard)
                    card.BackColor = Color.White;
                HidePatientDetails(card);
            };

            return card;
        }

        private void ShowPatientDetails(Panel card)
        {
            if (cardDetailMap.TryGetValue(card, out Panel detailPanel) && card.Tag is Patient row)
            {
                if (detailPanel.Visible)
                    return;

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

                AddDetailLabel(contentPanel, "Ad Soyad:", $"{row.FirstName} {row.LastName}", ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "TC No:", row.TcNo.ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Doğum Tarihi:", Convert.ToDateTime(row.BirthDate).ToShortDateString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Cinsiyet:", row.Gender.ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Telefon:", row.Phone.ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Şehir/İlçe:", $"{row.City}/{row.District}", ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Sigorta:", row.Insurance.ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Son Muayene:", row.LastPrescriptionDate.HasValue ? row.LastPrescriptionDate.Value.ToShortDateString() : "Kayıt Yok", ref yPos, labelWidth, valueWidth, rowHeight); detailPanel.Controls.Add(contentPanel);
                detailPanel.Controls.Add(lblTitle);

                Point cardLocation = card.PointToScreen(Point.Empty);
                Point relativeLocation = this.PointToClient(cardLocation);

                // Panel ekran dışına taşmasın
                int maxBottom = this.ClientSize.Height - detailPanel.Height - 10;
                int y = Math.Min(relativeLocation.Y, Math.Max(0, maxBottom));

                // Sağ ya da sol tarafa yerleştir
                int x = relativeLocation.X + card.Width + 5;
                if (x + detailPanel.Width > this.Width)
                    x = relativeLocation.X - detailPanel.Width - 5;

                detailPanel.Location = new Point(x, y);
                detailPanel.Visible = true;
                detailPanel.BringToFront();
            }
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

        private void SelectPatientCard(Panel card)
        {
            // Önceki seçimi temizle
            if (selectedPatientCard != null)
            {
                selectedPatientCard.BackColor = Color.White;
                if (cardDetailMap.TryGetValue(selectedPatientCard, out Panel oldDetail))
                    oldDetail.Visible = false;
            }

            // Yeni seçim yap
            selectedPatientCard = card;
            card.BackColor = Color.LightBlue;

            if (card.Tag is Patient row)
            {
                SelectedPatient.Id = Convert.ToInt32(row.Id);
                SelectedPatient.FirstName = row.FirstName.ToString();
                SelectedPatient.LastName = row.LastName.ToString();
                SelectedPatient.TcNo = row.TcNo.ToString();
                SelectedPatient.Insurance = row.Insurance.ToString();
                SelectedPatient.BirthDate = Convert.ToDateTime(row.BirthDate);
                SelectedPatient.Gender = row.Gender.ToString();
                SelectedPatient.City = row.City.ToString();
                SelectedPatient.District = row.District.ToString();
                SelectedPatient.Phone = row.Phone.ToString();

                ShowPatientDetails(card);
                ShowToast($"{SelectedPatient.FirstName} seçildi");
            }
        }

        private void UpdateDashboardCounts()
        {
            try
            {
                lblTopPatientCount.Text = $"👥 Toplam: {allPatients.Count}";
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

            PatientForm form = new PatientForm(allPatients);
            form.DoctorId = LoggedUser.Id;
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
                UpdateDashboardCounts();
                ResetSelectedPatient();
            }
        }

        private void CmbFilterDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            ApiLoadPatients();
        }


        private void BtnUpdatePatient_Click(object sender, EventArgs e)
        {
            if (selectedPatientCard == null)
            {
                MessageBox.Show("Önce bir hasta seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PatientForm form = new PatientForm(allPatients);
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
                var api = new ApiClient();
                var response = await api.DeletePatient(SelectedPatient.Id.ToString());
                if (response.Success)
                {
                    allPatients = allPatients.Where(p => p.Id != SelectedPatient.Id).ToList();
                    MessageBox.Show("Hasta ve tüm ilişkili reçeteler başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPatients();
                    UpdateDashboardCounts();
                    selectedPatientCard = null;
                }
                else
                {
                    MessageBox.Show("Silme sırasında hata oluştu: ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void flpPatients_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}