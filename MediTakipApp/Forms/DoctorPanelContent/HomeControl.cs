using System;
using System.Data;
using System.Drawing.Drawing2D;
using Microsoft.Data.SqlClient;
using Timer = System.Windows.Forms.Timer;

namespace MediTakipApp.Forms
{
    public partial class HomeControl : UserControl
    {
        private string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";
        private DataTable allPatients = new DataTable();
        private Dictionary<Panel, Panel> cardDetailMap = new Dictionary<Panel, Panel>();
        private Panel selectedPatientCard = null;
        private Label currentToast = null;
        private Timer detailHideTimer = new Timer();
        private Panel currentlyHoveredCard = null;

        public HomeControl()
        {
            InitializeComponent();
            InitializeEvents();
            InitializeDetailPanel();
            InitializeTimer();
        }

        private void InitializeEvents()
        {
            this.Load += HomeControl_Load;
            this.btnAdd.Click += BtnAddPatient_Click;
            this.btnUpdate.Click += BtnUpdatePatient_Click;
            this.btnDelete.Click += BtnDeletePatient_Click;
            this.txtSearch.TextChanged += TxtSearch_TextChanged;
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
            detailHideTimer.Interval = 200;
            detailHideTimer.Tick += (s, e) =>
            {
                if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition)))
                {
                    HideAllDetails();
                }
            };
            detailHideTimer.Start();
        }

        private void HomeControl_Load(object sender, EventArgs e)
        {
            LoadPatients();
            UpdateDashboardCounts();

            AddDashboardHoverEffects(cardPatient);
            AddDashboardHoverEffects(cardPrescription);
            AddDashboardHoverEffects(cardDrug);
        }

        private void LoadPatients()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Patients", conn);
                    allPatients.Clear();
                    da.Fill(allPatients);

                    flpPatients.SuspendLayout();
                    flpPatients.Controls.Clear();

                    foreach (DataRow row in allPatients.Rows)
                    {
                        Panel patientCard = CreatePatientCard(row);
                        flpPatients.Controls.Add(patientCard);
                    }

                    flpPatients.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta yükleme hatası: " + ex.Message);
            }
        }

        private Panel CreatePatientCard(DataRow row)
        {
            Panel card = new Panel()
            {
                Width = 280,
                Height = 150,
                BackColor = Color.White,
                Margin = new Padding(15),
                Tag = row,
                Cursor = Cursors.Hand
            };

            // Kart tasarımı
            card.Paint += (sender, e) => PaintCard(card, e);

            // Detay paneli oluştur
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

            // Kontrolleri ekle
            AddCardControls(card, row);

            // Event'ler
            card.MouseEnter += (s, e) => OnCardMouseEnter(card);
            card.MouseLeave += (s, e) => OnCardMouseLeave(card);
            card.Click += (s, e) => SelectPatientCard(card);

            return card;
        }

        private void PaintCard(Panel card, PaintEventArgs e)
        {
            int shadowSize = 5;
            int borderRadius = 8;

            // Gölge efekti
            for (int i = 0; i < shadowSize; i++)
            {
                e.Graphics.DrawRectangle(
                    new Pen(Color.FromArgb(10 * i, Color.Black)),
                    new Rectangle(i, i, card.Width - 1 - 2 * i, card.Height - 1 - 2 * i)
                );
            }

            // Köşe yuvarlatma
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
                path.AddArc(card.Width - borderRadius - 1, 0, borderRadius, borderRadius, 270, 90);
                path.AddArc(card.Width - borderRadius - 1, card.Height - borderRadius - 1, borderRadius, borderRadius, 0, 90);
                path.AddArc(0, card.Height - borderRadius - 1, borderRadius, borderRadius, 90, 90);
                path.CloseAllFigures();

                card.Region = new Region(path);
            }
        }

        private void AddCardControls(Panel card, DataRow row)
        {
            // Hasta bilgileri
            TransparentLabel lblName = new TransparentLabel()
            {
                Text = row["FirstName"] + " " + row["LastName"],
                Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(20, 20),
                AutoSize = true
            };

            TransparentLabel lblTc = new TransparentLabel()
            {
                Text = "TC: " + row["TcNo"],
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(20, 55),
                AutoSize = true
            };

            TransparentLabel lblPhone = new TransparentLabel()
            {
                Text = "📞 " + row["Phone"],
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(20, 80),
                AutoSize = true
            };

            TransparentLabel lblInsurance = new TransparentLabel()
            {
                Text = "🏥 " + row["Insurance"],
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(20, 105),
                AutoSize = true
            };

            card.Controls.Add(lblName);
            card.Controls.Add(lblTc);
            card.Controls.Add(lblPhone);
            card.Controls.Add(lblInsurance);
        }

        private void OnCardMouseEnter(Panel card)
        {
            currentlyHoveredCard = card;

            // Seçili kart değilse hover rengini uygula
            if (card != selectedPatientCard)
            {
                card.BackColor = Color.FromArgb(245, 245, 245);
            }

            ShowPatientDetails(card);
        }

        private void OnCardMouseLeave(Panel card)
        {
            // Sadece seçili olmayan kartların rengini değiştir
            if (card != selectedPatientCard)
            {
                card.BackColor = Color.White;
            }

            if (currentlyHoveredCard == card)
            {
                currentlyHoveredCard = null;
            }
            HidePatientDetails(card);
        }

        private void ShowPatientDetails(Panel card)
        {
            if (cardDetailMap.TryGetValue(card, out Panel detailPanel) && card.Tag is DataRow row)
            {
                detailPanel.Controls.Clear();

                // Başlık
                Label lblTitle = new Label()
                {
                    Text = "Hasta Detayları",
                    Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.White,
                    Margin = new Padding(0) // Margin'i sıfırla
                };

                // Ana panel (içerik için)
                Panel contentPanel = new Panel()
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    AutoScroll = true
                };

                // Detayları buraya ekleyeceğiz
                int yPos = 10;
                int labelWidth = 120;
                int valueWidth = 160;
                int rowHeight = 35;

                AddDetailLabel(contentPanel, "Ad Soyad:", $"{row["FirstName"]} {row["LastName"]}", ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "TC No:", row["TcNo"].ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Doğum Tarihi:", Convert.ToDateTime(row["BirthDate"]).ToShortDateString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Cinsiyet:", row["Gender"].ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Telefon:", row["Phone"].ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Şehir/İlçe:", $"{row["City"]}/{row["District"]}", ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Sigorta:", row["Insurance"].ToString(), ref yPos, labelWidth, valueWidth, rowHeight);
                AddDetailLabel(contentPanel, "Son Muayene:", GetLastAppointmentDate(row["Id"]), ref yPos, labelWidth, valueWidth, rowHeight);

                detailPanel.Controls.Add(contentPanel);
                detailPanel.Controls.Add(lblTitle);

                // Konumlandırma
                Point cardLocation = card.PointToScreen(Point.Empty);
                Point relativeLocation = this.PointToClient(cardLocation);

                int x = relativeLocation.X + card.Width + 5;
                if (x + detailPanel.Width > this.Width)
                    x = relativeLocation.X - detailPanel.Width - 5;

                detailPanel.Location = new Point(x, Math.Max(relativeLocation.Y, 0));
                detailPanel.Visible = true;
                detailPanel.BringToFront();
            }
        }

        private void AddDetailLabel(Panel parent, string labelText, string valueText, ref int yPos, int labelWidth, int valueWidth, int rowHeight)
        {
            // Etiket (Başlık)
            Label lbl = new Label()
            {
                Text = labelText,
                Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Location = new Point(10, yPos),
                Size = new Size(labelWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Değer
            Label lblValue = new Label()
            {
                Text = valueText,
                Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(10 + labelWidth, yPos),
                Size = new Size(valueWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleLeft
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(lblValue);

            yPos += rowHeight + 5; // Bir sonraki satır için pozisyonu artır
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

            // Yeni seçimi yap
            selectedPatientCard = card;
            card.BackColor = Color.LightBlue;

            if (card.Tag is DataRow row)
            {
                SelectedPatient.Id = Convert.ToInt32(row["Id"]);
                SelectedPatient.FirstName = row["FirstName"].ToString();
                SelectedPatient.LastName = row["LastName"].ToString();
                SelectedPatient.TcNo = row["TcNo"].ToString();
                SelectedPatient.Insurance = row["Insurance"].ToString();
                SelectedPatient.BirthDate = Convert.ToDateTime(row["BirthDate"]);
                SelectedPatient.Gender = row["Gender"].ToString();
                SelectedPatient.City = row["City"].ToString();
                SelectedPatient.District = row["District"].ToString();
                SelectedPatient.Phone = row["Phone"].ToString();

                ShowPatientDetails(card);
                ShowToast($"{SelectedPatient.FirstName} seçildi");
            }
        }

        private string GetLastAppointmentDate(object patientId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 AppointmentDate FROM Appointments WHERE PatientId = @id ORDER BY AppointmentDate DESC",
                        conn);
                    cmd.Parameters.AddWithValue("@id", patientId);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDateTime(result).ToShortDateString() : "Kayıt Yok";
                }
            }
            catch
            {
                return "Bilinmiyor";
            }
        }

        private void UpdateDashboardCounts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Hasta sayısı
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Patients", conn);
                    lblPatientCount.Text = cmd.ExecuteScalar().ToString();

                    // Reçete sayısı
                    cmd.CommandText = "SELECT COUNT(*) FROM Prescriptions";
                    lblPrescriptionCount.Text = cmd.ExecuteScalar().ToString();

                    // İlaç sayısı
                    cmd.CommandText = "SELECT COUNT(*) FROM Drugs";
                    lblDrugCount.Text = cmd.ExecuteScalar().ToString();
                }
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
            }
        }

        private void BtnDeletePatient_Click(object sender, EventArgs e)
        {
            if (selectedPatientCard == null)
            {
                MessageBox.Show("Önce bir hasta seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"{SelectedPatient.FirstName} {SelectedPatient.LastName} adlı hastayı silmek istediğinize emin misiniz?",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Patients WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", SelectedPatient.Id);
                    cmd.ExecuteNonQuery();
                }

                LoadPatients();
                UpdateDashboardCounts();
                ShowToast("Hasta başarıyla silindi!");
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            flpPatients.SuspendLayout(); // UI güncellemelerini geçici durdur
            flpPatients.Controls.Clear();

            foreach (DataRow row in allPatients.Rows)
            {
                string fullName = (row["FirstName"] + " " + row["LastName"]).ToLower();
                string tcNo = row["TcNo"].ToString().ToLower();

                // Arama kriterlerini kontrol et
                if (fullName.Contains(keyword) || tcNo.Contains(keyword))
                {
                    // Yeni kart oluştur
                    Panel card = CreatePatientCard(row);

                    // Eğer bu kart seçili hastaya aitse
                    if (selectedPatientCard != null && row["Id"].ToString() == SelectedPatient.Id.ToString())
                    {
                        card.BackColor = Color.LightBlue; // Mavi yap
                        selectedPatientCard = card;       // Seçili kart referansını güncelle
                    }

                    flpPatients.Controls.Add(card); // Kartı ekle
                }
            }

            flpPatients.ResumeLayout(); // UI güncellemelerini yeniden başlat
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
    }
}