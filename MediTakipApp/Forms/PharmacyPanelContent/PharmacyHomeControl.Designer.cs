namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class PharmacyHomeControl
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Label lblTitle;
        private Button btnShowReport;
        private DateTimePicker dtpReportDate;
        private Label lblDate;

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
            this.panelMain = new Panel();
            this.lblTitle = new Label();
            this.btnShowReport = new Button();
            this.dtpReportDate = new DateTimePicker();
            this.lblDate = new Label();

            this.SuspendLayout();

            // panelMain
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.White;
            this.panelMain.Padding = new Padding(20);
            this.Controls.Add(this.panelMain);

            // lblTitle
            this.lblTitle.Text = "📊 Günlük Z Raporu";
            this.lblTitle.Font = new Font("Bahnschrift SemiCondensed", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.MidnightBlue;
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.AutoSize = true;
            this.panelMain.Controls.Add(this.lblTitle);

            // lblDate
            this.lblDate.Text = "📅 Tarih:";
            this.lblDate.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.lblDate.Location = new Point(20, 70);
            this.lblDate.AutoSize = true;
            this.panelMain.Controls.Add(this.lblDate);

            // dtpReportDate
            this.dtpReportDate.Format = DateTimePickerFormat.Short;
            this.dtpReportDate.Location = new Point(100, 66);
            this.dtpReportDate.Width = 120;
            this.panelMain.Controls.Add(this.dtpReportDate);

            // btnShowReport
            this.btnShowReport.Text = "📥 Raporu Göster";
            this.btnShowReport.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold);
            this.btnShowReport.BackColor = Color.MediumSlateBlue;
            this.btnShowReport.ForeColor = Color.White;
            this.btnShowReport.FlatStyle = FlatStyle.Flat;
            this.btnShowReport.FlatAppearance.BorderSize = 0;
            this.btnShowReport.Location = new Point(240, 64);
            this.btnShowReport.Size = new Size(150, 30);
            this.panelMain.Controls.Add(this.btnShowReport);

            // PharmacyHomeControl
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.Controls.Add(this.panelMain);
            this.Name = "PharmacyHomeControl";
            this.Size = new Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}
