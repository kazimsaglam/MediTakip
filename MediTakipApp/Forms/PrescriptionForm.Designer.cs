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
            lblPatient = new Label();
            dgvDrugs = new DataGridView();
            dgvSelected = new DataGridView();
            btnCancel = new Button();
            btnSave = new Button();
            dtpDate = new DateTimePicker();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDrugs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSelected).BeginInit();
            SuspendLayout();
            // 
            // lblPatient
            // 
            lblPatient.AutoSize = true;
            lblPatient.Font = new Font("Bahnschrift Condensed", 13.8F, FontStyle.Bold);
            lblPatient.Location = new Point(12, 9);
            lblPatient.Name = "lblPatient";
            lblPatient.Size = new Size(100, 28);
            lblPatient.TabIndex = 1;
            lblPatient.Text = "Hasta Adı ...";
            // 
            // dgvDrugs
            // 
            dgvDrugs.BackgroundColor = SystemColors.ControlLight;
            dgvDrugs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDrugs.Location = new Point(12, 52);
            dgvDrugs.Name = "dgvDrugs";
            dgvDrugs.RowHeadersWidth = 51;
            dgvDrugs.Size = new Size(955, 347);
            dgvDrugs.TabIndex = 2;
            dgvDrugs.CellContentClick += dgvDrugs_CellContentClick;
            // 
            // dgvSelected
            // 
            dgvSelected.BackgroundColor = SystemColors.ControlLight;
            dgvSelected.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSelected.Location = new Point(12, 462);
            dgvSelected.Name = "dgvSelected";
            dgvSelected.RowHeadersWidth = 51;
            dgvSelected.Size = new Size(955, 174);
            dgvSelected.TabIndex = 3;
            dgvSelected.CellContentClick += dgvSelected_CellContentClick;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 654);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(199, 48);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "İptal Et";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(760, 654);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(199, 48);
            btnSave.TabIndex = 5;
            btnSave.Text = "Reçeteyi Yazdır";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(744, 11);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(223, 27);
            dtpDate.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift Condensed", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 420);
            label1.Name = "label1";
            label1.Size = new Size(201, 28);
            label1.TabIndex = 7;
            label1.Text = "Reçeteye Eklenen İlaçlar";
            // 
            // PrescriptionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 714);
            Controls.Add(label1);
            Controls.Add(dtpDate);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(dgvSelected);
            Controls.Add(dgvDrugs);
            Controls.Add(lblPatient);
            Name = "PrescriptionForm";
            Text = "PrescriptionForm";
            Load += PrescriptionForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDrugs).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSelected).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblPatient;
        private DataGridView dgvDrugs;
        private DataGridView dgvSelected;
        private Button btnCancel;
        private Button btnSave;
        private DateTimePicker dtpDate;
        private Label label1;
    }
}