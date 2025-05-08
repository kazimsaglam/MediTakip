namespace MediTakipApp.Forms
{
    partial class _LoginScreen
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox pictureBoxLogo;
        private Panel panelUsername;
        private Panel panelPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Panel panelUsernameLine;
        private Panel panelPasswordLine;
        private Button btnTogglePassword;
        private ComboBox cmbUserType;
        private Button btnLogin;
        private Label lblForgotPassword;
        private Label lblExit;
        private Label lblError;
        private PictureBox pictureBoxUser;
        private PictureBox pictureBoxLock;
        private PictureBox pictureBoxUserType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pictureBoxLogo = new PictureBox();
            txtUsername = new TextBox();
            panelUsernameLine = new Panel();
            panelPasswordLine = new Panel();
            txtPassword = new TextBox();
            cmbUserType = new ComboBox();
            btnLogin = new Button();
            lblForgotPassword = new Label();
            lblExit = new Label();
            lblError = new Label();
            pictureBoxUser = new PictureBox();
            pictureBoxLock = new PictureBox();
            pictureBoxUserType = new PictureBox();
            btnTogglePassword = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserType).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.BackColor = Color.Transparent;
            pictureBoxLogo.BackgroundImageLayout = ImageLayout.None;
            pictureBoxLogo.Image = Properties.Resources.Meditakip_Logo;
            pictureBoxLogo.Location = new Point(1, -2);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(430, 250);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.WhiteSmoke;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Bahnschrift Condensed", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtUsername.Location = new Point(100, 279);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Kullanıcı Adı";
            txtUsername.Size = new Size(250, 33);
            txtUsername.TabIndex = 3;
            // 
            // panelUsernameLine
            // 
            panelUsernameLine.BackColor = Color.DeepSkyBlue;
            panelUsernameLine.Location = new Point(100, 318);
            panelUsernameLine.Name = "panelUsernameLine";
            panelUsernameLine.Size = new Size(250, 2);
            panelUsernameLine.TabIndex = 4;
            // 
            // panelPasswordLine
            // 
            panelPasswordLine.BackColor = Color.DeepSkyBlue;
            panelPasswordLine.Location = new Point(100, 398);
            panelPasswordLine.Name = "panelPasswordLine";
            panelPasswordLine.Size = new Size(250, 2);
            panelPasswordLine.TabIndex = 7;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.WhiteSmoke;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Bahnschrift Condensed", 16.2F);
            txtPassword.Location = new Point(100, 359);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Şifre";
            txtPassword.Size = new Size(250, 33);
            txtPassword.TabIndex = 6;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // cmbUserType
            // 
            cmbUserType.BackColor = SystemColors.Window;
            cmbUserType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUserType.Font = new Font("Bahnschrift Condensed", 16.2F);
            cmbUserType.Items.AddRange(new object[] { "Doktor", "Eczane" });
            cmbUserType.Location = new Point(100, 432);
            cmbUserType.Name = "cmbUserType";
            cmbUserType.Size = new Size(250, 41);
            cmbUserType.TabIndex = 9;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.DodgerBlue;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Bahnschrift", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(100, 493);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(250, 50);
            btnLogin.TabIndex = 10;
            btnLogin.Text = "Giriş Yap";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            btnLogin.MouseEnter += btnLogin_MouseEnter;
            btnLogin.MouseLeave += btnLogin_MouseLeave;
            // 
            // lblForgotPassword
            // 
            lblForgotPassword.AutoSize = true;
            lblForgotPassword.Font = new Font("Bahnschrift Condensed", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblForgotPassword.ForeColor = Color.Gray;
            lblForgotPassword.Location = new Point(111, 580);
            lblForgotPassword.Name = "lblForgotPassword";
            lblForgotPassword.Size = new Size(125, 24);
            lblForgotPassword.TabIndex = 11;
            lblForgotPassword.Text = "Şifremi unuttum?";
            lblForgotPassword.Click += lblForgotPassword_Click;
            lblForgotPassword.MouseEnter += LblForgotPassword_MouseEnter;
            lblForgotPassword.MouseLeave += LblForgotPassword_MouseLeave;
            // 
            // lblExit
            // 
            lblExit.AutoSize = true;
            lblExit.Font = new Font("Bahnschrift Condensed", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblExit.ForeColor = Color.Gray;
            lblExit.Location = new Point(299, 580);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(41, 24);
            lblExit.TabIndex = 12;
            lblExit.Text = "Çıkış";
            lblExit.Click += lblExit_Click;
            lblExit.MouseEnter += LblExit_MouseEnter;
            lblExit.MouseLeave += LblExit_MouseLeave;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(100, 546);
            lblError.Name = "lblError";
            lblError.Size = new Size(231, 20);
            lblError.TabIndex = 13;
            lblError.Text = "Hatalı giriş! Lütfen tekrar deneyin.";
            lblError.Visible = false;
            // 
            // pictureBoxUser
            // 
            pictureBoxUser.Image = Properties.Resources.User_Logo;
            pictureBoxUser.Location = new Point(40, 273);
            pictureBoxUser.Name = "pictureBoxUser";
            pictureBoxUser.Size = new Size(50, 50);
            pictureBoxUser.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUser.TabIndex = 2;
            pictureBoxUser.TabStop = false;
            // 
            // pictureBoxLock
            // 
            pictureBoxLock.Image = Properties.Resources.Padlock_Logo;
            pictureBoxLock.Location = new Point(40, 353);
            pictureBoxLock.Name = "pictureBoxLock";
            pictureBoxLock.Size = new Size(50, 50);
            pictureBoxLock.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLock.TabIndex = 5;
            pictureBoxLock.TabStop = false;
            // 
            // pictureBoxUserType
            // 
            pictureBoxUserType.Image = Properties.Resources.UserType_Logo;
            pictureBoxUserType.Location = new Point(40, 423);
            pictureBoxUserType.Name = "pictureBoxUserType";
            pictureBoxUserType.Size = new Size(50, 50);
            pictureBoxUserType.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUserType.TabIndex = 8;
            pictureBoxUserType.TabStop = false;
            // 
            // btnTogglePassword
            // 
            btnTogglePassword.BackColor = Color.WhiteSmoke;
            btnTogglePassword.Cursor = Cursors.Hand;
            btnTogglePassword.FlatAppearance.BorderSize = 0;
            btnTogglePassword.FlatStyle = FlatStyle.Flat;
            btnTogglePassword.Location = new Point(356, 363);
            btnTogglePassword.Name = "btnTogglePassword";
            btnTogglePassword.Size = new Size(40, 40);
            btnTogglePassword.TabIndex = 7;
            btnTogglePassword.Text = "👁️";
            btnTogglePassword.UseVisualStyleBackColor = false;
            btnTogglePassword.Click += BtnTogglePassword_Click;
            // 
            // LoginScreen
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(430, 620);
            Controls.Add(pictureBoxLogo);
            Controls.Add(pictureBoxUser);
            Controls.Add(txtUsername);
            Controls.Add(panelUsernameLine);
            Controls.Add(pictureBoxLock);
            Controls.Add(txtPassword);
            Controls.Add(btnTogglePassword);
            Controls.Add(panelPasswordLine);
            Controls.Add(pictureBoxUserType);
            Controls.Add(cmbUserType);
            Controls.Add(btnLogin);
            Controls.Add(lblForgotPassword);
            Controls.Add(lblExit);
            Controls.Add(lblError);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediTakip Giriş";
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserType).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
