namespace MediTakipApp.Forms
{
    partial class DoctorPanel
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMenu;
        private Panel panelProfile;
        private Label lblDoctorName;
        private Label lblDoctorRole;
        private Button btnHome;
        private Button btnDrugs;
        private Button btnPower;
        private Label lblTitle;
        private Panel panelMain;
        private Label lblFooter;

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
            btnPower = new Button();
            btnDrugs = new Button();
            btnHome = new Button();
            panelProfile = new Panel();
            lblDoctorName = new Label();
            lblDoctorRole = new Label();
            lblTitle = new Label();
            panelMain = new Panel();
            panelMenu.SuspendLayout();
            panelProfile.SuspendLayout();
            SuspendLayout();
           
            // panelMenu
            panelMenu.BackColor = Color.FromArgb(45, 45, 45);
            panelMenu.BorderStyle = BorderStyle.Fixed3D;
            panelMenu.Controls.Add(lblFooter);
            panelMenu.Controls.Add(btnPower);
            panelMenu.Controls.Add(btnDrugs);
            panelMenu.Controls.Add(btnHome);
            panelMenu.Controls.Add(panelProfile);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(250, 700);
            panelMenu.TabIndex = 2;

            // lblFooter
            lblFooter.Dock = DockStyle.Bottom;
            lblFooter.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblFooter.ForeColor = Color.LightGray;
            lblFooter.Location = new Point(0, 666);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(246, 30);
            lblFooter.TabIndex = 0;
            lblFooter.Text = "v1.0.0 © 2025 MediTakip";
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;

            // btnPower
            btnPower.Dock = DockStyle.Top;
            btnPower.FlatAppearance.BorderSize = 0;
            btnPower.FlatStyle = FlatStyle.Flat;
            btnPower.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnPower.ForeColor = Color.White;
            btnPower.Location = new Point(0, 360);
            btnPower.Name = "btnPower";
            btnPower.Padding = new Padding(20, 0, 0, 0);
            btnPower.Size = new Size(246, 70);
            btnPower.TabIndex = 1;
            btnPower.Text = "\u23fb Çıkış Yap";
            btnPower.TextAlign = ContentAlignment.MiddleLeft;
            btnPower.Click += btnPower_Click;

            // btnDrugs
            btnDrugs.Dock = DockStyle.Top;
            btnDrugs.FlatAppearance.BorderSize = 0;
            btnDrugs.FlatStyle = FlatStyle.Flat;
            btnDrugs.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnDrugs.ForeColor = Color.White;
            btnDrugs.Location = new Point(0, 150);
            btnDrugs.Name = "btnDrugs";
            btnDrugs.Padding = new Padding(20, 0, 0, 0);
            btnDrugs.Size = new Size(246, 70);
            btnDrugs.TabIndex = 4;
            btnDrugs.Text = "💊 İlaç Yaz";
            btnDrugs.TextAlign = ContentAlignment.MiddleLeft;
            btnDrugs.Click += btnDrugs_Click;

            // btnHome
            btnHome.BackgroundImageLayout = ImageLayout.None;
            btnHome.Dock = DockStyle.Top;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnHome.ForeColor = Color.White;
            btnHome.Location = new Point(0, 80);
            btnHome.Name = "btnHome";
            btnHome.Padding = new Padding(20, 0, 0, 0);
            btnHome.Size = new Size(246, 70);
            btnHome.TabIndex = 6;
            btnHome.Text = "🏠 Ana Sayfa";
            btnHome.TextAlign = ContentAlignment.MiddleLeft;
            btnHome.Click += btnHome_Click;
         
            // panelProfile
            panelProfile.BackColor = Color.FromArgb(55, 55, 55);
            panelProfile.Controls.Add(lblDoctorName);
            panelProfile.Controls.Add(lblDoctorRole);
            panelProfile.Dock = DockStyle.Top;
            panelProfile.Location = new Point(0, 0);
            panelProfile.Name = "panelProfile";
            panelProfile.Padding = new Padding(10);
            panelProfile.Size = new Size(246, 80);
            panelProfile.TabIndex = 7;
        
            // lblDoctorName
            lblDoctorName.Dock = DockStyle.Top;
            lblDoctorName.Font = new Font("Bahnschrift SemiCondensed", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblDoctorName.ForeColor = Color.White;
            lblDoctorName.Location = new Point(10, 33);
            lblDoctorName.Name = "lblDoctorName";
            lblDoctorName.Size = new Size(226, 37);
            lblDoctorName.TabIndex = 1;
            lblDoctorName.Text = "Dr. Kazim Sağlam";
            lblDoctorName.TextAlign = ContentAlignment.MiddleCenter;
         
            // lblDoctorRole
            lblDoctorRole.Dock = DockStyle.Top;
            lblDoctorRole.Font = new Font("Bahnschrift Condensed", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblDoctorRole.ForeColor = Color.LightGray;
            lblDoctorRole.Location = new Point(10, 10);
            lblDoctorRole.Name = "lblDoctorRole";
            lblDoctorRole.Size = new Size(226, 23);
            lblDoctorRole.TabIndex = 2;
            lblDoctorRole.Text = "👨‍⚕️ Doktor";
            lblDoctorRole.TextAlign = ContentAlignment.MiddleCenter;
        
            // lblTitle 
            lblTitle.BorderStyle = BorderStyle.Fixed3D;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Bahnschrift SemiCondensed", 18.2F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(250, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1000, 60);
            lblTitle.TabIndex = 1;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
     
            // panelMain
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(250, 60);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1000, 640);
            panelMain.TabIndex = 0;
    
            // DoctorPanel
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 700);
            Controls.Add(panelMain);
            Controls.Add(lblTitle);
            Controls.Add(panelMenu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DoctorPanel";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediTakip - Doktor";
            WindowState = FormWindowState.Maximized;
            Load += DoctorPanel_Load;
            panelMenu.ResumeLayout(false);
            panelProfile.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
