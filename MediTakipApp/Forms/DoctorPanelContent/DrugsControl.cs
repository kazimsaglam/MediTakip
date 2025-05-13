using System.Data;
using System.Text;
using MediTakipApp.Utils;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.DoctorPanelContent
{
    public partial class DrugsControl : UserControl
    {
        string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";
        private Dictionary<Panel, Panel> drugsDetailMap = new Dictionary<Panel, Panel>();
        private Dictionary<Panel, Panel> prescriptionDetailMap = new Dictionary<Panel, Panel>();
        private List<PrescribedDrug> selectedDrugs = new List<PrescribedDrug>();
        private DataTable allDrugs = new DataTable();

        public DrugsControl()
        {
            InitializeComponent();
        }

        private void DrugsControl_Load(object sender, EventArgs e)
        {
            if (SelectedPatient.Id == 0)
            {
                lblPatientInfo.Text = "👤 Seçilen Hasta: Yok";
                return;
            }

            lblPatientInfo.Text = $"Seçilen Hasta: {SelectedPatient.FullName} ({GetAge(SelectedPatient.BirthDate)} yaşında)";

            LoadDrugs();
            LoadPatientPrescriptions();
        }

        private void LoadDrugs()
        {
            flpDrugs.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT d.*, 
                ISNULL((
                    SELECT SUM(ds.StockQuantity)
                    FROM DrugStocks ds
                    WHERE ds.DrugId = d.Id
                        AND ds.ExpirationDate >= CAST(GETDATE() AS DATE)
                        AND ds.StockQuantity > 0
                ), 0) AS TotalStock
            FROM Drugs d
            WHERE d.IsActive = 1
            AND EXISTS (
                SELECT 1
                FROM DrugStocks ds
                WHERE ds.DrugId = d.Id
                AND ds.ExpirationDate >= CAST(GETDATE() AS DATE)
                AND ds.StockQuantity > 0
            )";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                allDrugs.Clear();
                da.Fill(allDrugs);

                foreach (DataRow row in allDrugs.Rows)
                {
                    Panel card = CreateDrugCard(row);
                    flpDrugs.Controls.Add(card);
                }
            }
        }

        private Panel CreateDrugCard(DataRow row)
        {
            Panel card = new Panel
            {
                Width = 300,
                Height = 200,
                BackColor = Color.White,
                Margin = new Padding(10, 10, 10, 30),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = row,
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

            // Güncellenmiş sütun: TotalStock
            int stock = row["TotalStock"] == DBNull.Value ? 0 : Convert.ToInt32(row["TotalStock"]);


            // Kart içeriği
            card.Controls.Add(new TransparentLabel { Text = $"💊 {row["Name"]}", Font = new Font("Bahnschrift SemiCondensed", 14, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🔬 Etken Madde: {row["ActiveIngredient"]}", Location = new Point(10, 40), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"👶 Kullanım Yaşı: {row["UsageAge"]}+", Location = new Point(10, 60), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🏷️ Barkod: {row["Barcode"]}", Location = new Point(10, 80), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"📦 Stok: {stock} adet", Location = new Point(10, 100), AutoSize = true });

            // Reçeteli/Reçetesiz
            string prescriptionInfo = Convert.ToBoolean(row["IsPrescription"]) ? "Reçeteli" : "Reçetesiz";
            Color presColor = Convert.ToBoolean(row["IsPrescription"]) ? Color.Red : Color.DarkGreen;

            card.Controls.Add(new TransparentLabel { Text = $"🩺 {prescriptionInfo}", Location = new Point(10, 140), AutoSize = true, ForeColor = presColor });

            // ⚠️ Uyarılar
            int warningY = 165;
            if (stock < 50)
            {
                card.Controls.Add(new TransparentLabel
                {
                    Text = "⚠️ Stok az!",
                    Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                    ForeColor = Color.DarkOrange,
                    Location = new Point(10, warningY),
                    AutoSize = true
                });
            }

            // Eventler
            card.Click += (s, e) => OnDrugCardClick(row);
            card.MouseEnter += (s, e) => { card.BackColor = Color.LightGray; ShowDescriptionDetail(card); };
            card.MouseLeave += (s, e) => { card.BackColor = Color.White; HideDescriptionDetail(card); };

            return card;
        }

        private void ShowDescriptionDetail(Panel card)
        {
            if (drugsDetailMap.TryGetValue(card, out Panel detailPanel) && card.Tag is DataRow row)
            {
                detailPanel.Controls.Clear();

                Label lblDescription = new Label
                {
                    Text = string.IsNullOrWhiteSpace(row["Description"]?.ToString())
                           ? "🏷️ Açıklama bulunamadı."
                           : $"🏷️ {row["Description"].ToString()}",
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

        private void OnDrugCardClick(DataRow row)
        {
            int drugId = Convert.ToInt32(row["Id"]);
            string drugName = row["Name"].ToString();

            // Eğer zaten ekliyse uyarı ver
            if (selectedDrugs.Any(d => d.DrugId == drugId))
            {
                MessageBox.Show($"{drugName} zaten reçeteye eklenmiş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yeni giriş formu aç
            using (DrugDetailForm form = new DrugDetailForm(drugName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    selectedDrugs.Add(new PrescribedDrug
                    {
                        DrugId = drugId,
                        DrugName = drugName,
                        Quantity = form.Quantity,
                        Instructions = form.Dosage,
                        UsagePeriod = form.UsagePeriod,
                        SpecialInstructions = form.SpecialInstructions,
                        Price = Convert.ToDecimal(row["Price"])
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

        private void BtnSavePrescription_Click(object sender, EventArgs e)
        {
            if (selectedDrugs.Count == 0 || SelectedPatient.Id == 0 || string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Lütfen hasta, teşhis ve ilaç seçimi yapınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string prescriptionCode = GeneratePrescriptionCode();
                conn.Open();

                // Ana reçete kaydı
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Prescriptions (PrescriptionCode, PatientId, DoctorId, Diagnosis, PrescriptionDate) 
            OUTPUT INSERTED.PrescriptionId 
            VALUES (@code, @pId, @docId, @diag, @date)", conn);

                cmd.Parameters.AddWithValue("@code", prescriptionCode);
                cmd.Parameters.AddWithValue("@pId", SelectedPatient.Id);
                cmd.Parameters.AddWithValue("@docId", LoggedUser.Id);
                cmd.Parameters.AddWithValue("@diag", txtDiagnosis.Text.Trim());
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                int prescriptionId = (int)cmd.ExecuteScalar();

                // İlaçları kayıt et
                foreach (var drug in selectedDrugs)
                {
                    SqlCommand detailCmd = new SqlCommand(@"
                INSERT INTO PrescriptionDetails 
                (PrescriptionId, DrugId, Dosage, UsagePeriod, SpecialInstructions, Quantity) 
                VALUES 
                (@presId, @drugId, @dosage, @period, @special, @qty)", conn);

                    detailCmd.Parameters.AddWithValue("@presId", prescriptionId);
                    detailCmd.Parameters.AddWithValue("@drugId", drug.DrugId);
                    detailCmd.Parameters.AddWithValue("@dosage", drug.Instructions);
                    detailCmd.Parameters.AddWithValue("@period", drug.UsagePeriod);
                    detailCmd.Parameters.AddWithValue("@special", (object?)drug.SpecialInstructions ?? DBNull.Value);
                    detailCmd.Parameters.AddWithValue("@qty", drug.Quantity);

                    detailCmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Reçete başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            selectedDrugs.Clear();
            txtDiagnosis.Clear();
            RefreshSelectedDrugList();
            LoadPatientPrescriptions();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            flpDrugs.Controls.Clear();

            foreach (DataRow row in allDrugs.Rows)
            {
                string name = row["Name"].ToString().ToLower();
                string barcode = row["Barcode"].ToString().ToLower();

                if (name.Contains(keyword) || barcode.Contains(keyword))
                {
                    flpDrugs.Controls.Add(CreateDrugCard(row));
                }
            }
        }

        private void LoadPatientPrescriptions()
        {
            if (SelectedPatient.Id == 0) return;

            flpHistory.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
SELECT p.PrescriptionId, p.PrescriptionCode, p.Diagnosis, p.PrescriptionDate, 
       (SELECT COUNT(*) FROM PrescriptionDetails pd WHERE pd.PrescriptionId = p.PrescriptionId) AS DrugCount
FROM Prescriptions p
WHERE p.PatientId = @pId
ORDER BY p.PrescriptionDate DESC", conn);

                cmd.Parameters.AddWithValue("@pId", SelectedPatient.Id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int prescriptionId = (int)reader["PrescriptionId"];
                    string diagnosis = reader["Diagnosis"].ToString();
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
                        Tag = prescriptionId
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
                    TransparentLabel lblDate = new TransparentLabel
                    {
                        Text = $"📅 {Convert.ToDateTime(reader["PrescriptionDate"]).ToString("dd.MM.yyyy HH:mm")}",
                        Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                        ForeColor = Color.DarkSlateGray,
                        Location = new Point(10, 10),
                        AutoSize = true
                    };

                    TransparentLabel lblDiag = new TransparentLabel
                    {
                        Text = $"📝 {reader["Diagnosis"].ToString()}",
                        Font = new Font("Bahnschrift SemiCondensed", 12F),
                        Location = new Point(10, 35),
                        AutoSize = true
                    };

                    TransparentLabel lblDrugCount = new TransparentLabel
                    {
                        Text = $"💊 {reader["DrugCount"]} ilaç",
                        Font = new Font("Bahnschrift SemiCondensed", 12F),
                        ForeColor = Color.DarkSlateGray,
                        Location = new Point(10, 60),
                        AutoSize = true
                    };

                    // ↩ Ekle Butonu
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
                        Tag = prescriptionId
                    };
                    btnAddWholePrescription.FlatAppearance.BorderSize = 0;

                    btnAddWholePrescription.Click += (s, e) =>
                    {
                        if (selectedDrugs.Count > 0)
                        {
                            MessageBox.Show("Önceki ilaçları temizlemeden bu reçeteyi ekleyemezsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        using (SqlConnection innerConn = new SqlConnection(connStr))
                        {
                            innerConn.Open();

                            SqlCommand detailCmd = new SqlCommand(@"
                        SELECT d.Id, d.Name, pd.Quantity, pd.Dosage, pd.UsagePeriod
                        FROM PrescriptionDetails pd
                        JOIN Drugs d ON pd.DrugId = d.Id
                        WHERE pd.PrescriptionId = @presId", innerConn);

                            detailCmd.Parameters.AddWithValue("@presId", prescriptionId);
                            SqlDataReader detailReader = detailCmd.ExecuteReader();

                            while (detailReader.Read())
                            {
                                selectedDrugs.Add(new PrescribedDrug
                                {
                                    DrugId = Convert.ToInt32(detailReader["Id"]),
                                    DrugName = detailReader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(detailReader["Quantity"]),
                                    Instructions = detailReader["Dosage"].ToString(),
                                    UsagePeriod = detailReader["UsagePeriod"].ToString()
                                });
                            }

                            detailReader.Close();
                        }

                        txtDiagnosis.Text = diagnosis;
                        RefreshSelectedDrugList();
                        MessageBox.Show("Reçetedeki ilaçlar başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };

                    card.Controls.Add(lblDate);
                    card.Controls.Add(lblDiag);
                    card.Controls.Add(lblDrugCount);
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
                }

                reader.Close();
            }
        }

        private void ShowPrescriptionDetail(Panel card)
        {
            if (prescriptionDetailMap.TryGetValue(card, out Panel detailPanel) && card.Tag is int prescriptionId)
            {
                detailPanel.Controls.Clear();
                detailPanel.AutoScroll = true;
                detailPanel.BackColor = Color.White;
                detailPanel.BorderStyle = BorderStyle.FixedSingle;
                detailPanel.Padding = new Padding(5);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    SqlCommand cmdPres = new SqlCommand("SELECT PrescriptionCode, Diagnosis, PrescriptionDate FROM Prescriptions WHERE PrescriptionId = @id", conn);
                    cmdPres.Parameters.AddWithValue("@id", prescriptionId);

                    SqlDataReader presReader = cmdPres.ExecuteReader();
                    string prescriptionCode = "", diagnosis = "", date = "";

                    if (presReader.Read())
                    {
                        prescriptionCode = presReader["PrescriptionCode"].ToString();
                        diagnosis = presReader["Diagnosis"].ToString();
                        date = Convert.ToDateTime(presReader["PrescriptionDate"]).ToString("dd.MM.yyyy");
                    }
                    presReader.Close();

                    Panel headerPanel = new Panel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Lavender, Dock = DockStyle.Top, Padding = new Padding(8), Margin = new Padding(0, 0, 0, 10) };
                    headerPanel.Controls.Add(new Label { Text = $"📝 Teşhis: {diagnosis}", Font = new Font("Bahnschrift SemiCondensed", 12F), Dock = DockStyle.Top, Height = 25 });
                    headerPanel.Controls.Add(new Label { Text = $"📅 Tarih: {date}", Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Italic), Dock = DockStyle.Top, Height = 20 });
                    headerPanel.Controls.Add(new Label { Text = $"📋 Reçete Kodu: {prescriptionCode}", Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold), Dock = DockStyle.Top, Height = 25 });

                    Panel drugsContainer = new Panel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Dock = DockStyle.Top, Padding = new Padding(5) };
                    SqlCommand cmdDetails = new SqlCommand("SELECT d.Name, pd.Quantity, pd.Dosage, pd.UsagePeriod FROM PrescriptionDetails pd JOIN Drugs d ON pd.DrugId = d.Id WHERE pd.PrescriptionId = @presId", conn);
                    cmdDetails.Parameters.AddWithValue("@presId", prescriptionId);

                    SqlDataReader detailReader = cmdDetails.ExecuteReader();
                    while (detailReader.Read())
                    {
                        Panel drugPanel = new Panel { Height = 80, Dock = DockStyle.Top, Padding = new Padding(8), BackColor = Color.WhiteSmoke, Margin = new Padding(0, 0, 0, 10), BorderStyle = BorderStyle.FixedSingle };
                        drugPanel.Controls.Add(new Label { Text = $"💊 {detailReader["Name"]} x{detailReader["Quantity"]}", Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold), Dock = DockStyle.Top, Height = 25 });
                        drugPanel.Controls.Add(new Label { Text = $"📖 Doz: {detailReader["Dosage"]}", Font = new Font("Bahnschrift SemiCondensed", 10F), Dock = DockStyle.Top, Height = 20 });
                        drugPanel.Controls.Add(new Label { Text = $"⏳ Süre: {detailReader["UsagePeriod"]}", Font = new Font("Bahnschrift SemiCondensed", 10F), Dock = DockStyle.Top, Height = 20 });

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
                        string drugName = detailReader["Name"].ToString();
                        int quantity = Convert.ToInt32(detailReader["Quantity"]);
                        string dosage = detailReader["Dosage"].ToString();
                        string usage = detailReader["UsagePeriod"].ToString();
                        btnAddAgain.Click += (s, e) =>
                        {
                            selectedDrugs.Add(new PrescribedDrug
                            {
                                DrugId = GetDrugIdByName(drugName),
                                DrugName = drugName,
                                Quantity = quantity,
                                Instructions = dosage,
                                UsagePeriod = usage
                            });
                            RefreshSelectedDrugList();
                        };
                        drugPanel.Controls.Add(btnAddAgain);

                        drugsContainer.Controls.Add(drugPanel);
                    }
                    detailReader.Close();

                    detailPanel.Controls.Add(drugsContainer);
                    detailPanel.Controls.Add(headerPanel);
                }

                // 🔥 Konum ve maksimum yükseklik ayarla
                int totalHeight = detailPanel.Controls.Cast<Control>().Sum(c => c.Height + c.Margin.Top + c.Margin.Bottom) + 20;
                int maxHeight = (int)(this.Height * 0.75); // Formun %75'i kadar yükseklik
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
        }

        private int GetDrugIdByName(string name)
        {
            var row = allDrugs.AsEnumerable().FirstOrDefault(r => r["Name"].ToString() == name);
            return row != null ? Convert.ToInt32(row["Id"]) : 0;
        }

        private void HidePrescriptionDetail(Panel card)
        {
            if (prescriptionDetailMap.TryGetValue(card, out Panel detailPanel))
            {
                detailPanel.Visible = false;
            }
        }

        private int GetAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear) age--;
            return age;
        }

        private string GeneratePrescriptionCode()
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            int countToday = 0;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Prescriptions WHERE CAST(PrescriptionDate AS DATE) = CAST(GETDATE() AS DATE)", conn);
                countToday = (int)cmd.ExecuteScalar();
            }

            countToday++; // yeni kayıt için 1 artır
            return $"REC-{today}-{countToday:D3}"; // REC-20240427-001 gibi
        }

        private async void BtnRecommend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Lütfen önce bir teşhis giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int age = GetAge(SelectedPatient.BirthDate);

            // İlaç listesini sade JSON formatında hazırla
            var drugsList = allDrugs.AsEnumerable()
                .Select(row => new
                {
                    Name = row["Name"].ToString(),
                    UsageAge = Convert.ToInt32(row["UsageAge"]),
                    ActiveIngredient = row["ActiveIngredient"].ToString()
                }).ToList();

            var requestBody = new
            {
                diagnosis = txtDiagnosis.Text,
                age = age,
                drugs = drugsList
            };

            string json = System.Text.Json.JsonSerializer.Serialize(requestBody);

            using (var client = new HttpClient())
            {
                btnRecommend.Enabled = false;
                btnRecommend.Text = "⏳ Öneriler alınıyor...";

                try
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:5001/recommend", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var resultJson = await response.Content.ReadAsStringAsync();
                        var result = System.Text.Json.JsonDocument.Parse(resultJson);

                        string raw = result.RootElement.GetProperty("raw_response").ToString();

                        var match = System.Text.RegularExpressions.Regex.Match(raw, @"\[\s*""[^]]+?\]");
                        if (!match.Success)
                        {
                            MessageBox.Show("⚠️ Model geçerli bir JSON cevabı döndürmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string jsonArray = match.Value;
                        var suggestions = System.Text.Json.JsonSerializer.Deserialize<List<string>>(jsonArray);

                        // 🔄 Önce eski öneri etiketlerini ve arka planları sıfırla
                        foreach (Control control in flpDrugs.Controls)
                        {
                            if (control is Panel panel)
                            {
                                panel.BackColor = Color.White;

                                // Mevcut "⭐️ Önerilen" etiketlerini kaldır
                                foreach (var lbl in panel.Controls.OfType<Label>().Where(l => l.Text.Contains("⭐️ Önerilen")).ToList())
                                {
                                    panel.Controls.Remove(lbl);
                                }
                            }
                        }

                        // 🔁 Kartları en baştan sırala: önce önerilenler → sonra kalanlar
                        var allPanels = flpDrugs.Controls.OfType<Panel>().ToList();
                        flpDrugs.Controls.Clear();

                        var sortedPanels = allPanels
                            .OrderByDescending(panel =>
                            {
                                if (panel.Tag is DataRow row)
                                {
                                    string drugName = row["Name"].ToString();
                                    return suggestions.Contains(drugName) ? 1 : 0;
                                }
                                return 0;
                            })
                            .ToList();

                        foreach (var panel in sortedPanels)
                        {
                            if (panel.Tag is DataRow row)
                            {
                                string drugName = row["Name"].ToString();

                                if (suggestions.Contains(drugName))
                                {
                                    if (!allDrugs.AsEnumerable().Any(r => r["Name"].ToString() == drugName))
                                        continue; // Veritabanında yoksa bu kartı gösterme

                                    panel.BackColor = Color.LightGoldenrodYellow;

                                    // ⭐️ etiketi ekle
                                    Label lblStar = new Label
                                    {
                                        Text = "⭐️ Önerilen",
                                        Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold),
                                        ForeColor = Color.DarkOrange,
                                        BackColor = Color.Transparent,
                                        AutoSize = true,
                                        Location = new Point(160, 160)
                                    };
                                    panel.Controls.Add(lblStar);
                                }
                            }

                            flpDrugs.Controls.Add(panel);
                        }

                        MessageBox.Show("✅ Öneriler alındı ve vurgulandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("❌ Öneri alınamadı: " + response.StatusCode, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("⚠️ Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRecommend.Enabled = true;
                    btnRecommend.Text = "🔮 İlaç Öner";
                }

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
