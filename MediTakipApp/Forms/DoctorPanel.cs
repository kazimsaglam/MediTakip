using System;
using System.Windows.Forms;
using MediTakipApp.Forms.DoctorPanelContent;
using MediTakipApp.Utils;
using Timer = System.Windows.Forms.Timer;

namespace MediTakipApp.Forms
{
    public partial class DoctorPanel : Form
    {
        // Renkler
        private readonly Color menuBlue = Color.FromArgb(25, 42, 86);
        private readonly Color hoverBlue = Color.FromArgb(52, 152, 219);
        private readonly Color activeBlue = Color.FromArgb(0, 122, 204);

        private Panel indicatorPanel;
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

            indicatorPanel = new Panel
            {
                Size = new Size(10, 70),
                BackColor = Color.LimeGreen,
                Location = new Point(0, btnHome.Top),
                Visible = true
            };
            panelMenu.Controls.Add(indicatorPanel);
            indicatorPanel.BringToFront();

            SetHoverEffects(btnHome);
            SetHoverEffects(btnDrugs);
            SetHoverEffects(btnPower);

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
            control.Size = new Size((int)(panelMain.Width * 0.8), (int)(panelMain.Height * 0.8));
            control.Location = new Point(
                panelMain.Width / 2 - control.Width / 2,
                panelMain.Height / 2 - control.Height / 2
            );

            control.Visible = false;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);

            float scale = 0.8f;
            Timer timer = new Timer { Interval = 10 };
            timer.Tick += (s, args) =>
            {
                scale += 0.02f;
                int newWidth = (int)(panelMain.Width * scale);
                int newHeight = (int)(panelMain.Height * scale);

                control.Size = new Size(newWidth, newHeight);
                control.Location = new Point(
                    panelMain.Width / 2 - control.Width / 2,
                    panelMain.Height / 2 - control.Height / 2
                );

                if (scale >= 1f)
                {
                    control.Size = panelMain.Size;
                    control.Location = new Point(0, 0);
                    control.Visible = true;
                    timer.Stop();
                }
            };
            timer.Start();
        }

        private void SetActiveButton(Button button)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = menuBlue;
                activeButton.ForeColor = Color.White;
            }

            activeButton = button;
            activeButton.BackColor = activeBlue;
            activeButton.ForeColor = Color.White;

            indicatorPanel.Top = button.Top;
            indicatorPanel.BringToFront();
        }


        private void SetHoverEffects(Button button)
        {
            button.MouseEnter += (s, e) =>
            {
                if (button != activeButton)
                    button.BackColor = hoverBlue;
            };

            button.MouseLeave += (s, e) =>
            {
                if (button != activeButton)
                    button.BackColor = menuBlue;
            };
        }
    }
}