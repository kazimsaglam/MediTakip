using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class PharmacyInventoryControl
    {
        private IContainer components = null;

        private Panel topPanel;
        private TextBox txtSearchCard;
        private Button btnRefreshCards;
        private ComboBox cmbFilter;
        private Label lblSearchTitle;
        private FlowLayoutPanel pnlStockCards;
        private Panel panelDashboard;
        private FlowLayoutPanel flpDashboardCards;

        private Panel cardTotalDrugs;
        private Panel cardLowStock;
        private Panel cardExpiringSoon;

        private TransparentLabel lblTotalDrugsCount;
        private TransparentLabel lblLowStockCount;
        private TransparentLabel lblExpiringSoonCount;

        private TransparentLabel lblTotalDrugsTitle;
        private TransparentLabel lblLowStockTitle;
        private TransparentLabel lblExpiringSoonTitle;

        private Label lblDashboardTitle;

        private void InitializeComponent()
        {
            topPanel = new Panel();
            lblSearchTitle = new Label();
            txtSearchCard = new TextBox();
            cmbFilter = new ComboBox();
            btnRefreshCards = new Button();
            pnlStockCards = new FlowLayoutPanel();
            panelDashboard = new Panel();
            flpDashboardCards = new FlowLayoutPanel();
            lblDashboardTitle = new Label();
            cardTotalDrugs = new Panel();
            cardLowStock = new Panel();
            cardExpiringSoon = new Panel();
            lblTotalDrugsCount = new TransparentLabel();
            lblLowStockCount = new TransparentLabel();
            lblExpiringSoonCount = new TransparentLabel();
            lblTotalDrugsTitle = new TransparentLabel();
            lblLowStockTitle = new TransparentLabel();
            lblExpiringSoonTitle = new TransparentLabel();
            topPanel.SuspendLayout();
            panelDashboard.SuspendLayout();
            flpDashboardCards.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.White;
            topPanel.BorderStyle = BorderStyle.FixedSingle;
            topPanel.Controls.Add(lblSearchTitle);
            topPanel.Controls.Add(txtSearchCard);
            topPanel.Controls.Add(cmbFilter);
            topPanel.Controls.Add(btnRefreshCards);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1020, 60);
            topPanel.TabIndex = 1;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold);
            lblSearchTitle.Location = new Point(20, 18);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(97, 23);
            lblSearchTitle.TabIndex = 0;
            lblSearchTitle.Text = "🔍 İlaç Ara:";
            // 
            // txtSearchCard
            // 
            txtSearchCard.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            txtSearchCard.Location = new Point(123, 13);
            txtSearchCard.Name = "txtSearchCard";
            txtSearchCard.PlaceholderText = "İlaç Ara...";
            txtSearchCard.Size = new Size(250, 32);
            txtSearchCard.TabIndex = 1;
            // 
            // cmbFilter
            // 
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold);
            cmbFilter.Items.AddRange(new object[] { "Tümü", "Stok Az", "Stok Bitmiş", "SKT Yaklaşan" });
            cmbFilter.Location = new Point(393, 13);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(150, 30);
            cmbFilter.TabIndex = 2;
            // 
            // btnRefreshCards
            // 
            btnRefreshCards.BackColor = Color.LightSteelBlue;
            btnRefreshCards.FlatStyle = FlatStyle.Flat;
            btnRefreshCards.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold);
            btnRefreshCards.Location = new Point(553, 11);
            btnRefreshCards.Name = "btnRefreshCards";
            btnRefreshCards.Size = new Size(100, 32);
            btnRefreshCards.TabIndex = 3;
            btnRefreshCards.Text = "🔁 Yenile";
            btnRefreshCards.UseVisualStyleBackColor = false;
            // 
            // pnlStockCards
            // 
            pnlStockCards.AutoScroll = true;
            pnlStockCards.BackColor = Color.WhiteSmoke;
            pnlStockCards.BorderStyle = BorderStyle.Fixed3D;
            pnlStockCards.Dock = DockStyle.Fill;
            pnlStockCards.Location = new Point(0, 60);
            pnlStockCards.Name = "pnlStockCards";
            pnlStockCards.Padding = new Padding(10);
            pnlStockCards.Size = new Size(1020, 740);
            pnlStockCards.TabIndex = 0;
            // 
            // panelDashboard
            // 
            panelDashboard.BackColor = Color.White;
            panelDashboard.BorderStyle = BorderStyle.FixedSingle;
            panelDashboard.Controls.Add(flpDashboardCards);
            panelDashboard.Dock = DockStyle.Right;
            panelDashboard.Location = new Point(1020, 0);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Padding = new Padding(10);
            panelDashboard.Size = new Size(260, 800);
            panelDashboard.TabIndex = 2;
            // 
            // flpDashboardCards
            // 
            flpDashboardCards.AutoScroll = true;
            flpDashboardCards.Controls.Add(lblDashboardTitle);
            flpDashboardCards.Controls.Add(cardTotalDrugs);
            flpDashboardCards.Controls.Add(cardLowStock);
            flpDashboardCards.Controls.Add(cardExpiringSoon);
            flpDashboardCards.Dock = DockStyle.Fill;
            flpDashboardCards.FlowDirection = FlowDirection.TopDown;
            flpDashboardCards.Location = new Point(10, 10);
            flpDashboardCards.Name = "flpDashboardCards";
            flpDashboardCards.Size = new Size(238, 778);
            flpDashboardCards.TabIndex = 0;
            flpDashboardCards.WrapContents = false;
            // 
            // lblDashboardTitle
            // 
            lblDashboardTitle.BackColor = Color.SteelBlue;
            lblDashboardTitle.Dock = DockStyle.Top;
            lblDashboardTitle.Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold);
            lblDashboardTitle.ForeColor = Color.White;
            lblDashboardTitle.Location = new Point(3, 0);
            lblDashboardTitle.Name = "lblDashboardTitle";
            lblDashboardTitle.Size = new Size(200, 45);
            lblDashboardTitle.TabIndex = 0;
            lblDashboardTitle.Text = "📊 Stok İstatistikleri";
            lblDashboardTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardTotalDrugs
            // 
            cardTotalDrugs.Location = new Point(3, 48);
            cardTotalDrugs.Name = "cardTotalDrugs";
            cardTotalDrugs.Size = new Size(200, 100);
            cardTotalDrugs.TabIndex = 1;
            // 
            // cardLowStock
            // 
            cardLowStock.Location = new Point(3, 154);
            cardLowStock.Name = "cardLowStock";
            cardLowStock.Size = new Size(200, 100);
            cardLowStock.TabIndex = 2;
            // 
            // cardExpiringSoon
            // 
            cardExpiringSoon.Location = new Point(3, 260);
            cardExpiringSoon.Name = "cardExpiringSoon";
            cardExpiringSoon.Size = new Size(200, 100);
            cardExpiringSoon.TabIndex = 3;
            // 
            // lblTotalDrugsCount
            // 
            lblTotalDrugsCount.Location = new Point(0, 0);
            lblTotalDrugsCount.Name = "lblTotalDrugsCount";
            lblTotalDrugsCount.Size = new Size(100, 23);
            lblTotalDrugsCount.TabIndex = 0;
            // 
            // lblLowStockCount
            // 
            lblLowStockCount.Location = new Point(0, 0);
            lblLowStockCount.Name = "lblLowStockCount";
            lblLowStockCount.Size = new Size(100, 23);
            lblLowStockCount.TabIndex = 0;
            // 
            // lblExpiringSoonCount
            // 
            lblExpiringSoonCount.Location = new Point(0, 0);
            lblExpiringSoonCount.Name = "lblExpiringSoonCount";
            lblExpiringSoonCount.Size = new Size(100, 23);
            lblExpiringSoonCount.TabIndex = 0;
            // 
            // lblTotalDrugsTitle
            // 
            lblTotalDrugsTitle.Location = new Point(0, 0);
            lblTotalDrugsTitle.Name = "lblTotalDrugsTitle";
            lblTotalDrugsTitle.Size = new Size(100, 23);
            lblTotalDrugsTitle.TabIndex = 0;
            // 
            // lblLowStockTitle
            // 
            lblLowStockTitle.Location = new Point(0, 0);
            lblLowStockTitle.Name = "lblLowStockTitle";
            lblLowStockTitle.Size = new Size(100, 23);
            lblLowStockTitle.TabIndex = 0;
            // 
            // lblExpiringSoonTitle
            // 
            lblExpiringSoonTitle.Location = new Point(0, 0);
            lblExpiringSoonTitle.Name = "lblExpiringSoonTitle";
            lblExpiringSoonTitle.Size = new Size(100, 23);
            lblExpiringSoonTitle.TabIndex = 0;
            // 
            // PharmacyInventoryControl
            // 
            Controls.Add(pnlStockCards);
            Controls.Add(topPanel);
            Controls.Add(panelDashboard);
            Name = "PharmacyInventoryControl";
            Size = new Size(1280, 800);
            Load += PharmacyInventoryControl_Load;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            panelDashboard.ResumeLayout(false);
            flpDashboardCards.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void CreateDashboardCard(Panel card, TransparentLabel title, TransparentLabel count, string titleText, Color color)
        {
            card.Size = new Size(220, 120);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;

            title.Text = titleText;
            title.Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold);
            title.ForeColor = Color.Gray;
            title.Location = new Point(15, 10);
            title.AutoSize = true;

            count.Text = "0";
            count.Font = new Font("Bahnschrift SemiCondensed", 28F, FontStyle.Bold);
            count.ForeColor = color;
            count.Location = new Point(15, 50);
            count.AutoSize = true;

            card.Controls.Add(title);
            card.Controls.Add(count);
        }
    }
}
