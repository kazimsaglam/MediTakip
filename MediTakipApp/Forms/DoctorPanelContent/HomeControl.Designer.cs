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

        // Sayfalama
        private Panel panelPagination;
        private Button btnPaginationPrev;
        private Button btnPaginationNext;
        private Label lblPaginationInfo;

        private ComboBox cmbFilterDoctor;
        private Label lblTopPatientCount;

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
            lblTopPatientCount = new Label();
            cmbFilterDoctor = new ComboBox();
            flpPatients = new FlowLayoutPanel();
            panelPagination = new Panel();
            btnPaginationPrev = new Button();
            lblPaginationInfo = new Label();
            btnPaginationNext = new Button();
            topPanel.SuspendLayout();
            panelPagination.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.White;
            topPanel.BorderStyle = BorderStyle.Fixed3D;
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnAdd);
            topPanel.Controls.Add(btnUpdate);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(lblTopPatientCount);
            topPanel.Controls.Add(cmbFilterDoctor);
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
            // lblTopPatientCount
            // 
            lblTopPatientCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTopPatientCount.Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold);
            lblTopPatientCount.ForeColor = Color.DarkSlateGray;
            lblTopPatientCount.Location = new Point(1037, 17);
            lblTopPatientCount.Name = "lblTopPatientCount";
            lblTopPatientCount.Size = new Size(209, 50);
            lblTopPatientCount.TabIndex = 4;
            lblTopPatientCount.Text = "👥 Toplam: 0";
            lblTopPatientCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbFilterDoctor
            // 
            cmbFilterDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterDoctor.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            cmbFilterDoctor.Items.AddRange(new object[] { "Tüm Hastalar", "Sadece Benimkiler" });
            cmbFilterDoctor.Location = new Point(650, 25);
            cmbFilterDoctor.Name = "cmbFilterDoctor";
            cmbFilterDoctor.Size = new Size(180, 32);
            cmbFilterDoctor.TabIndex = 5;
            cmbFilterDoctor.SelectedIndexChanged += CmbFilterDoctor_SelectedIndexChanged;
            // 
            // flpPatients
            // 
            flpPatients.AutoScroll = true;
            flpPatients.BackColor = Color.WhiteSmoke;
            flpPatients.BorderStyle = BorderStyle.Fixed3D;
            flpPatients.Dock = DockStyle.Fill;
            flpPatients.Location = new Point(0, 80);
            flpPatients.Name = "flpPatients";
            flpPatients.Padding = new Padding(5);
            flpPatients.Size = new Size(1300, 720);
            flpPatients.TabIndex = 0;
            // 
            // panelPagination
            // 
            panelPagination.BackColor = Color.WhiteSmoke;
            panelPagination.Controls.Add(btnPaginationPrev);
            panelPagination.Controls.Add(lblPaginationInfo);
            panelPagination.Controls.Add(btnPaginationNext);
            panelPagination.Dock = DockStyle.Bottom;
            panelPagination.Location = new Point(0, 720);
            panelPagination.Name = "panelPagination";
            panelPagination.Padding = new Padding(10);
            panelPagination.Size = new Size(1300, 38);
            panelPagination.TabIndex = 1;
            // 
            // btnPaginationPrev
            // 
            btnPaginationPrev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPaginationPrev.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            btnPaginationPrev.Location = new Point(1061, 4);
            btnPaginationPrev.Name = "btnPaginationPrev";
            btnPaginationPrev.Size = new Size(67, 38);
            btnPaginationPrev.TabIndex = 0;
            btnPaginationPrev.Text = "◀ Geri";
            btnPaginationPrev.Click += BtnPaginationPrev_Click;
            // 
            // lblPaginationInfo
            // 
            lblPaginationInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPaginationInfo.AutoSize = true;
            lblPaginationInfo.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            lblPaginationInfo.Location = new Point(1134, 13);
            lblPaginationInfo.Name = "lblPaginationInfo";
            lblPaginationInfo.Size = new Size(75, 21);
            lblPaginationInfo.TabIndex = 1;
            lblPaginationInfo.Text = "Sayfa 1 / 1";
            // 
            // btnPaginationNext
            // 
            btnPaginationNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPaginationNext.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            btnPaginationNext.Location = new Point(1220, 4);
            btnPaginationNext.Name = "btnPaginationNext";
            btnPaginationNext.Size = new Size(67, 38);
            btnPaginationNext.TabIndex = 2;
            btnPaginationNext.Text = "İleri ▶";
            btnPaginationNext.Click += BtnPaginationNext_Click;
            // 
            // HomeControl
            // 
            Controls.Add(panelPagination);
            Controls.Add(flpPatients);
            Controls.Add(topPanel);
            Name = "HomeControl";
            Size = new Size(1300, 800);
            Load += HomeControl_Load;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            panelPagination.ResumeLayout(false);
            panelPagination.PerformLayout();
            ResumeLayout(false);
        }
    }
}
