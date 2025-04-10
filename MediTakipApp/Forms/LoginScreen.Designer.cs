namespace MediTakipApp.Forms
{
    partial class LoginScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            btnLogin = new Button();
            txtUsername = new TextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            txtPassword = new TextBox();
            cmbUserType = new ComboBox();
            btnForgot = new Label();
            btnExit = new Label();
            lblError = new Label();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Meditakip_Logo;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 250);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.User_Logo1;
            pictureBox2.Location = new Point(56, 278);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(60, 60);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Padlock_Logo1;
            pictureBox3.Location = new Point(56, 358);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(60, 60);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.DodgerBlue;
            btnLogin.Font = new Font("Palatino Linotype", 24F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLogin.Location = new Point(56, 514);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(305, 72);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Giriş Yap";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.WhiteSmoke;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Palatino Linotype", 13.8F, FontStyle.Bold);
            txtUsername.Location = new Point(130, 293);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Kullanıcı Adı";
            txtUsername.Size = new Size(250, 32);
            txtUsername.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DeepSkyBlue;
            panel1.Location = new Point(130, 336);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 2);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DeepSkyBlue;
            panel2.Location = new Point(130, 415);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 2);
            panel2.TabIndex = 8;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.WhiteSmoke;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Palatino Linotype", 13.8F, FontStyle.Bold);
            txtPassword.Location = new Point(130, 373);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Şifre";
            txtPassword.Size = new Size(250, 32);
            txtPassword.TabIndex = 7;
            // 
            // cmbUserType
            // 
            cmbUserType.Font = new Font("SimSun", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbUserType.FormattingEnabled = true;
            cmbUserType.Location = new Point(130, 447);
            cmbUserType.Name = "cmbUserType";
            cmbUserType.Size = new Size(250, 28);
            cmbUserType.TabIndex = 9;
            // 
            // btnForgot
            // 
            btnForgot.AutoSize = true;
            btnForgot.Location = new Point(150, 618);
            btnForgot.Name = "btnForgot";
            btnForgot.Size = new Size(122, 20);
            btnForgot.TabIndex = 10;
            btnForgot.Text = "Şifremi unuttum?";
            btnForgot.Click += btnForgot_Click;
            // 
            // btnExit
            // 
            btnExit.AutoSize = true;
            btnExit.Location = new Point(189, 647);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(39, 20);
            btnExit.TabIndex = 11;
            btnExit.Text = "Çıkış";
            btnExit.Click += btnExit_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(56, 589);
            lblError.Name = "lblError";
            lblError.Size = new Size(92, 20);
            lblError.TabIndex = 12;
            lblError.Text = "Hata Mesajı!";
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            lblError.Visible = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = Properties.Resources.UserType_Logo2;
            pictureBox4.Location = new Point(16, 433);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(100, 60);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 13;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Meditakip_Logo;
            pictureBox5.Location = new Point(12, 12);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(400, 250);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 1;
            pictureBox5.TabStop = false;
            // 
            // LoginScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(430, 676);
            Controls.Add(lblError);
            Controls.Add(btnExit);
            Controls.Add(btnForgot);
            Controls.Add(cmbUserType);
            Controls.Add(panel2);
            Controls.Add(txtPassword);
            Controls.Add(panel1);
            Controls.Add(txtUsername);
            Controls.Add(btnLogin);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginScreen";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Button btnLogin;
        private TextBox txtUsername;
        private Panel panel1;
        private Panel panel2;
        private TextBox txtPassword;
        private ComboBox cmbUserType;
        private Label btnForgot;
        private Label btnExit;
        private Label lblError;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
    }
}