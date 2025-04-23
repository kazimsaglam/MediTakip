using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms
{
    public partial class LoginScreen : Form
    {
        string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public LoginScreen()
        {
            InitializeComponent();
            cmbUserType.Items.AddRange(new string[] { "Doktor", "Eczane" });
            cmbUserType.SelectedIndex = 0;

            txtUsername.Text = null;
            txtPassword.Text = null;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string userType = cmbUserType.SelectedItem.ToString();

            if (username == "" || password == "")
            {
                lblError.Text = "Kullanıcı adı ve şifre boş olamaz!";
                lblError.Visible = true;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p AND UserType=@t";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@t", userType);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblError.Visible = false;

                    if (userType == "Doktor")
                    {
                        LoggedUser.Id = Convert.ToInt32(dr["Id"]); // 🔥 Doktor ID'si kaydediliyor
                        LoggedUser.Username = dr["Username"].ToString();

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
                    lblError.Text = "Kullanıcı adı, şifre veya kullanıcı tipi hatalı!";
                    lblError.Visible = true;
                }
                dr.Close();
            }
        }

        private void btnForgot_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen yöneticinizle iletişime geçiniz.\n(E-posta reset sistemi entegre edilebilir.)", "Şifremi Unuttum", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamadan çıkmak istiyor musunuz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }  
    }
}
