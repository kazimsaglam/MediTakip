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
            label1 = new Label();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            label2 = new Label();
            txtTcNo = new TextBox();
            label3 = new Label();
            label4 = new Label();
            cmbInsurance = new ComboBox();
            label5 = new Label();
            dtpBirthDate = new DateTimePicker();
            cmbGender = new ComboBox();
            label6 = new Label();
            txtCity = new TextBox();
            label7 = new Label();
            txtDistrict = new TextBox();
            label8 = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(134, 34);
            label1.Name = "label1";
            label1.Size = new Size(45, 29);
            label1.TabIndex = 0;
            label1.Text = "Ad :";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(196, 38);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(209, 27);
            txtFirstName.TabIndex = 1;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(196, 82);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(209, 27);
            txtLastName.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(107, 80);
            label2.Name = "label2";
            label2.Size = new Size(72, 29);
            label2.TabIndex = 2;
            label2.Text = "Soyad :";
            // 
            // txtTcNo
            // 
            txtTcNo.Location = new Point(196, 125);
            txtTcNo.Name = "txtTcNo";
            txtTcNo.Size = new Size(209, 27);
            txtTcNo.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(108, 123);
            label3.Name = "label3";
            label3.Size = new Size(71, 29);
            label3.TabIndex = 4;
            label3.Text = "Tc No :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(96, 170);
            label4.Name = "label4";
            label4.Size = new Size(83, 29);
            label4.TabIndex = 6;
            label4.Text = "Sigorta :";
            // 
            // cmbInsurance
            // 
            cmbInsurance.FormattingEnabled = true;
            cmbInsurance.Location = new Point(196, 174);
            cmbInsurance.Name = "cmbInsurance";
            cmbInsurance.Size = new Size(209, 28);
            cmbInsurance.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(41, 214);
            label5.Name = "label5";
            label5.Size = new Size(138, 29);
            label5.TabIndex = 8;
            label5.Text = "Doğum Tarihi :";
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Location = new Point(196, 216);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(209, 27);
            dtpBirthDate.TabIndex = 9;
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(196, 258);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(209, 28);
            cmbGender.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(90, 254);
            label6.Name = "label6";
            label6.Size = new Size(89, 29);
            label6.TabIndex = 10;
            label6.Text = "Cinsiyet :";
            // 
            // txtCity
            // 
            txtCity.Location = new Point(196, 302);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(209, 27);
            txtCity.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(144, 302);
            label7.Name = "label7";
            label7.Size = new Size(35, 29);
            label7.TabIndex = 12;
            label7.Text = "İl :";
            // 
            // txtDistrict
            // 
            txtDistrict.Location = new Point(196, 346);
            txtDistrict.Name = "txtDistrict";
            txtDistrict.Size = new Size(209, 27);
            txtDistrict.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Sitka Banner", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(127, 344);
            label8.Name = "label8";
            label8.Size = new Size(52, 29);
            label8.TabIndex = 14;
            label8.Text = "İlçe :";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(34, 404);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(159, 49);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "İptal";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(267, 402);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(179, 52);
            btnSave.TabIndex = 17;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // PatientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 503);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(txtDistrict);
            Controls.Add(label8);
            Controls.Add(txtCity);
            Controls.Add(label7);
            Controls.Add(cmbGender);
            Controls.Add(label6);
            Controls.Add(dtpBirthDate);
            Controls.Add(label5);
            Controls.Add(cmbInsurance);
            Controls.Add(label4);
            Controls.Add(txtTcNo);
            Controls.Add(label3);
            Controls.Add(txtLastName);
            Controls.Add(label2);
            Controls.Add(txtFirstName);
            Controls.Add(label1);
            Name = "PatientForm";
            Text = "PatientForm";
            Load += PatientForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private Label label2;
        private TextBox txtTcNo;
        private Label label3;
        private Label label4;
        private ComboBox cmbInsurance;
        private Label label5;
        private DateTimePicker dtpBirthDate;
        private ComboBox cmbGender;
        private Label label6;
        private TextBox txtCity;
        private Label label7;
        private TextBox txtDistrict;
        private Label label8;
        private Button btnCancel;
        private Button btnSave;
    }
}