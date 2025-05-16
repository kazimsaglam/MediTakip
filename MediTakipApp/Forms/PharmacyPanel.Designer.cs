namespace MediTakipApp.Forms
{
    partial class PharmacyPanel
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMenu;
        private Label lblFooter;
        private Button btnLogout;
        private Button btnStockAndSupply;
        private Button btnSell;
        private Button btnPrescriptions;
        private Button btnHome;
        private Panel panelProfile;
        private Label lblPharmacistName;
        private Label lblPharmacistRole;
        private Label lblTitle;
        private Panel panelMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelMenu = new Panel();
            lblFooter = new Label();
            btnLogout = new Button();
            btnStockAndSupply = new Button();
            btnSell = new Button();
            btnPrescriptions = new Button();
            btnHome = new Button();
            panelProfile = new Panel();
            lblPharmacistName = new Label();
            lblPharmacistRole = new Label();
            lblTitle = new Label();
            panelMain = new Panel();
            panelMenu.SuspendLayout();
            panelProfile.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(25, 42, 86);
            panelMenu.BorderStyle = BorderStyle.Fixed3D;
            panelMenu.Controls.Add(lblFooter);
            panelMenu.Controls.Add(btnLogout);
            panelMenu.Controls.Add(btnStockAndSupply);
            panelMenu.Controls.Add(btnSell);
            panelMenu.Controls.Add(btnPrescriptions);
            panelMenu.Controls.Add(btnHome);
            panelMenu.Controls.Add(panelProfile);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(250, 700);
            panelMenu.TabIndex = 0;
            // 
            // lblFooter
            // 
            lblFooter.Dock = DockStyle.Bottom;
            lblFooter.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblFooter.ForeColor = Color.LightSteelBlue;
            lblFooter.Location = new Point(0, 666);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(246, 30);
            lblFooter.TabIndex = 0;
            lblFooter.Text = "v1.0.0 © 2025 MediTakip";
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(25, 42, 86);
            btnLogout.Dock = DockStyle.Top;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 430);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(20, 0, 0, 0);
            btnLogout.Size = new Size(246, 70);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "\u23fb Çıkış Yap";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnStockAndSupply
            // 
            btnStockAndSupply.BackColor = Color.FromArgb(25, 42, 86);
            btnStockAndSupply.Dock = DockStyle.Top;
            btnStockAndSupply.FlatAppearance.BorderSize = 0;
            btnStockAndSupply.FlatStyle = FlatStyle.Flat;
            btnStockAndSupply.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnStockAndSupply.ForeColor = Color.White;
            btnStockAndSupply.Location = new Point(0, 360);
            btnStockAndSupply.Name = "btnStockAndSupply";
            btnStockAndSupply.Padding = new Padding(20, 0, 0, 0);
            btnStockAndSupply.Size = new Size(246, 70);
            btnStockAndSupply.TabIndex = 2;
            btnStockAndSupply.Text = "📦 Stok / Tedarik";
            btnStockAndSupply.TextAlign = ContentAlignment.MiddleLeft;
            btnStockAndSupply.UseVisualStyleBackColor = false;
            btnStockAndSupply.Click += btnStockAndSupply_Click;
            // 
            // btnSell
            // 
            btnSell.BackColor = Color.FromArgb(25, 42, 86);
            btnSell.Dock = DockStyle.Top;
            btnSell.FlatAppearance.BorderSize = 0;
            btnSell.FlatStyle = FlatStyle.Flat;
            btnSell.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnSell.ForeColor = Color.White;
            btnSell.Location = new Point(0, 220);
            btnSell.Name = "btnSell";
            btnSell.Padding = new Padding(20, 0, 0, 0);
            btnSell.Size = new Size(246, 70);
            btnSell.TabIndex = 2;
            btnSell.Text = "💵 İlaç Sat";
            btnSell.TextAlign = ContentAlignment.MiddleLeft;
            btnSell.UseVisualStyleBackColor = false;
            btnSell.Click += btnSell_Click;
            // 
            // btnPrescriptions
            // 
            btnPrescriptions.BackColor = Color.FromArgb(25, 42, 86);
            btnPrescriptions.Dock = DockStyle.Top;
            btnPrescriptions.FlatAppearance.BorderSize = 0;
            btnPrescriptions.FlatStyle = FlatStyle.Flat;
            btnPrescriptions.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnPrescriptions.ForeColor = Color.White;
            btnPrescriptions.Location = new Point(0, 150);
            btnPrescriptions.Name = "btnPrescriptions";
            btnPrescriptions.Padding = new Padding(20, 0, 0, 0);
            btnPrescriptions.Size = new Size(246, 70);
            btnPrescriptions.TabIndex = 4;
            btnPrescriptions.Text = "📜 Reçeteler";
            btnPrescriptions.TextAlign = ContentAlignment.MiddleLeft;
            btnPrescriptions.UseVisualStyleBackColor = false;
            btnPrescriptions.Click += btnPrescriptions_Click;
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.FromArgb(25, 42, 86);
            btnHome.Dock = DockStyle.Top;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnHome.ForeColor = Color.White;
            btnHome.Location = new Point(0, 80);
            btnHome.Name = "btnHome";
            btnHome.Padding = new Padding(20, 0, 0, 0);
            btnHome.Size = new Size(246, 70);
            btnHome.TabIndex = 5;
            btnHome.Text = "🏠 Ana Sayfa";
            btnHome.TextAlign = ContentAlignment.MiddleLeft;
            btnHome.UseVisualStyleBackColor = false;
            btnHome.Click += btnHome_Click;
            // 
            // panelProfile
            // 
            panelProfile.BackColor = Color.FromArgb(30, 55, 100);
            panelProfile.Controls.Add(lblPharmacistName);
            panelProfile.Controls.Add(lblPharmacistRole);
            panelProfile.Dock = DockStyle.Top;
            panelProfile.Location = new Point(0, 0);
            panelProfile.Name = "panelProfile";
            panelProfile.Padding = new Padding(10);
            panelProfile.Size = new Size(246, 80);
            panelProfile.TabIndex = 6;
            // 
            // lblPharmacistName
            // 
            lblPharmacistName.Dock = DockStyle.Top;
            lblPharmacistName.Font = new Font("Bahnschrift SemiCondensed", 13.8F, FontStyle.Bold);
            lblPharmacistName.ForeColor = Color.White;
            lblPharmacistName.Location = new Point(10, 33);
            lblPharmacistName.Name = "lblPharmacistName";
            lblPharmacistName.Size = new Size(226, 37);
            lblPharmacistName.TabIndex = 0;
            lblPharmacistName.Text = "Ecz. Ahmet";
            lblPharmacistName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPharmacistRole
            // 
            lblPharmacistRole.Dock = DockStyle.Top;
            lblPharmacistRole.Font = new Font("Bahnschrift Condensed", 12F, FontStyle.Bold);
            lblPharmacistRole.ForeColor = Color.LightGray;
            lblPharmacistRole.Location = new Point(10, 10);
            lblPharmacistRole.Name = "lblPharmacistRole";
            lblPharmacistRole.Size = new Size(226, 23);
            lblPharmacistRole.TabIndex = 1;
            lblPharmacistRole.Text = "💊 Eczacı";
            lblPharmacistRole.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.BorderStyle = BorderStyle.FixedSingle;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Bahnschrift SemiCondensed", 18.2F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(250, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1000, 60);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Ana Sayfa";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(250, 60);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1000, 640);
            panelMain.TabIndex = 0;
            // 
            // PharmacyPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 700);
            Controls.Add(panelMain);
            Controls.Add(lblTitle);
            Controls.Add(panelMenu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PharmacyPanel";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ana Sayfa";
            WindowState = FormWindowState.Maximized;
            Load += PharmacyPanel_Load;
            panelMenu.ResumeLayout(false);
            panelProfile.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
