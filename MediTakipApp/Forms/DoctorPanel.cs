using System;
using System.Windows.Forms;
using MediTakipApp.Forms.DoctorPanelContent;
using Timer = System.Windows.Forms.Timer;

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
            lblTitle.Text = "Ana Sayfa";
            LoadControl(new HomeControl());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Ana Sayfa";
            LoadControl(new HomeControl());
        }

        private void btnDrugs_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "İlaç Yaz";
            LoadControl(new DrugsControl());
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

        private void LoadControl(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);
        }
    }
}
