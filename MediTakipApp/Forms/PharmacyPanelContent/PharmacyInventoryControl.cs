using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class PharmacyInventoryControl : UserControl
    {
        string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";

        private Dictionary<Panel, Panel> drugsDetailMap = new Dictionary<Panel, Panel>();

        public PharmacyInventoryControl()
        {
            InitializeComponent();
        }

        private void PharmacyInventoryControl_Load(object sender, EventArgs e)
        {
            CreateDashboardCard(cardTotalDrugs, lblTotalDrugsTitle, lblTotalDrugsCount, "Toplam İlaç", Color.MidnightBlue);
            CreateDashboardCard(cardLowStock, lblLowStockTitle, lblLowStockCount, "Düşük Stok", Color.OrangeRed);
            CreateDashboardCard(cardExpiringSoon, lblExpiringSoonTitle, lblExpiringSoonCount, "SKT Yaklaşan", Color.DarkGoldenrod);

            LoadStockCards();
            UpdateDashboard();

            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += (s, e) => LoadStockCards();

            txtSearchCard.TextChanged += TxtSearchCard_TextChanged;
            btnRefreshCards.Click += (s, e) =>
            {
                LoadStockCards();
                UpdateDashboard();
            };
        }

        private void LoadStockCards()
        {
            pnlStockCards.Controls.Clear();

            string query = @"
        SELECT D.Id, D.Name, D.Barcode, D.ActiveIngredient, D.UsageAge, D.IsPrescription, D.Price,
               ISNULL(SUM(S.StockQuantity), 0) AS TotalStock,
               MIN(S.ExpirationDate) AS EarliestExpiry
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

            string filter = cmbFilter?.SelectedItem?.ToString() ?? "Tümü";

            foreach (DataRow row in dt.Rows)
            {
                int stock = row["TotalStock"] == DBNull.Value ? 0 : Convert.ToInt32(row["TotalStock"]);
                DateTime? expiry = row["EarliestExpiry"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["EarliestExpiry"]);

                if (filter == "Stok Az" && stock >= 50)
                    continue;
                if (filter == "Stok Bitmiş" && stock > 0)
                    continue;
                if (filter == "SKT Yaklaşan" && (expiry == null || expiry > DateTime.Today.AddMonths(1)))
                    continue;

                Panel card = CreateStockCard(row);
                pnlStockCards.Controls.Add(card);
            }
        }


        private void UpdateDashboard()
        {
            string query = @"
        SELECT
            COUNT(DISTINCT D.Id) AS TotalDrugs,
            COUNT(CASE WHEN S.StockQuantity < 50 THEN 1 END) AS LowStock,
            COUNT(CASE WHEN S.StockQuantity = 0 THEN 1 END) AS ZeroStock,
            COUNT(CASE WHEN S.ExpirationDate <= DATEADD(MONTH, 1, GETDATE()) THEN 1 END) AS ExpiringSoon
        FROM Drugs D
        LEFT JOIN DrugStocks S ON D.Id = S.DrugId";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblTotalDrugsCount.Text = row["TotalDrugs"].ToString();
                lblLowStockCount.Text = row["LowStock"].ToString();
                lblExpiringSoonCount.Text = row["ExpiringSoon"].ToString();
            }
        }


        private Panel CreateStockCard(DataRow row)
        {
            Panel card = new Panel
            {
                Width = 300,
                Height = 250,
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

            int stock = row["TotalStock"] == DBNull.Value ? 0 : Convert.ToInt32(row["TotalStock"]);
            decimal price = row["Price"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Price"]);
            DateTime? expiry = row["EarliestExpiry"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["EarliestExpiry"]);

            card.Controls.Add(new TransparentLabel { Text = $"💊 {row["Name"]}", Font = new Font("Bahnschrift SemiCondensed", 14, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🔬 Etken Madde: {row["ActiveIngredient"]}", Location = new Point(10, 40), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"👶 Kullanım Yaşı: {row["UsageAge"]}+", Location = new Point(10, 60), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"🏷️ Barkod: {row["Barcode"]}", Location = new Point(10, 80), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"💰 Fiyat: {price:C2}", Location = new Point(10, 100), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"📦 Stok: {stock} adet", Location = new Point(10, 120), AutoSize = true });
            card.Controls.Add(new TransparentLabel { Text = $"⏳ SKT: {(expiry == null ? "-" : ((DateTime)expiry).ToShortDateString())}", Location = new Point(10, 140), AutoSize = true });

            string prescriptionInfo = Convert.ToBoolean(row["IsPrescription"]) ? "Reçeteli" : "Reçetesiz";
            Color presColor = Convert.ToBoolean(row["IsPrescription"]) ? Color.Red : Color.DarkGreen;
            card.Controls.Add(new TransparentLabel { Text = $"🩺 {prescriptionInfo}", Location = new Point(10, 165), AutoSize = true, ForeColor = presColor });

            if (stock < 50)
            {
                card.Controls.Add(new TransparentLabel
                {
                    Text = stock == 0 ? "❌ Stok Tükenmiş!" : "⚠️ Stok az!",
                    Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                    ForeColor = stock == 0 ? Color.DarkRed : Color.DarkOrange,
                    Location = new Point(10, 185),
                    AutoSize = true
                });
            }

            Button btnSupply = new Button
            {
                Text = "➕ Tedarik Et",
                Width = card.Width - 2,
                Height = 30,
                Location = new Point(0, card.Height - 30),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSupply.FlatAppearance.BorderSize = 0;
            btnSupply.Click += (s, e) => OpenSupplyForm(row);
            card.Controls.Add(btnSupply);

            card.Click += (s, e) => OnDrugCardClick(row);
            card.MouseEnter += (s, e) => { card.BackColor = Color.LightGray; ShowDescriptionDetail(card); };
            card.MouseLeave += (s, e) => { card.BackColor = Color.White; HideDescriptionDetail(card); };

            return card;
        }

        private void OpenSupplyForm(DataRow row)
        {
            //int drugId = Convert.ToInt32(row["Id"]);
            //string drugName = row["Name"].ToString();

            //using (var supplyForm = new SupplyForm(drugId, drugName))
            //{
            //    if (supplyForm.ShowDialog() == DialogResult.OK)
            //    {
            //        LoadStockCards();
            //        UpdateDashboard();
            //    }
            //}
        }

        private void TxtSearchCard_TextChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnlStockCards.Controls)
            {
                if (ctrl is Panel card && card.Tag is DataRow row)
                {
                    string name = row["Name"].ToString().ToLower();
                    string barcode = row["Barcode"].ToString().ToLower();
                    string search = txtSearchCard.Text.ToLower();

                    card.Visible = name.Contains(search) || barcode.Contains(search);
                }
            }
        }

        private void OnDrugCardClick(DataRow row) { }
        private void ShowDescriptionDetail(Panel card) { if (drugsDetailMap.ContainsKey(card)) drugsDetailMap[card].Visible = true; }
        private void HideDescriptionDetail(Panel card) { if (drugsDetailMap.ContainsKey(card)) drugsDetailMap[card].Visible = false; }
    }
}