namespace MediTakipApp.Forms.DoctorPanelContent
{
    partial class DrugsControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flpDrugs;
        private System.Windows.Forms.FlowLayoutPanel flpSelectedDrugs;
        private System.Windows.Forms.Button btnSavePrescription;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;

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
            this.components = new System.ComponentModel.Container();
            this.flpDrugs = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSelectedDrugs = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSavePrescription = new System.Windows.Forms.Button();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();

            this.SuspendLayout();

            // topPanel
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 80;
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.Controls.Add(this.lblPatientInfo);
            this.topPanel.Controls.Add(this.lblDiagnosis);
            this.topPanel.Controls.Add(this.txtDiagnosis);
            this.topPanel.Controls.Add(this.lblSearch);
            this.topPanel.Controls.Add(this.txtSearch);

            // lblPatientInfo
            this.lblPatientInfo.Text = "Seçilen Hasta: -";
            this.lblPatientInfo.Location = new System.Drawing.Point(10, 10);
            this.lblPatientInfo.Size = new System.Drawing.Size(400, 25);
            this.lblPatientInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPatientInfo.ForeColor = System.Drawing.Color.DarkBlue;

            // lblDiagnosis
            this.lblDiagnosis.Text = "Teşhis:";
            this.lblDiagnosis.Location = new System.Drawing.Point(10, 45);
            this.lblDiagnosis.Size = new System.Drawing.Size(60, 25);

            // txtDiagnosis
            this.txtDiagnosis.Location = new System.Drawing.Point(70, 45);
            this.txtDiagnosis.Size = new System.Drawing.Size(300, 27);

            // lblSearch
            this.lblSearch.Text = "İlaç Ara:";
            this.lblSearch.Location = new System.Drawing.Point(400, 45);
            this.lblSearch.Size = new System.Drawing.Size(65, 25);

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(470, 45);
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // flpDrugs
            this.flpDrugs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDrugs.AutoScroll = true;
            this.flpDrugs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpDrugs.Padding = new System.Windows.Forms.Padding(10);
            this.flpDrugs.WrapContents = true;
            this.flpDrugs.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;

            // bottomPanel
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Height = 220;
            this.bottomPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(10);
            this.bottomPanel.Controls.Add(this.flpSelectedDrugs);
            this.bottomPanel.Controls.Add(this.btnSavePrescription);

            // flpSelectedDrugs
            this.flpSelectedDrugs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSelectedDrugs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSelectedDrugs.AutoScroll = true;
            this.flpSelectedDrugs.WrapContents = false;
            this.flpSelectedDrugs.Padding = new System.Windows.Forms.Padding(10);

            // btnSavePrescription
            this.btnSavePrescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSavePrescription.Height = 40;
            this.btnSavePrescription.Text = "💾 Reçeteyi Kaydet";
            this.btnSavePrescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePrescription.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSavePrescription.ForeColor = System.Drawing.Color.White;
            this.btnSavePrescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePrescription.Click += new System.EventHandler(this.BtnSavePrescription_Click);

            // DrugsControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpDrugs);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "DrugsControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.DrugsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
