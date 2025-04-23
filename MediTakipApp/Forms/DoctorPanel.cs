using System;
using System.Windows.Forms;
using MediTakipApp.Forms.DoctorPanelContent;

namespace MediTakipApp.Forms
{
    public partial class DoctorPanel : Form
    {
        public DoctorPanel()
        {
            InitializeComponent();
        }

        private void DoctorPanel_Load(object sender, EventArgs e)
        {
            // Başlangıçta Ana Panel göster
            lblTitle.Text = "Ana Sayfa";
            //LoadControl(new HomeControl());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Ana Sayfa";
            //LoadControl(new HomeControl());
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Hastalar";
            LoadControl(new PatientsControl());
        }

        private void btnDrugs_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "İlaçlar";
            LoadControl(new DrugsControl());
        }

        private void btnPrescriptions_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Reçeteler";
            LoadControl(new PrescriptionHistoryControl());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Ayarlar";
            //LoadControl(new SettingsControl());
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Uygulamayı kapatmak istiyor musunuz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnPower_MouseEnter(object sender, EventArgs e)
        {
            btnPower.ForeColor = Color.Red;
            btnPower.BackColor = Color.Red;
        }

        private void btnPower_MouseLeave(object sender, EventArgs e)
        {
            btnPower.ForeColor = Color.White;
            btnPower.BackColor = Color.Transparent;
        }


        private void LoadControl(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);
        }
    }
}
