using System;
using System.Windows.Forms;
using MediTakipApp.Forms.DoctorPanelContent;
using MediTakipApp.Utils;
using Timer = System.Windows.Forms.Timer;

namespace MediTakipApp.Forms
{
    public partial class DoctorPanel : Form
    {
        private Button activeButton;

        public DoctorPanel()
        {
            InitializeComponent();
        }

        private void DoctorPanel_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Ana Sayfa";
            LoadControl(new HomeControl());
            lblDoctorName.Text = LoggedUser.FullName;
            SetActiveButton(btnHome);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Ana Sayfa";
            LoadControl(new HomeControl());
            SetActiveButton(btnHome);
        }

        private void btnDrugs_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "İlaç Yaz";
            LoadControl(new DrugsControl());
            SetActiveButton(btnDrugs);
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
            control.Visible = false;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);

            // Fade-in animasyonu  
            Timer timer = new Timer { Interval = 10 };
            float opacity = 0;
            timer.Tick += (s, args) =>
            {
                opacity += 0.05f;
                control.BackColor = Color.FromArgb((int)(opacity * 255), control.BackColor);
                if (opacity >= 1)
                {
                    timer.Stop();
                    control.Visible = true;
                }
            };
            timer.Start();
        }

        private void SetActiveButton(Button button)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = Color.FromArgb(45, 45, 45);
                activeButton.ForeColor = Color.White;
            }
            activeButton = button;
            activeButton.BackColor = Color.FromArgb(76, 175, 80); // Yeşil vurgu  
            activeButton.ForeColor = Color.White;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.H))
            {
                btnHome_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.R))
            {
                btnDrugs_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}