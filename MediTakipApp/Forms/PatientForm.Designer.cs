namespace MediTakipApp.Forms
{
    partial class PatientForm
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
            btnSave = new Button();
            btnCancel = new Button();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtTcNo = new TextBox();
            dtpBirthDate = new DateTimePicker();
            cmbGender = new ComboBox();
            cmbInsurance = new ComboBox();
            txtCity = new TextBox();
            txtDistrict = new TextBox();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(582, 359);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(195, 66);
            btnSave.TabIndex = 0;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(43, 366);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(211, 59);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "İptal";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(244, 33);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(249, 27);
            txtFirstName.TabIndex = 2;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(244, 66);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(249, 27);
            txtLastName.TabIndex = 3;
            // 
            // txtTcNo
            // 
            txtTcNo.Location = new Point(244, 99);
            txtTcNo.Name = "txtTcNo";
            txtTcNo.Size = new Size(249, 27);
            txtTcNo.TabIndex = 4;
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Location = new Point(244, 143);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(246, 27);
            dtpBirthDate.TabIndex = 5;
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(245, 192);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(246, 28);
            cmbGender.TabIndex = 6;
            // 
            // cmbInsurance
            // 
            cmbInsurance.FormattingEnabled = true;
            cmbInsurance.Location = new Point(245, 233);
            cmbInsurance.Name = "cmbInsurance";
            cmbInsurance.Size = new Size(248, 28);
            cmbInsurance.TabIndex = 7;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(245, 276);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(249, 27);
            txtCity.TabIndex = 8;
            // 
            // txtDistrict
            // 
            txtDistrict.Location = new Point(245, 321);
            txtDistrict.Name = "txtDistrict";
            txtDistrict.Size = new Size(249, 27);
            txtDistrict.TabIndex = 9;
            // 
            // PatientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtDistrict);
            Controls.Add(txtCity);
            Controls.Add(cmbInsurance);
            Controls.Add(cmbGender);
            Controls.Add(dtpBirthDate);
            Controls.Add(txtTcNo);
            Controls.Add(txtLastName);
            Controls.Add(txtFirstName);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Name = "PatientForm";
            Text = "PatientForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSave;
        private Button btnCancel;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtTcNo;
        private DateTimePicker dtpBirthDate;
        private ComboBox cmbGender;
        private ComboBox cmbInsurance;
        private TextBox txtCity;
        private TextBox txtDistrict;
    }
}