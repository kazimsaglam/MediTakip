namespace MediTakipApp.Forms.DoctorPanelContent
{
    partial class DrugsControl
    {
        private System.ComponentModel.IContainer components = null;

        private Panel topPanel;
        private Label lblPatientInfo;
        private Label lblDiagnosis;
        private TextBox txtDiagnosis;
        private Label lblSearch;
        private TextBox txtSearch;

        private FlowLayoutPanel flpDrugs;

        private Panel bottomPanel;
        private FlowLayoutPanel flpSelectedDrugs;
        private Button btnSavePrescription;

        private Panel panelHistory;
        private Label lblHistoryTitle;
        private FlowLayoutPanel flpHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            topPanel = new Panel();
            lblPatientInfo = new Label();
            lblDiagnosis = new Label();
            txtDiagnosis = new TextBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            flpDrugs = new FlowLayoutPanel();
            bottomPanel = new Panel();
            flpSelectedDrugs = new FlowLayoutPanel();
            btnSavePrescription = new Button();
            panelHistory = new Panel();
            flpHistory = new FlowLayoutPanel();
            lblHistoryTitle = new Label();
            topPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            panelHistory.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.WhiteSmoke;
            topPanel.Controls.Add(lblPatientInfo);
            topPanel.Controls.Add(lblDiagnosis);
            topPanel.Controls.Add(txtDiagnosis);
            topPanel.Controls.Add(lblSearch);
            topPanel.Controls.Add(txtSearch);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(10);
            topPanel.Size = new Size(1000, 100);
            topPanel.TabIndex = 2;
            // 
            // lblPatientInfo
            // 
            lblPatientInfo.Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold);
            lblPatientInfo.ForeColor = Color.DarkBlue;
            lblPatientInfo.Location = new Point(10, 10);
            lblPatientInfo.Name = "lblPatientInfo";
            lblPatientInfo.Size = new Size(400, 25);
            lblPatientInfo.TabIndex = 0;
            lblPatientInfo.Text = "\U0001f9d1‍⚕️ Seçilen Hasta: -";
            // 
            // lblDiagnosis
            // 
            lblDiagnosis.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            lblDiagnosis.Location = new Point(10, 50);
            lblDiagnosis.Name = "lblDiagnosis";
            lblDiagnosis.Size = new Size(93, 25);
            lblDiagnosis.TabIndex = 1;
            lblDiagnosis.Text = "📝 Teşhis:";
            // 
            // txtDiagnosis
            // 
            txtDiagnosis.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            txtDiagnosis.Location = new Point(109, 47);
            txtDiagnosis.Name = "txtDiagnosis";
            txtDiagnosis.PlaceholderText = "Hastanın teşhisini giriniz....";
            txtDiagnosis.Size = new Size(250, 32);
            txtDiagnosis.TabIndex = 2;
            // 
            // lblSearch
            // 
            lblSearch.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            lblSearch.Location = new Point(360, 50);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(104, 25);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "🔎 İlaç Ara:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            txtSearch.Location = new Point(470, 47);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "İlaç adı girin...";
            txtSearch.Size = new Size(200, 32);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // flpDrugs
            // 
            flpDrugs.AutoScroll = true;
            flpDrugs.BackColor = Color.WhiteSmoke;
            flpDrugs.Dock = DockStyle.Fill;
            flpDrugs.Location = new Point(0, 100);
            flpDrugs.Margin = new Padding(0, 100, 0, 0);
            flpDrugs.Name = "flpDrugs";
            flpDrugs.Padding = new Padding(15);
            flpDrugs.Size = new Size(1000, 326);
            flpDrugs.TabIndex = 0;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = Color.Gainsboro;
            bottomPanel.Controls.Add(flpSelectedDrugs);
            bottomPanel.Controls.Add(btnSavePrescription);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 426);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Padding = new Padding(15);
            bottomPanel.Size = new Size(1000, 274);
            bottomPanel.TabIndex = 2;
            // 
            // flpSelectedDrugs
            // 
            flpSelectedDrugs.AutoScroll = true;
            flpSelectedDrugs.Dock = DockStyle.Fill;
            flpSelectedDrugs.FlowDirection = FlowDirection.TopDown;
            flpSelectedDrugs.Location = new Point(15, 15);
            flpSelectedDrugs.Name = "flpSelectedDrugs";
            flpSelectedDrugs.Size = new Size(970, 200);
            flpSelectedDrugs.TabIndex = 0;
            flpSelectedDrugs.WrapContents = false;
            // 
            // btnSavePrescription
            // 
            btnSavePrescription.BackColor = Color.MediumSeaGreen;
            btnSavePrescription.Cursor = Cursors.Hand;
            btnSavePrescription.Dock = DockStyle.Bottom;
            btnSavePrescription.FlatAppearance.BorderSize = 0;
            btnSavePrescription.FlatStyle = FlatStyle.Flat;
            btnSavePrescription.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            btnSavePrescription.ForeColor = Color.White;
            btnSavePrescription.Location = new Point(15, 215);
            btnSavePrescription.Name = "btnSavePrescription";
            btnSavePrescription.Size = new Size(970, 44);
            btnSavePrescription.TabIndex = 1;
            btnSavePrescription.Text = "💾 Reçeteyi Kaydet";
            btnSavePrescription.UseVisualStyleBackColor = false;
            btnSavePrescription.Click += BtnSavePrescription_Click;
            // 
            // panelHistory
            // 
            panelHistory.BackColor = Color.WhiteSmoke;
            panelHistory.Controls.Add(flpHistory);
            panelHistory.Controls.Add(lblHistoryTitle);
            panelHistory.Dock = DockStyle.Right;
            panelHistory.Location = new Point(1000, 0);
            panelHistory.Name = "panelHistory";
            panelHistory.Padding = new Padding(10);
            panelHistory.Size = new Size(300, 700);
            panelHistory.TabIndex = 3;
            // 
            // flpHistory
            // 
            flpHistory.AutoScroll = true;
            flpHistory.Dock = DockStyle.Fill;
            flpHistory.FlowDirection = FlowDirection.TopDown;
            flpHistory.Location = new Point(10, 110);
            flpHistory.Name = "flpHistory";
            flpHistory.Size = new Size(280, 580);
            flpHistory.TabIndex = 0;
            flpHistory.WrapContents = false;
            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.Dock = DockStyle.Top;
            lblHistoryTitle.Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold);
            lblHistoryTitle.ForeColor = Color.DarkSlateBlue;
            lblHistoryTitle.Location = new Point(10, 10);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Size = new Size(280, 100);
            lblHistoryTitle.TabIndex = 1;
            lblHistoryTitle.Text = "📜 Geçmiş Reçeteler";
            lblHistoryTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrugsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(flpDrugs);
            Controls.Add(bottomPanel);
            Controls.Add(topPanel);
            Controls.Add(panelHistory);
            Name = "DrugsControl";
            Size = new Size(1300, 700);
            Load += DrugsControl_Load;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            panelHistory.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
