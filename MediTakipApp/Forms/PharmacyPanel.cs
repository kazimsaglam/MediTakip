using System;
using System.Windows.Forms;
using MediTakipApp.Forms.PharmacyPanelContent;
using MediTakipApp.Utils;

namespace MediTakipApp.Forms
{
    public partial class PharmacyPanel : Form
    {
        public PharmacyPanel()
        {
            InitializeComponent();
        }

        private void PharmacyPanel_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "🏠 Ana Sayfa";
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new PharmacyHomeControl());

            lblPharmacistName.Text = LoggedUser.FullName;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "🏠 Ana Sayfa";
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new PharmacyHomeControl());
        }

        private void btnPrescriptions_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "📜 Reçeteler";
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new PharmacyPrescriptionsControl());
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "💵 İlaç Satışı";
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new PharmacySellControl());
        }


        private void btnStock_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "💊 Stok Yönetimi";
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new PharmacyStockControl());
        }

        private void btnSupply_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "🚚 Depo / Tedarik";
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new PharmacySupplyControl());
        }

        private void btnLogout_Click(object sender, EventArgs e)
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
    }
}
