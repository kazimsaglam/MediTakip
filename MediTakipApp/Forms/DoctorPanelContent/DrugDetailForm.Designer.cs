namespace MediTakipApp.Forms.DoctorPanelContent
{
    partial class DrugDetailForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblDrugName;
        private NumericUpDown nudQuantity;
        private TextBox txtDosage;
        private TextBox txtUsagePeriod;
        private TextBox txtSpecialInstructions;
        private Button btnAdd;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblDrugName = new Label();
            this.nudQuantity = new NumericUpDown();
            this.txtDosage = new TextBox();
            this.txtUsagePeriod = new TextBox();
            this.txtSpecialInstructions = new TextBox();
            this.btnAdd = new Button();
            this.btnCancel = new Button();

            // Yeni eklenen label'lar
            Label lblQuantity = new Label();
            Label lblDosage = new Label();
            Label lblUsagePeriod = new Label();
            Label lblInstructions = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();

            // lblDrugName
            this.lblDrugName.Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold);
            this.lblDrugName.Location = new Point(20, 20);
            this.lblDrugName.Size = new Size(350, 35);
            this.lblDrugName.Text = "💊 İlaç Adı";

            // lblQuantity
            lblQuantity.Text = "Adet:";
            lblQuantity.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            lblQuantity.Location = new Point(30, 70);
            lblQuantity.Size = new Size(100, 25);

            this.nudQuantity.Font = new Font("Bahnschrift SemiCondensed", 12F);
            this.nudQuantity.Location = new Point(30, 95);
            this.nudQuantity.Minimum = 1;
            this.nudQuantity.Maximum = 100;
            this.nudQuantity.Size = new Size(320, 30);

            // lblDosage
            lblDosage.Text = "Dozaj:";
            lblDosage.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            lblDosage.Location = new Point(30, 135);
            lblDosage.Size = new Size(100, 25);

            this.txtDosage.Font = new Font("Bahnschrift SemiCondensed", 12F);
            this.txtDosage.Location = new Point(30, 160);
            this.txtDosage.Size = new Size(320, 30);

            // lblUsagePeriod
            lblUsagePeriod.Text = "Kullanım Süresi:";
            lblUsagePeriod.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            lblUsagePeriod.Location = new Point(30, 225);
            lblUsagePeriod.Size = new Size(150, 25);

            this.txtUsagePeriod.Font = new Font("Bahnschrift SemiCondensed", 12F);
            this.txtUsagePeriod.Location = new Point(30, 250);
            this.txtUsagePeriod.Size = new Size(320, 30);

            // lblInstructions
            lblInstructions.Text = "Özel Talimatlar:";
            lblInstructions.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            lblInstructions.Location = new Point(30, 315);
            lblInstructions.Size = new Size(150, 25);

            this.txtSpecialInstructions.Font = new Font("Bahnschrift SemiCondensed", 12F);
            this.txtSpecialInstructions.Location = new Point(30, 340);
            this.txtSpecialInstructions.Multiline = true;
            this.txtSpecialInstructions.Size = new Size(320, 60);
            this.txtSpecialInstructions.PlaceholderText = "Varsa özel talimatlar...";

            // btnAdd
            this.btnAdd.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.btnAdd.BackColor = Color.MediumSeaGreen;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Location = new Point(200, 415);
            this.btnAdd.Size = new Size(150, 40);
            this.btnAdd.Text = "➕ Ekle";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // btnCancel
            this.btnCancel.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.btnCancel.BackColor = Color.IndianRed;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Location = new Point(30, 415);
            this.btnCancel.Size = new Size(150, 40);
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // DrugDetailForm
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 475);
            this.Controls.Add(this.lblDrugName);
            this.Controls.Add(lblQuantity);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(lblDosage);
            this.Controls.Add(this.txtDosage);
            this.Controls.Add(lblUsagePeriod);
            this.Controls.Add(this.txtUsagePeriod);
            this.Controls.Add(lblInstructions);
            this.Controls.Add(this.txtSpecialInstructions);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "İlaç Detayı";

            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

    }
}
