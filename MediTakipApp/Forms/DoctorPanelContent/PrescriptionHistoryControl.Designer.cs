namespace MediTakipApp.Forms.DoctorPanelContent
{
    partial class PrescriptionHistoryControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSelectedPatient;
        private System.Windows.Forms.FlowLayoutPanel flpPrescriptions;
        private System.Windows.Forms.FlowLayoutPanel flpDetails;

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
            lblHeader = new Label();
            lblSelectedPatient = new Label();
            flpPrescriptions = new FlowLayoutPanel();
            flpDetails = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeader.Location = new Point(0, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(1517, 40);
            lblHeader.TabIndex = 3;
            lblHeader.Text = "📜 Geçmiş Reçeteler";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSelectedPatient
            // 
            lblSelectedPatient.Dock = DockStyle.Top;
            lblSelectedPatient.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedPatient.ForeColor = Color.DarkBlue;
            lblSelectedPatient.Location = new Point(0, 40);
            lblSelectedPatient.Name = "lblSelectedPatient";
            lblSelectedPatient.Size = new Size(1517, 30);
            lblSelectedPatient.TabIndex = 2;
            lblSelectedPatient.Text = "👤 Seçilen Hasta: -";
            lblSelectedPatient.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flpPrescriptions
            // 
            flpPrescriptions.AutoScroll = true;
            flpPrescriptions.BackColor = Color.WhiteSmoke;
            flpPrescriptions.Dock = DockStyle.Top;
            flpPrescriptions.Location = new Point(0, 70);
            flpPrescriptions.Name = "flpPrescriptions";
            flpPrescriptions.Padding = new Padding(10);
            flpPrescriptions.Size = new Size(1517, 250);
            flpPrescriptions.TabIndex = 1;
            // 
            // flpDetails
            // 
            flpDetails.AutoScroll = true;
            flpDetails.BackColor = Color.Gainsboro;
            flpDetails.Dock = DockStyle.Fill;
            flpDetails.FlowDirection = FlowDirection.TopDown;
            flpDetails.Location = new Point(0, 320);
            flpDetails.Name = "flpDetails";
            flpDetails.Padding = new Padding(10);
            flpDetails.Size = new Size(1517, 465);
            flpDetails.TabIndex = 0;
            flpDetails.WrapContents = false;
            // 
            // PrescriptionHistoryControl
            // 
            Controls.Add(flpDetails);
            Controls.Add(flpPrescriptions);
            Controls.Add(lblSelectedPatient);
            Controls.Add(lblHeader);
            Name = "PrescriptionHistoryControl";
            Size = new Size(1517, 785);
            Load += PrescriptionHistoryControl_Load;
            ResumeLayout(false);
        }
    }
}