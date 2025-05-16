using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class PharmacyInventoryControl
    {
        private IContainer components = null;

        private Panel panelMenuTabs;
        private Button btnTabStock;
        private Button btnTabSupply;
        private Button btnTabHistory;

        private Panel panelMainContent;
        private Panel panelStockList;
        private Panel panelAllDrugsList;
        private Panel panelHistory;
        private Panel panelCards;

        private Panel dashboardPanel;
        private ComboBox cmbStockFilter;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblNoResultsStock;

        private Panel panelSupply;
        private Panel panelSupplyList;
        private Panel panelSupplyForm;
        private Label lblNoResultsSupplyStock;

        private TextBox txtSearchSupply;
        private ComboBox cmbSupplyFilter;

        private TextBox txtSearchHistory;
        private DataGridView dgvHistory;
        private DataGridView dgvGroupedHistory;
        private DataTable cachedHistoryData;



        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // === Üst Sekme Paneli ===
            panelMenuTabs = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.WhiteSmoke };

            btnTabStock = new Button
            {
                Text = "📦 Stoklar",
                Dock = DockStyle.Left,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold)
            };

            btnTabSupply = new Button
            {
                Text = "➕ Yeni Tedarik",
                Dock = DockStyle.Left,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold)
            };

            btnTabHistory = new Button
            {
                Text = "📜 Geçmiş",
                Dock = DockStyle.Left,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold)
            };

            panelMenuTabs.Controls.Add(btnTabHistory);
            panelMenuTabs.Controls.Add(btnTabSupply);
            panelMenuTabs.Controls.Add(btnTabStock);

            // === Ana İçerik Paneli ===
            panelMainContent = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            // === Alt Sekmeler (Sayfalar) ===
            panelStockList = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 0, 15), BackColor = Color.White };
            panelAllDrugsList = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            panelHistory = new Panel { Dock = DockStyle.Fill, BackColor = Color.Gainsboro };

            // === Stok Kartları Paneli ===
            panelCards = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            panelStockList.Controls.Add(panelCards);

            // === Dashboard Paneli ve Araçları ===
            dashboardPanel = new Panel();
            cmbStockFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(750, 15),
                Size = new Size(200, 30),
                Font = new Font("Bahnschrift", 10F)
            };
            cmbStockFilter.Items.AddRange(new object[]
            {
                "Tümü",
                "Stok Az (<50)",
                "SKT Geçmiş",
                "SKT Yaklaşan (1 ay)",
                "Sadece Reçeteli",
                "Sadece Reçetesiz"
            });
            cmbStockFilter.SelectedIndex = 0;
            dashboardPanel.Controls.Add(cmbStockFilter);

            txtSearch = new TextBox
            {
                Location = new Point(1110, 15),
                Size = new Size(150, 28),
                Font = new Font("Bahnschrift", 10F)
            };
            dashboardPanel.Controls.Add(txtSearch);

            lblNoResultsStock = new Label
            {
                Text = "❌ İlaç bulunamadı.",
                ForeColor = Color.DarkRed,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                AutoSize = true,
                Visible = false,
                Location = new Point(20, 20)
            };
            panelCards.Controls.Add(lblNoResultsStock);

            // === Yeni Tedarik Paneli ===
            panelSupply = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            panelSupplyList = new Panel
            {
                Dock = DockStyle.Left,
                BackColor = Color.White,
                Padding = new Padding(10),
                AutoScroll = true
            };

            panelSupplyForm = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle
            };

            Panel panelSupplyTopBar = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblSearch = new Label
            {
                Text = "🔍 İlaç Ara:",
                Location = new Point(10, 15),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };

            txtSearchSupply = new TextBox
            {
                PlaceholderText = "🔍 Ara (isim/barcode)",
                Width = 250,
                Font = new Font("Bahnschrift", 11),
                Location = new Point(115, 12)
            };

            Label lblFilter = new Label
            {
                Text = "📂 Filtrele:",
                Location = new Point(400, 15),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold)
            };

            cmbSupplyFilter = new ComboBox
            {
                Location = new Point(500, 12),
                Width = 200,
                Font = new Font("Bahnschrift", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSupplyFilter.Items.AddRange(new string[]
            {
                "Tümü",
                "Sadece Reçeteli",
                "Sadece Reçetesiz"
            });
            cmbSupplyFilter.SelectedIndex = 0;

            lblNoResultsSupplyStock = new Label
            {
                Text = "❌ İlaç bulunamadı.",
                ForeColor = Color.DarkRed,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                AutoSize = true,
                Visible = false,
                Location = new Point(20, 20),
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            panelSupplyTopBar.Controls.Add(lblSearch);
            panelSupplyTopBar.Controls.Add(txtSearchSupply);
            panelSupplyTopBar.Controls.Add(lblFilter);
            panelSupplyTopBar.Controls.Add(cmbSupplyFilter);

            panelSupply.Controls.Add(panelSupplyForm);
            panelSupply.Controls.Add(panelSupplyList);
            panelSupply.Controls.Add(panelSupplyTopBar);


            // === Sekme Panelini Ana Panel'e Bağla ===
            panelMainContent.Controls.Add(panelStockList);
            panelMainContent.Controls.Add(panelAllDrugsList);
            panelMainContent.Controls.Add(panelSupply);
            panelMainContent.Controls.Add(panelHistory);

            Controls.Add(panelMainContent);
            Controls.Add(panelMenuTabs);

            // === İlk görünüm ===
            panelStockList.Visible = true;
            panelAllDrugsList.Visible = false;
            panelSupply.Visible = false;
            panelHistory.Visible = false;

            Name = "PharmacyInventoryControl";
            Size = new Size(1280, 800);
            Load += PharmacyInventoryControl_Load;
        }

    }
}
