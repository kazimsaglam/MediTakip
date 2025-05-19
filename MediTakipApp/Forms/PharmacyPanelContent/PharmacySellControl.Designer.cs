namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class PharmacySellControl
    {
        /// <summary> 
        /// Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        /// <param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı tarafından üretilen kod

        private System.Windows.Forms.FlowLayoutPanel flpCards;
        private System.Windows.Forms.DataGridView dgvPayment;
        private System.Windows.Forms.Button btnPay;



        private void InitializeComponent()
        {
            flpCards = new FlowLayoutPanel();
            dgvPayment = new DataGridView();
            btnPay = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvPayment).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // flpCards
            // 
            flpCards.AutoScroll = true;
            flpCards.Dock = DockStyle.Left;
            flpCards.Location = new Point(0, 0);
            flpCards.Name = "flpCards";
            flpCards.Padding = new Padding(10);
            flpCards.Size = new Size(929, 800);
            flpCards.TabIndex = 0;
            // 
            // dgvPayment
            // 
            dgvPayment.AllowUserToAddRows = false;
            dgvPayment.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvPayment.BackgroundColor = Color.White;
            dgvPayment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayment.Location = new Point(955, 80);
            dgvPayment.Name = "dgvPayment";
            dgvPayment.RowHeadersWidth = 51;
            dgvPayment.Size = new Size(265, 480);
            dgvPayment.TabIndex = 1;
            dgvPayment.CellContentClick += dgvPayment_CellContentClick;
            // 
            // btnPay
            // 
            btnPay.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPay.BackColor = Color.MediumSeaGreen;
            btnPay.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnPay.ForeColor = Color.White;
            btnPay.Location = new Point(955, 570);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(265, 90);
            btnPay.TabIndex = 2;
            btnPay.Text = "Öde";
            btnPay.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.Odeme_Logo;
            pictureBox1.Location = new Point(955, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(265, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click_1;
            // 
            // PharmacySellControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox1);
            Controls.Add(btnPay);
            Controls.Add(dgvPayment);
            Controls.Add(flpCards);
            Name = "PharmacySellControl";
            Size = new Size(1240, 800);
            ((System.ComponentModel.ISupportInitialize)dgvPayment).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
    }
}
