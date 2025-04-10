namespace MediTakipApp.Forms
{
    partial class PrescriptionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbPatients = new ComboBox();
            cmbDrugs = new ComboBox();
            txtQuantity = new TextBox();
            txtInstructions = new TextBox();
            btnAddDrug = new Button();
            dgvPrescription = new DataGridView();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPrescription).BeginInit();
            SuspendLayout();
            // 
            // cmbPatients
            // 
            cmbPatients.FormattingEnabled = true;
            cmbPatients.Location = new Point(67, 32);
            cmbPatients.Name = "cmbPatients";
            cmbPatients.Size = new Size(559, 28);
            cmbPatients.TabIndex = 0;
            // 
            // cmbDrugs
            // 
            cmbDrugs.FormattingEnabled = true;
            cmbDrugs.Location = new Point(64, 86);
            cmbDrugs.Name = "cmbDrugs";
            cmbDrugs.Size = new Size(565, 28);
            cmbDrugs.TabIndex = 1;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(66, 143);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(173, 27);
            txtQuantity.TabIndex = 2;
            // 
            // txtInstructions
            // 
            txtInstructions.Location = new Point(63, 193);
            txtInstructions.Name = "txtInstructions";
            txtInstructions.Size = new Size(176, 27);
            txtInstructions.TabIndex = 3;
            // 
            // btnAddDrug
            // 
            btnAddDrug.Location = new Point(565, 260);
            btnAddDrug.Name = "btnAddDrug";
            btnAddDrug.Size = new Size(183, 62);
            btnAddDrug.TabIndex = 4;
            btnAddDrug.Text = "Reçeteye ilaç ekle";
            btnAddDrug.UseVisualStyleBackColor = true;
            btnAddDrug.Click += btnAddDrug_Click;
            // 
            // dgvPrescription
            // 
            dgvPrescription.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrescription.Location = new Point(12, 260);
            dgvPrescription.Name = "dgvPrescription";
            dgvPrescription.RowHeadersWidth = 51;
            dgvPrescription.Size = new Size(412, 179);
            dgvPrescription.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(561, 350);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(193, 82);
            btnSave.TabIndex = 6;
            btnSave.Text = "Reçeteyi Yazdır";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // PrescriptionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(dgvPrescription);
            Controls.Add(btnAddDrug);
            Controls.Add(txtInstructions);
            Controls.Add(txtQuantity);
            Controls.Add(cmbDrugs);
            Controls.Add(cmbPatients);
            Name = "PrescriptionForm";
            Text = "PrescriptionForm";
            Load += PrescriptionForm_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvPrescription).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbPatients;
        private ComboBox cmbDrugs;
        private TextBox txtQuantity;
        private TextBox txtInstructions;
        private Button btnAddDrug;
        private DataGridView dgvPrescription;
        private Button btnSave;
    }
}