namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class SupplyForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblDrugName;
        private NumericUpDown nudQuantity;
        private DateTimePicker dtpExpiry;
        private ComboBox cmbSupplier;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblDrugName = new Label();
            this.nudQuantity = new NumericUpDown();
            this.dtpExpiry = new DateTimePicker();
            this.cmbSupplier = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)(nudQuantity)).BeginInit();
            this.SuspendLayout();

            // Form
            this.Text = "Tedarik Ekle";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;

            // lblTitle
            lblTitle.Text = "🩺 Tedarik Girişi";
            lblTitle.Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(300, 30);

            // lblDrugName
            lblDrugName.Text = "İlaç: " + drugName;
            lblDrugName.Location = new Point(20, 60);
            lblDrugName.Size = new Size(300, 20);

            // nudQuantity
            nudQuantity.Location = new Point(20, 90);
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 10000;
            nudQuantity.Value = 1;
            nudQuantity.Size = new Size(150, 30);

            // dtpExpiry
            dtpExpiry.Location = new Point(20, 130);
            dtpExpiry.Size = new Size(250, 30);

            // cmbSupplier
            cmbSupplier.Location = new Point(20, 170);
            cmbSupplier.Size = new Size(250, 30);
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

            // btnSave
            btnSave.Text = "💾 Kaydet";
            btnSave.Location = new Point(60, 220);
            btnSave.Size = new Size(100, 35);
            btnSave.Click += BtnSave_Click;

            // btnCancel
            btnCancel.Text = "❌ Vazgeç";
            btnCancel.Location = new Point(180, 220);
            btnCancel.Size = new Size(100, 35);
            btnCancel.Click += BtnCancel_Click;

            // Controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblDrugName);
            this.Controls.Add(nudQuantity);
            this.Controls.Add(dtpExpiry);
            this.Controls.Add(cmbSupplier);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            ((System.ComponentModel.ISupportInitialize)(nudQuantity)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
