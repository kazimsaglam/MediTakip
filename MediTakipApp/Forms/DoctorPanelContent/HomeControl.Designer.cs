using System.Windows.Forms;

namespace MediTakipApp.Forms
{
    partial class HomeControl
    {
        private System.ComponentModel.IContainer components = null;

        // Üst Panel (Arama ve Butonlar)
        private Panel topPanel;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        // Ana Alanlar
        private FlowLayoutPanel flpPatients;
        private Panel panelDashboard;
        private FlowLayoutPanel flpDashboardCards;

        // Sayfalama
        private Panel panelPagination;
        private Button btnPaginationPrev;
        private Button btnPaginationNext;
        private Label lblPaginationInfo;

        // Dashboard Kartları
        private Panel cardPatient;
        private Panel cardPrescription;
        private Panel cardDrug;

        private TransparentLabel lblPatientCount;
        private TransparentLabel lblPrescriptionCount;
        private TransparentLabel lblDrugCount;

        private TransparentLabel lblPatientTitle;
        private TransparentLabel lblPrescriptionTitle;
        private TransparentLabel lblDrugTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                detailHideTimer?.Stop();
                detailHideTimer?.Dispose();

                foreach (var panel in cardDetailMap.Values)
                {
                    panel.Dispose();
                }
                cardDetailMap.Clear();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            topPanel = new Panel();
            txtSearch = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            flpPatients = new FlowLayoutPanel();
            panelDashboard = new Panel();
            flpDashboardCards = new FlowLayoutPanel();
            cardPatient = new Panel();
            cardPrescription = new Panel();
            cardDrug = new Panel();
            panelPagination = new Panel();
            btnPaginationPrev = new Button();
            lblPaginationInfo = new Label();
            btnPaginationNext = new Button();
            lblPatientCount = new TransparentLabel();
            lblPrescriptionCount = new TransparentLabel();
            lblDrugCount = new TransparentLabel();
            lblPatientTitle = new TransparentLabel();
            lblPrescriptionTitle = new TransparentLabel();
            lblDrugTitle = new TransparentLabel();
            topPanel.SuspendLayout();
            panelDashboard.SuspendLayout();
            flpDashboardCards.SuspendLayout();
            panelPagination.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.White;
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnAdd);
            topPanel.Controls.Add(btnUpdate);
            topPanel.Controls.Add(btnDelete);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1300, 80);
            topPanel.TabIndex = 2;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            txtSearch.Location = new Point(20, 25);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Hasta Ara...";
            txtSearch.Size = new Size(250, 32);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            btnAdd.Location = new Point(290, 25);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 35);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "➕ Yeni Hasta";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAddPatient_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightSkyBlue;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            btnUpdate.Location = new Point(420, 25);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(120, 35);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "✏️ Güncelle";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += BtnUpdatePatient_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.IndianRed;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            btnDelete.Location = new Point(550, 25);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 35);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "🗑️ Sil";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDeletePatient_Click;
            // 
            // flpPatients
            // 
            flpPatients.AutoScroll = true;
            flpPatients.BackColor = Color.WhiteSmoke;
            flpPatients.Dock = DockStyle.Fill;
            flpPatients.Location = new Point(0, 80);
            flpPatients.Name = "flpPatients";
            flpPatients.Padding = new Padding(10);
            flpPatients.Size = new Size(1020, 720);
            flpPatients.TabIndex = 0;
            // 
            // panelDashboard
            // 
            panelDashboard.BackColor = Color.White;
            panelDashboard.Controls.Add(flpDashboardCards);
            panelDashboard.Controls.Add(panelPagination);
            panelDashboard.Dock = DockStyle.Right;
            panelDashboard.Location = new Point(1020, 80);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Padding = new Padding(10);
            panelDashboard.Size = new Size(280, 720);
            panelDashboard.TabIndex = 1;
            // 
            // flpDashboardCards
            // 
            flpDashboardCards.AutoScroll = true;
            flpDashboardCards.Controls.Add(cardPatient);
            flpDashboardCards.Controls.Add(cardPrescription);
            flpDashboardCards.Controls.Add(cardDrug);
            flpDashboardCards.Dock = DockStyle.Fill;
            flpDashboardCards.FlowDirection = FlowDirection.TopDown;
            flpDashboardCards.Location = new Point(10, 10);
            flpDashboardCards.Name = "flpDashboardCards";
            flpDashboardCards.Size = new Size(260, 635);
            flpDashboardCards.TabIndex = 0;
            flpDashboardCards.WrapContents = false;
            // 
            // cardPatient
            // 
            cardPatient.Location = new Point(3, 3);
            cardPatient.Name = "cardPatient";
            cardPatient.Size = new Size(200, 100);
            cardPatient.TabIndex = 0;
            // 
            // cardPrescription
            // 
            cardPrescription.Location = new Point(3, 109);
            cardPrescription.Name = "cardPrescription";
            cardPrescription.Size = new Size(200, 100);
            cardPrescription.TabIndex = 1;
            // 
            // cardDrug
            // 
            cardDrug.Location = new Point(3, 215);
            cardDrug.Name = "cardDrug";
            cardDrug.Size = new Size(200, 100);
            cardDrug.TabIndex = 2;
            // 
            // panelPagination
            // 
            panelPagination.BackColor = Color.WhiteSmoke;
            panelPagination.Controls.Add(btnPaginationPrev);
            panelPagination.Controls.Add(lblPaginationInfo);
            panelPagination.Controls.Add(btnPaginationNext);
            panelPagination.Dock = DockStyle.Bottom;
            panelPagination.Location = new Point(10, 645);
            panelPagination.Name = "panelPagination";
            panelPagination.Padding = new Padding(10);
            panelPagination.Size = new Size(260, 65);
            panelPagination.TabIndex = 1;
            // 
            // btnPaginationPrev
            // 
            btnPaginationPrev.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            btnPaginationPrev.Location = new Point(10, 10);
            btnPaginationPrev.Name = "btnPaginationPrev";
            btnPaginationPrev.Size = new Size(70, 40);
            btnPaginationPrev.TabIndex = 0;
            btnPaginationPrev.Text = "◀ Geri";
            btnPaginationPrev.Click += BtnPaginationPrev_Click;
            // 
            // lblPaginationInfo
            // 
            lblPaginationInfo.AutoSize = true;
            lblPaginationInfo.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            lblPaginationInfo.Location = new Point(90, 20);
            lblPaginationInfo.Name = "lblPaginationInfo";
            lblPaginationInfo.Size = new Size(85, 20);
            lblPaginationInfo.TabIndex = 1;
            lblPaginationInfo.Text = "Sayfa 1 / 1";
            // 
            // btnPaginationNext
            // 
            btnPaginationNext.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            btnPaginationNext.Location = new Point(180, 10);
            btnPaginationNext.Name = "btnPaginationNext";
            btnPaginationNext.Size = new Size(70, 40);
            btnPaginationNext.TabIndex = 2;
            btnPaginationNext.Text = "İleri ▶";
            btnPaginationNext.Click += BtnPaginationNext_Click;
            // 
            // lblPatientCount
            // 
            lblPatientCount.Location = new Point(0, 0);
            lblPatientCount.Name = "lblPatientCount";
            lblPatientCount.Size = new Size(100, 23);
            lblPatientCount.TabIndex = 0;
            // 
            // lblPrescriptionCount
            // 
            lblPrescriptionCount.Location = new Point(0, 0);
            lblPrescriptionCount.Name = "lblPrescriptionCount";
            lblPrescriptionCount.Size = new Size(100, 23);
            lblPrescriptionCount.TabIndex = 0;
            // 
            // lblDrugCount
            // 
            lblDrugCount.Location = new Point(0, 0);
            lblDrugCount.Name = "lblDrugCount";
            lblDrugCount.Size = new Size(100, 23);
            lblDrugCount.TabIndex = 0;
            // 
            // lblPatientTitle
            // 
            lblPatientTitle.Location = new Point(0, 0);
            lblPatientTitle.Name = "lblPatientTitle";
            lblPatientTitle.Size = new Size(100, 23);
            lblPatientTitle.TabIndex = 0;
            // 
            // lblPrescriptionTitle
            // 
            lblPrescriptionTitle.Location = new Point(0, 0);
            lblPrescriptionTitle.Name = "lblPrescriptionTitle";
            lblPrescriptionTitle.Size = new Size(100, 23);
            lblPrescriptionTitle.TabIndex = 0;
            // 
            // lblDrugTitle
            // 
            lblDrugTitle.Location = new Point(0, 0);
            lblDrugTitle.Name = "lblDrugTitle";
            lblDrugTitle.Size = new Size(100, 23);
            lblDrugTitle.TabIndex = 0;
            // 
            // HomeControl
            // 
            Controls.Add(flpPatients);
            Controls.Add(panelDashboard);
            Controls.Add(topPanel);
            Name = "HomeControl";
            Size = new Size(1300, 800);
            Load += HomeControl_Load;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            panelDashboard.ResumeLayout(false);
            flpDashboardCards.ResumeLayout(false);
            panelPagination.ResumeLayout(false);
            panelPagination.PerformLayout();
            ResumeLayout(false);

            // Dashboard kartlarını oluştur
            CreateDashboardCard(cardPatient, lblPatientTitle, lblPatientCount, "Toplam Hastalar", Color.MediumSeaGreen);
            CreateDashboardCard(cardPrescription, lblPrescriptionTitle, lblPrescriptionCount, "Toplam Reçeteler", Color.SteelBlue);
            CreateDashboardCard(cardDrug, lblDrugTitle, lblDrugCount, "Toplam İlaçlar", Color.DarkOrange);

        }

        private void CreateDashboardCard(Panel card, TransparentLabel title, TransparentLabel count, string titleText, Color color)
        {
            card.Size = new Size(250, 120);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;

            title.Text = titleText;
            title.Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold);
            title.ForeColor = Color.Gray;
            title.Location = new Point(20, 10);
            title.AutoSize = true;

            count.Text = "0";
            count.Font = new Font("Bahnschrift SemiCondensed", 26F, FontStyle.Bold);
            count.ForeColor = color;
            count.Location = new Point(20, 50);
            count.AutoSize = true;

            card.Controls.Add(title);
            card.Controls.Add(count);
        }

    }
}
