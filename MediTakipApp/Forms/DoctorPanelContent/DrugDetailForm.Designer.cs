namespace MediTakipApp.Forms.DoctorPanelContent
{
    partial class DrugDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblDrugName;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private Label lblDosage;
        private TextBox txtDosage;
        private Label lblUsagePeriod;
        private TextBox txtUsagePeriod;
        private Label lblSpecialInstructions;
        private TextBox txtSpecialInstructions;
        private Button btnAdd;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblDrugName = new Label();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblDosage = new Label();
            txtDosage = new TextBox();
            lblUsagePeriod = new Label();
            txtUsagePeriod = new TextBox();
            lblSpecialInstructions = new Label();
            txtSpecialInstructions = new TextBox();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // lblDrugName
            // 
            lblDrugName.Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold);
            lblDrugName.Location = new Point(20, 20);
            lblDrugName.Name = "lblDrugName";
            lblDrugName.Size = new Size(300, 30);
            lblDrugName.TabIndex = 0;
            lblDrugName.Text = "💊 İlaç Adı";
            // 
            // lblQuantity
            // 
            lblQuantity.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblQuantity.Location = new Point(20, 70);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(80, 25);
            lblQuantity.TabIndex = 1;
            lblQuantity.Text = "Adet:";
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold);
            txtQuantity.Location = new Point(110, 70);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(180, 28);
            txtQuantity.TabIndex = 2;
            txtQuantity.Text = "1";
            // 
            // lblDosage
            // 
            lblDosage.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold);
            lblDosage.Location = new Point(20, 110);
            lblDosage.Name = "lblDosage";
            lblDosage.Size = new Size(250, 25);
            lblDosage.TabIndex = 3;
            lblDosage.Text = "Dozaj (örn: Günde 2 kez):";
            // 
            // txtDosage
            // 
            txtDosage.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold);
            txtDosage.Location = new Point(20, 140);
            txtDosage.Name = "txtDosage";
            txtDosage.Size = new Size(270, 28);
            txtDosage.TabIndex = 4;
            txtDosage.Text = "Günde 2 kez";
            // 
            // lblUsagePeriod
            // 
            lblUsagePeriod.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold);
            lblUsagePeriod.Location = new Point(20, 180);
            lblUsagePeriod.Name = "lblUsagePeriod";
            lblUsagePeriod.Size = new Size(250, 25);
            lblUsagePeriod.TabIndex = 5;
            lblUsagePeriod.Text = "Kullanım Süresi (örn: 7 gün):";
            // 
            // txtUsagePeriod
            // 
            txtUsagePeriod.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold);
            txtUsagePeriod.Location = new Point(20, 210);
            txtUsagePeriod.Name = "txtUsagePeriod";
            txtUsagePeriod.Size = new Size(270, 28);
            txtUsagePeriod.TabIndex = 6;
            txtUsagePeriod.Text = "7 gün";
            // 
            // lblSpecialInstructions
            // 
            lblSpecialInstructions.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold);
            lblSpecialInstructions.Location = new Point(20, 250);
            lblSpecialInstructions.Name = "lblSpecialInstructions";
            lblSpecialInstructions.Size = new Size(250, 25);
            lblSpecialInstructions.TabIndex = 7;
            lblSpecialInstructions.Text = "Özel Talimatlar (opsiyonel):";
            // 
            // txtSpecialInstructions
            // 
            txtSpecialInstructions.Font = new Font("Bahnschrift SemiBold SemiConden", 10.2F, FontStyle.Bold);
            txtSpecialInstructions.Location = new Point(20, 280);
            txtSpecialInstructions.Multiline = true;
            txtSpecialInstructions.Name = "txtSpecialInstructions";
            txtSpecialInstructions.Size = new Size(270, 60);
            txtSpecialInstructions.TabIndex = 8;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.MediumSeaGreen;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 360);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(270, 40);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "✅ Ekle";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // DrugDetailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 420);
            Controls.Add(lblDrugName);
            Controls.Add(lblQuantity);
            Controls.Add(txtQuantity);
            Controls.Add(lblDosage);
            Controls.Add(txtDosage);
            Controls.Add(lblUsagePeriod);
            Controls.Add(txtUsagePeriod);
            Controls.Add(lblSpecialInstructions);
            Controls.Add(txtSpecialInstructions);
            Controls.Add(btnAdd);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "DrugDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "İlaç Bilgileri";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
