namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class SupplyForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblDrugName;
        private Label lblQuantity;
        private Label lblExpiry;
        private Label lblSupplier;
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
            lblTitle = new Label();
            lblDrugName = new Label();
            lblQuantity = new Label();
            lblExpiry = new Label();
            lblSupplier = new Label();
            nudQuantity = new NumericUpDown();
            dtpExpiry = new DateTimePicker();
            cmbSupplier = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(350, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "\U0001fa7a Tedarik Girişi";
            // 
            // lblDrugName
            // 
            lblDrugName.Font = new Font("Bahnschrift SemiCondensed", 13F, FontStyle.Bold);
            lblDrugName.Location = new Point(20, 60);
            lblDrugName.Name = "lblDrugName";
            lblDrugName.Size = new Size(360, 24);
            lblDrugName.TabIndex = 1;
            lblDrugName.Text = "İlaç: -";
            // 
            // lblQuantity
            // 
            lblQuantity.Location = new Point(20, 100);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(100, 24);
            lblQuantity.TabIndex = 2;
            lblQuantity.Font = new Font("Bahnschrift", 11F);
            lblQuantity.Text = "🔢 Miktar:";
            // 
            // lblExpiry
            // 
            lblExpiry.Location = new Point(20, 135);
            lblExpiry.Name = "lblExpiry";
            lblExpiry.Size = new Size(100, 24);
            lblExpiry.TabIndex = 4;
            lblExpiry.Font = new Font("Bahnschrift", 11F);
            lblExpiry.Text = "📅 SKT:";
            // 
            // lblSupplier
            // 
            lblSupplier.Location = new Point(20, 170);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.Size = new Size(120, 24);
            lblSupplier.TabIndex = 6;
            lblSupplier.Font = new Font("Bahnschrift", 11F);
            lblSupplier.Text = "🏭 Tedarikçi:";
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Bahnschrift", 11F);
            nudQuantity.Location = new Point(140, 97);
            nudQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(200, 28);
            nudQuantity.TabIndex = 3;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dtpExpiry
            // 
            dtpExpiry.Font = new Font("Bahnschrift", 11F);
            dtpExpiry.Format = DateTimePickerFormat.Short;
            dtpExpiry.Location = new Point(140, 132);
            dtpExpiry.Name = "dtpExpiry";
            dtpExpiry.Size = new Size(200, 28);
            dtpExpiry.TabIndex = 5;
            // 
            // cmbSupplier
            // 
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplier.Font = new Font("Bahnschrift", 11F);
            cmbSupplier.Location = new Point(140, 167);
            cmbSupplier.Name = "cmbSupplier";
            cmbSupplier.Size = new Size(200, 29);
            cmbSupplier.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(70, 220);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 8;
            btnSave.Text = "💾 Kaydet";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.IndianRed;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(210, 220);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "❌ Vazgeç";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // SupplyForm
            // 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Padding = new Padding(10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new Font("Bahnschrift", 16F, FontStyle.Regular);
            ClientSize = new Size(402, 293);
            Controls.Add(lblTitle);
            Controls.Add(lblDrugName);
            Controls.Add(lblQuantity);
            Controls.Add(nudQuantity);
            Controls.Add(lblExpiry);
            Controls.Add(dtpExpiry);
            Controls.Add(lblSupplier);
            Controls.Add(cmbSupplier);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            MaximizeBox = false;
            Name = "SupplyForm";
            Text = "İlaç Tedarik Et";
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ResumeLayout(false);
        }
    }
}
