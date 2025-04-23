namespace MediTakipApp.Forms
{
    partial class PatientForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblTcNo;
        private System.Windows.Forms.TextBox txtTcNo;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.ComboBox cmbInsurance;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblDistrict;
        private System.Windows.Forms.TextBox txtDistrict;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblTcNo = new Label();
            txtTcNo = new TextBox();
            lblInsurance = new Label();
            cmbInsurance = new ComboBox();
            lblBirthDate = new Label();
            dtpBirthDate = new DateTimePicker();
            lblGender = new Label();
            cmbGender = new ComboBox();
            lblCity = new Label();
            txtCity = new TextBox();
            lblDistrict = new Label();
            txtDistrict = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblFirstName
            // 
            lblFirstName.Location = new Point(20, 20);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(100, 23);
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "Ad:";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(150, 20);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(100, 27);
            txtFirstName.TabIndex = 1;
            // 
            // lblLastName
            // 
            lblLastName.Location = new Point(20, 60);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(100, 23);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Soyad:";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(150, 60);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(100, 27);
            txtLastName.TabIndex = 3;
            // 
            // lblTcNo
            // 
            lblTcNo.Location = new Point(20, 100);
            lblTcNo.Name = "lblTcNo";
            lblTcNo.Size = new Size(100, 23);
            lblTcNo.TabIndex = 4;
            lblTcNo.Text = "TC No:";
            // 
            // txtTcNo
            // 
            txtTcNo.Location = new Point(150, 100);
            txtTcNo.Name = "txtTcNo";
            txtTcNo.Size = new Size(100, 27);
            txtTcNo.TabIndex = 5;
            // 
            // lblInsurance
            // 
            lblInsurance.Location = new Point(20, 140);
            lblInsurance.Name = "lblInsurance";
            lblInsurance.Size = new Size(100, 23);
            lblInsurance.TabIndex = 6;
            lblInsurance.Text = "Sigorta:";
            // 
            // cmbInsurance
            // 
            cmbInsurance.Location = new Point(150, 140);
            cmbInsurance.Name = "cmbInsurance";
            cmbInsurance.Size = new Size(121, 28);
            cmbInsurance.TabIndex = 7;
            // 
            // lblBirthDate
            // 
            lblBirthDate.Location = new Point(20, 180);
            lblBirthDate.Name = "lblBirthDate";
            lblBirthDate.Size = new Size(100, 23);
            lblBirthDate.TabIndex = 8;
            lblBirthDate.Text = "Doğum Tarihi:";
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Location = new Point(150, 180);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(200, 27);
            dtpBirthDate.TabIndex = 9;
            // 
            // lblGender
            // 
            lblGender.Location = new Point(20, 220);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(100, 23);
            lblGender.TabIndex = 10;
            lblGender.Text = "Cinsiyet:";
            // 
            // cmbGender
            // 
            cmbGender.Location = new Point(150, 220);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(121, 28);
            cmbGender.TabIndex = 11;
            // 
            // lblCity
            // 
            lblCity.Location = new Point(20, 260);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(100, 23);
            lblCity.TabIndex = 12;
            lblCity.Text = "İl:";
            // 
            // txtCity
            // 
            txtCity.Location = new Point(150, 260);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(100, 27);
            txtCity.TabIndex = 13;
            // 
            // lblDistrict
            // 
            lblDistrict.Location = new Point(20, 300);
            lblDistrict.Name = "lblDistrict";
            lblDistrict.Size = new Size(100, 23);
            lblDistrict.TabIndex = 14;
            lblDistrict.Text = "İlçe:";
            // 
            // txtDistrict
            // 
            txtDistrict.Location = new Point(150, 300);
            txtDistrict.Name = "txtDistrict";
            txtDistrict.Size = new Size(100, 27);
            txtDistrict.TabIndex = 15;
            // 
            // lblPhone
            // 
            lblPhone.Location = new Point(20, 340);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(100, 23);
            lblPhone.TabIndex = 16;
            lblPhone.Text = "Telefon:";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(150, 340);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(100, 27);
            txtPhone.TabIndex = 17;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(226, 390);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(95, 40);
            btnSave.TabIndex = 18;
            btnSave.Text = "Kaydet";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(90, 390);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(95, 40);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "İptal";
            btnCancel.Click += btnCancel_Click;
            // 
            // PatientForm
            // 
            ClientSize = new Size(400, 450);
            Controls.Add(lblFirstName);
            Controls.Add(txtFirstName);
            Controls.Add(lblLastName);
            Controls.Add(txtLastName);
            Controls.Add(lblTcNo);
            Controls.Add(txtTcNo);
            Controls.Add(lblInsurance);
            Controls.Add(cmbInsurance);
            Controls.Add(lblBirthDate);
            Controls.Add(dtpBirthDate);
            Controls.Add(lblGender);
            Controls.Add(cmbGender);
            Controls.Add(lblCity);
            Controls.Add(txtCity);
            Controls.Add(lblDistrict);
            Controls.Add(txtDistrict);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PatientForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hasta Bilgisi";
            Load += PatientForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}