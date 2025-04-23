namespace MediTakipApp.Forms
{
    partial class PatientsControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flpPatients;
        private System.Windows.Forms.Panel panelPatientDetails;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel topPanel;

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
            this.topPanel = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.flpPatients = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPatientDetails = new System.Windows.Forms.Panel();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();

            this.SuspendLayout();

            // topPanel
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 60;
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.Controls.Add(this.txtSearch);
            this.topPanel.Controls.Add(this.btnAdd);
            this.topPanel.Controls.Add(this.btnUpdate);
            this.topPanel.Controls.Add(this.btnDelete);

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(20, 15);
            this.txtSearch.Size = new System.Drawing.Size(250, 27);
            this.txtSearch.PlaceholderText = "Hasta Ara...";
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(290, 15);
            this.btnAdd.Size = new System.Drawing.Size(120, 30);
            this.btnAdd.Text = "➕ Yeni Hasta";
            this.btnAdd.BackColor = System.Drawing.Color.LightGreen;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddPatient_Click);

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(420, 15);
            this.btnUpdate.Size = new System.Drawing.Size(120, 30);
            this.btnUpdate.Text = "✏️ Güncelle";
            this.btnUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdatePatient_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(550, 15);
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.Text = "🗑️ Sil";
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Click += new System.EventHandler(this.BtnDeletePatient_Click);

            // flpPatients
            this.flpPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPatients.AutoScroll = true;
            this.flpPatients.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpPatients.Padding = new System.Windows.Forms.Padding(10);

            // panelPatientDetails
            this.panelPatientDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPatientDetails.Height = 250;
            this.panelPatientDetails.BackColor = System.Drawing.Color.Gainsboro;
            this.panelPatientDetails.Padding = new System.Windows.Forms.Padding(20);
            this.panelPatientDetails.Controls.Add(this.flowCards);

            // flowCards
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCards.AutoScroll = true;
            this.flowCards.WrapContents = true;
            this.flowCards.FlowDirection = FlowDirection.LeftToRight;
            this.flowCards.Padding = new Padding(10);

            // PatientsControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpPatients);
            this.Controls.Add(this.panelPatientDetails);
            this.Controls.Add(this.topPanel);
            this.Name = "PatientsControl";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.PatientsControl_Load);
            this.ResumeLayout(false);
        }
    }
}