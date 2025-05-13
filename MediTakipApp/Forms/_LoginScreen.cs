using MediTakipApp.Utils;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms
{
    public partial class _LoginScreen : Form
    {
        private bool isPasswordVisible = false;
        string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";


        public _LoginScreen()
        {
            InitializeComponent();
            cmbUserType.SelectedIndex = 0;
            txtUsername.Text = null;
            txtPassword.Text = null;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string userType = cmbUserType.SelectedItem?.ToString() ?? "";

            // 🔥 Önce Alan Boşluk Kontrolleri
            if (string.IsNullOrEmpty(userType))
            {
                ShowUserTypeError();
                return;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowErrorState("Kullanıcı adı ve şifre boş bırakılamaz!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p AND UserType=@t";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", password);
                        cmd.Parameters.AddWithValue("@t", userType);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();

                                ResetFields();

                                LoggedUser.Id = Convert.ToInt32(dr["Id"]);
                                LoggedUser.Username = dr["Username"].ToString();
                                LoggedUser.FullName = dr["FullName"].ToString();
                                LoggedUser.Role = dr["UserType"].ToString();

                                if (userType == "Doktor")
                                {
                                    DoctorPanel dp = new DoctorPanel();
                                    dp.Show();
                                }
                                else if (userType == "Eczane")
                                {
                                    PharmacyPanel ep = new PharmacyPanel();
                                    ep.Show();
                                }

                                this.Hide();
                            }
                            else
                            {
                                ShowErrorState("Kullanıcı adı, şifre veya kullanıcı tipi hatalı!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucu hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.RoyalBlue; // Hover rengi
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.DodgerBlue; // Normal rengi
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtPassword.UseSystemPasswordChar = !isPasswordVisible;

            btnTogglePassword.Text = isPasswordVisible ? "🙈" : "👁️"; // 👁️ göster, 🙈 gizle ikonu
        }

        private void ShowErrorState(string message)
        {
            txtUsername.BackColor = Color.MistyRose;
            txtPassword.BackColor = Color.MistyRose;
            cmbUserType.BackColor = Color.White;
            lblError.Text = message;
            lblError.Visible = true;
            ShakeForm();
        }

        private void ShowUserTypeError()
        {
            lblError.Text = "Lütfen kullanıcı tipi seçiniz!";
            lblError.Visible = true;

            cmbUserType.BackColor = Color.MistyRose;
            ShakeForm();
        }

        private void ResetFields()
        {
            txtUsername.BackColor = Color.WhiteSmoke;
            txtPassword.BackColor = Color.WhiteSmoke;
            cmbUserType.BackColor = Color.White;
            lblError.Visible = false;
        }

        private async void ShakeForm()
        {
            var original = this.Location;
            int shakeAmplitude = 10;
            for (int i = 0; i < 6; i++)
            {
                this.Location = new Point(original.X + (i % 2 == 0 ? shakeAmplitude : -shakeAmplitude), original.Y);
                await Task.Delay(50);
            }
            this.Location = original;
        }


        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen yöneticinizle iletişime geçiniz.\n(E-posta reset sistemi entegre edilebilir.)", "Şifremi Unuttum", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LblForgotPassword_MouseEnter(object sender, EventArgs e)
        {
            lblForgotPassword.ForeColor = Color.DeepSkyBlue;
            lblForgotPassword.Font = new Font(lblForgotPassword.Font, FontStyle.Underline);
        }

        private void LblForgotPassword_MouseLeave(object sender, EventArgs e)
        {
            lblForgotPassword.ForeColor = Color.Gray;
            lblForgotPassword.Font = new Font(lblForgotPassword.Font, FontStyle.Regular);
        }


        private void lblExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamadan çıkmak istiyor musunuz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void LblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.Red;
            lblExit.Font = new Font(lblExit.Font, FontStyle.Underline);
        }

        private void LblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.Gray;
            lblExit.Font = new Font(lblExit.Font, FontStyle.Regular);
        }
    }
}
