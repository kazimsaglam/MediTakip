namespace MediTakipApp.Forms
{
    partial class PharmacyPanel
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
            dgvPrescriptions = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            dgvDrugsInPrescription = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvPrescriptions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDrugsInPrescription).BeginInit();
            SuspendLayout();
            // 
            // dgvPrescriptions
            // 
            dgvPrescriptions.BackgroundColor = SystemColors.ControlLight;
            dgvPrescriptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrescriptions.Location = new Point(21, 46);
            dgvPrescriptions.MultiSelect = false;
            dgvPrescriptions.Name = "dgvPrescriptions";
            dgvPrescriptions.ReadOnly = true;
            dgvPrescriptions.RowHeadersWidth = 51;
            dgvPrescriptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrescriptions.Size = new Size(917, 214);
            dgvPrescriptions.TabIndex = 0;
            dgvPrescriptions.CellContentClick += dgvPrescriptions_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift Condensed", 13.8F, FontStyle.Bold);
            label1.Location = new Point(27, 9);
            label1.Name = "label1";
            label1.Size = new Size(127, 28);
            label1.TabIndex = 1;
            label1.Text = "Reçete Listesi :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Bahnschrift Condensed", 13.8F, FontStyle.Bold);
            label2.Location = new Point(27, 278);
            label2.Name = "label2";
            label2.Size = new Size(147, 28);
            label2.TabIndex = 3;
            label2.Text = "Reçete Detayları :";
            // 
            // dgvDrugsInPrescription
            // 
            dgvDrugsInPrescription.BackgroundColor = SystemColors.ControlLight;
            dgvDrugsInPrescription.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDrugsInPrescription.Location = new Point(21, 315);
            dgvDrugsInPrescription.MultiSelect = false;
            dgvDrugsInPrescription.Name = "dgvDrugsInPrescription";
            dgvDrugsInPrescription.ReadOnly = true;
            dgvDrugsInPrescription.RowHeadersWidth = 51;
            dgvDrugsInPrescription.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDrugsInPrescription.Size = new Size(917, 214);
            dgvDrugsInPrescription.TabIndex = 2;
            // 
            // PharmacyPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 581);
            Controls.Add(label2);
            Controls.Add(dgvDrugsInPrescription);
            Controls.Add(label1);
            Controls.Add(dgvPrescriptions);
            Name = "PharmacyPanel";
            Text = "PharmacyPanel";
            Load += PharmacyPanel_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPrescriptions).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDrugsInPrescription).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPrescriptions;
        private Label label1;
        private Label label2;
        private DataGridView dgvDrugsInPrescription;
    }
}