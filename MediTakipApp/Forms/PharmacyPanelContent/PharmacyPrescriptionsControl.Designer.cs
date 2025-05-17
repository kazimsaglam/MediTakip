namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class PharmacyPrescriptionsControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPrescriptions;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtPrescriptionCode;
        private System.Windows.Forms.DataGridView dgvPrescriptionDetails;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        private void InitializeComponent()
        {
            dgvPrescriptions = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            txtPrescriptionCode = new TextBox();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            btnPay = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvPrescriptions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dgvPrescriptions
            // 
            dgvPrescriptions.AllowUserToAddRows = false;
            dgvPrescriptions.AllowUserToDeleteRows = false;
            dgvPrescriptions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvPrescriptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrescriptions.BackgroundColor = Color.White;
            dgvPrescriptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrescriptions.Location = new Point(10, 86);
            dgvPrescriptions.Name = "dgvPrescriptions";
            dgvPrescriptions.RowHeadersWidth = 51;
            dgvPrescriptions.Size = new Size(898, 176);
            dgvPrescriptions.TabIndex = 0;
            dgvPrescriptions.CellContentClick += dgvPrescriptions_CellContentClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(22, 10);
            txtSearch.Multiline = true;
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "TC kimlik numarası giriniz...";
            txtSearch.Size = new Size(288, 54);
            txtSearch.TabIndex = 1;
            txtSearch.TextAlign = HorizontalAlignment.Center;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.MediumSeaGreen;
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(612, 10);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(114, 54);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Ara";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += BtnSearch_Click;
            // 
            // txtPrescriptionCode
            // 
            txtPrescriptionCode.Location = new Point(330, 10);
            txtPrescriptionCode.Multiline = true;
            txtPrescriptionCode.Name = "txtPrescriptionCode";
            txtPrescriptionCode.PlaceholderText = "Reçete Kodu giriniz...";
            txtPrescriptionCode.Size = new Size(262, 54);
            txtPrescriptionCode.TabIndex = 3;
            txtPrescriptionCode.TextAlign = HorizontalAlignment.Center;
            txtPrescriptionCode.TextChanged += txtPrescriptionCode_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 276);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(898, 411);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(924, 86);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(309, 542);
            dataGridView2.TabIndex = 5;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // btnPay
            // 
            btnPay.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPay.BackColor = Color.MediumSeaGreen;
            btnPay.ForeColor = Color.White;
            btnPay.Location = new Point(924, 640);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(309, 47);
            btnPay.TabIndex = 6;
            btnPay.UseVisualStyleBackColor = false;
            btnPay.Click += btnPay_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.Meditakip_Logo;
            pictureBox1.Location = new Point(924, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(309, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // PharmacyPrescriptionsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox1);
            Controls.Add(btnPay);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(dgvPrescriptions);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(txtPrescriptionCode);
            Name = "PharmacyPrescriptionsControl";
            Size = new Size(1250, 700);
            ((System.ComponentModel.ISupportInitialize)dgvPrescriptions).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button btnPay;
        private PictureBox pictureBox1;
    }
}
