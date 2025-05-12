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
            // Ana başlık
            Label lblHeader = new Label()
            {
                Text = "🧑‍⚕️ Hasta Bilgileri",
                Font = new Font("Bahnschrift SemiCondensed", 18F, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Ana panel
            Panel mainPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true,
                BackColor = Color.White
            };

            // Field oluşturucu yardımcı method
            void AddField(Control parent, string labelText, Control inputControl, ref int y)
            {
                Label lbl = new Label()
                {
                    Text = labelText,
                    Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    ForeColor = Color.DimGray,
                    Location = new Point(10, y),
                    Size = new Size(120, 30)
                };
                inputControl.Location = new Point(140, y);
                inputControl.Size = new Size(220, 30);
                inputControl.Font = new Font("Bahnschrift SemiCondensed", 11F);
                parent.Controls.Add(lbl);
                parent.Controls.Add(inputControl);
                y += 45;
            }

            // Alanlar
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtTcNo = new TextBox();
            cmbInsurance = new ComboBox();
            dtpBirthDate = new DateTimePicker();
            cmbGender = new ComboBox();
            txtCity = new TextBox();
            txtDistrict = new TextBox();
            txtPhone = new TextBox();

            // Sigorta ve Cinsiyet combo örnekleri
            cmbInsurance.Items.AddRange(new string[] { "SGK", "Bağ-Kur", "Özel Sigorta", "Diğer", "Yok" });
            cmbGender.Items.AddRange(new string[] { "Erkek", "Kadın", "Diğer" });

            cmbInsurance.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpBirthDate.Format = DateTimePickerFormat.Custom;
            dtpBirthDate.CustomFormat = "dd.MM.yyyy";

            cmbGender.SelectedIndex = 0;

            int yPos = 10;
            AddField(mainPanel, "Ad:", txtFirstName, ref yPos);
            AddField(mainPanel, "Soyad:", txtLastName, ref yPos);
            AddField(mainPanel, "TC Kimlik No:", txtTcNo, ref yPos);
            AddField(mainPanel, "Sigorta Türü:", cmbInsurance, ref yPos);
            AddField(mainPanel, "Doğum Tarihi:", dtpBirthDate, ref yPos);
            AddField(mainPanel, "Cinsiyet:", cmbGender, ref yPos);
            AddField(mainPanel, "İl:", txtCity, ref yPos);
            AddField(mainPanel, "İlçe:", txtDistrict, ref yPos);
            AddField(mainPanel, "Telefon:", txtPhone, ref yPos);

            // Butonlar
            btnSave = new Button()
            {
                Text = "💾 Kaydet",
                Size = new Size(130, 40),
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold),
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(230, yPos + 20)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;

            btnCancel = new Button()
            {
                Text = "❌ İptal",
                Size = new Size(130, 40),
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(50, yPos + 20)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += btnCancel_Click;

            mainPanel.Controls.Add(btnSave);
            mainPanel.Controls.Add(btnCancel);

            // Form ayarları
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(450, 600);
            Controls.Add(mainPanel);
            Controls.Add(lblHeader);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hasta Bilgisi Ekle/Düzenle";
            Load += PatientForm_Load;
        }

    }
}