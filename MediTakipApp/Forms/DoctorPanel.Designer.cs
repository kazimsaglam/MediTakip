namespace MediTakipApp.Forms
{
    partial class DoctorPanel
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
            dgvPatients = new DataGridView();
            btnAddPatient = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnPrescriptions = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            SuspendLayout();
            // 
            // dgvPatients
            // 
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPatients.Location = new Point(12, 166);
            dgvPatients.MultiSelect = false;
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersWidth = 51;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.Size = new Size(776, 272);
            dgvPatients.TabIndex = 0;
            // 
            // btnAddPatient
            // 
            btnAddPatient.Location = new Point(536, 104);
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Size = new Size(205, 50);
            btnAddPatient.TabIndex = 1;
            btnAddPatient.Text = "Yeni Hasta Ekle";
            btnAddPatient.UseVisualStyleBackColor = true;
            btnAddPatient.Click += btnAddPatient_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(34, 110);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(191, 44);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Hasta Silme";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(312, 101);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(168, 48);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Güncelle";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnPrescriptions
            // 
            btnPrescriptions.Location = new Point(549, 29);
            btnPrescriptions.Name = "btnPrescriptions";
            btnPrescriptions.Size = new Size(194, 50);
            btnPrescriptions.TabIndex = 4;
            btnPrescriptions.Text = "Reçete Yaz 📋";
            btnPrescriptions.UseVisualStyleBackColor = true;
            btnPrescriptions.Click += btnPrescriptions_Click;
            // 
            // DoctorPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPrescriptions);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnAddPatient);
            Controls.Add(dgvPatients);
            Name = "DoctorPanel";
            Text = "DoctorPanel";
            Load += DoctorPanel_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvPatients;
        private Button btnAddPatient;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnPrescriptions;
    }
}