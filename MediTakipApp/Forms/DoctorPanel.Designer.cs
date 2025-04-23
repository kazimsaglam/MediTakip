using System.Drawing.Text;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace MediTakipApp.Forms
{
    partial class DoctorPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.Button btnDrugs;
        private System.Windows.Forms.Button btnPrescriptions;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnPower;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelMenu = new Panel();
            btnSettings = new Button();
            btnPrescriptions = new Button();
            btnDrugs = new Button();
            btnPatients = new Button();
            btnHome = new Button();
            btnPower = new Button();
            lblTitle = new Label();
            panelMain = new Panel();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(30, 30, 30);
            panelMenu.Controls.Add(btnSettings);
            panelMenu.Controls.Add(btnPrescriptions);
            panelMenu.Controls.Add(btnDrugs);
            panelMenu.Controls.Add(btnPatients);
            panelMenu.Controls.Add(btnHome);
            panelMenu.Controls.Add(btnPower);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(200, 697);
            panelMenu.TabIndex = 2;
            // 
            // btnSettings
            // 
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnSettings.ForeColor = Color.White;
            btnSettings.Location = new Point(0, 240);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(15, 0, 0, 0);
            btnSettings.Size = new Size(200, 60);
            btnSettings.TabIndex = 4;
            btnSettings.Text = "⚙️ Ayarlar";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnPrescriptions
            // 
            btnPrescriptions.Dock = DockStyle.Top;
            btnPrescriptions.FlatAppearance.BorderSize = 0;
            btnPrescriptions.FlatStyle = FlatStyle.Flat;
            btnPrescriptions.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnPrescriptions.ForeColor = Color.White;
            btnPrescriptions.Location = new Point(0, 180);
            btnPrescriptions.Name = "btnPrescriptions";
            btnPrescriptions.Padding = new Padding(15, 0, 0, 0);
            btnPrescriptions.Size = new Size(200, 60);
            btnPrescriptions.TabIndex = 3;
            btnPrescriptions.Text = "📄 Reçeteler";
            btnPrescriptions.TextAlign = ContentAlignment.MiddleLeft;
            btnPrescriptions.Click += btnPrescriptions_Click;
            // 
            // btnDrugs
            // 
            btnDrugs.Dock = DockStyle.Top;
            btnDrugs.FlatAppearance.BorderSize = 0;
            btnDrugs.FlatStyle = FlatStyle.Flat;
            btnDrugs.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnDrugs.ForeColor = Color.White;
            btnDrugs.Location = new Point(0, 120);
            btnDrugs.Name = "btnDrugs";
            btnDrugs.Padding = new Padding(15, 0, 0, 0);
            btnDrugs.Size = new Size(200, 60);
            btnDrugs.TabIndex = 2;
            btnDrugs.Text = "💊 İlaçlar";
            btnDrugs.TextAlign = ContentAlignment.MiddleLeft;
            btnDrugs.Click += btnDrugs_Click;
            // 
            // btnPatients
            // 
            btnPatients.Dock = DockStyle.Top;
            btnPatients.FlatAppearance.BorderSize = 0;
            btnPatients.FlatStyle = FlatStyle.Flat;
            btnPatients.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnPatients.ForeColor = Color.White;
            btnPatients.Location = new Point(0, 60);
            btnPatients.Name = "btnPatients";
            btnPatients.Padding = new Padding(15, 0, 0, 0);
            btnPatients.Size = new Size(200, 60);
            btnPatients.TabIndex = 1;
            btnPatients.Text = "👤 Hastalar";
            btnPatients.TextAlign = ContentAlignment.MiddleLeft;
            btnPatients.Click += btnPatients_Click;
            // 
            // btnHome
            // 
            btnHome.Dock = DockStyle.Top;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            btnHome.ForeColor = Color.White;
            btnHome.Location = new Point(0, 0);
            btnHome.Name = "btnHome";
            btnHome.Padding = new Padding(15, 0, 0, 0);
            btnHome.Size = new Size(200, 60);
            btnHome.TabIndex = 0;
            btnHome.Text = "🏠 Ana Sayfa";
            btnHome.TextAlign = ContentAlignment.MiddleLeft;
            btnHome.Click += btnHome_Click;
            // 
            // btnPower
            // 
            btnPower.BackColor = Color.Transparent;
            btnPower.Dock = DockStyle.Bottom;
            btnPower.FlatAppearance.BorderSize = 0;
            btnPower.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnPower.FlatStyle = FlatStyle.Flat;
            btnPower.Font = new Font("Bahnschrift SemiCondensed", 30F, FontStyle.Bold);
            btnPower.ForeColor = Color.White;
            btnPower.Name = "btnPower";
            btnPower.Size = new Size(200, 100);
            btnPower.TabIndex = 5;
            btnPower.Text = "\u23fb";
            btnPower.UseVisualStyleBackColor = false;
            btnPower.Click += btnPower_Click;
            btnPower.MouseEnter += btnPower_MouseEnter;
            btnPower.MouseLeave += btnPower_MouseLeave;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(200, 0);
            lblTitle.Font = new Font("Bahnschrift SemiCondensed", 16.2F, FontStyle.Bold);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1053, 60);
            lblTitle.TabIndex = 1;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(240, 240, 240);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(200, 60);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1053, 637);
            panelMain.TabIndex = 0;
            // 
            // DoctorPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 697);
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
            ResumeLayout(false);
        }

        #endregion
    }
}