namespace MediTakipApp.Forms.PharmacyPanelContent
{
    partial class SupplyForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblDrugName;
        private Label lblQuantity;
        private Label lblExpiry;
        private Label lblSupplier;

        private TextBox txtBarcode;
        private TextBox txtName;
        private TextBox txtIngredient;
        private TextBox txtUsageAge;
        private CheckBox chkPrescription;
        private TextBox txtPrice;

        private NumericUpDown nudQuantity;
        private DateTimePicker dtpExpiry;
        private ComboBox cmbSupplier;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new Label();
            this.lblDrugName = new Label();
            this.lblQuantity = new Label();
            this.lblExpiry = new Label();
            this.lblSupplier = new Label();

            this.txtBarcode = new TextBox();
            this.txtName = new TextBox();
            this.txtIngredient = new TextBox();
            this.txtUsageAge = new TextBox();
            this.chkPrescription = new CheckBox();
            this.txtPrice = new TextBox();

            this.nudQuantity = new NumericUpDown();
            this.dtpExpiry = new DateTimePicker();
            this.cmbSupplier = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            ((System.ComponentModel.ISupportInitialize)this.nudQuantity).BeginInit();
            this.SuspendLayout();

            // Title
            this.lblTitle.Text = "📦 İlaç Tedarik";
            this.lblTitle.Font = new Font("Bahnschrift SemiCondensed", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Size = new Size(300, 32);

            // Barcode
            this.txtBarcode.Location = new Point(20, 60);
            this.txtBarcode.Size = new Size(340, 28);
            this.txtBarcode.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.txtBarcode.PlaceholderText = "Barkod";

            // Name
            this.txtName.Location = new Point(20, 95);
            this.txtName.Size = new Size(340, 28);
            this.txtName.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.txtName.PlaceholderText = "İlaç Adı";

            // Ingredient
            this.txtIngredient.Location = new Point(20, 130);
            this.txtIngredient.Size = new Size(340, 28);
            this.txtIngredient.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.txtIngredient.PlaceholderText = "Etken Madde";

            // Usage Age
            this.txtUsageAge.Location = new Point(20, 165);
            this.txtUsageAge.Size = new Size(340, 28);
            this.txtUsageAge.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.txtUsageAge.PlaceholderText = "Kullanım Yaşı";

            // Prescription
            this.chkPrescription.Text = "📄 Reçeteli mi?";
            this.chkPrescription.Location = new Point(20, 200);
            this.chkPrescription.Size = new Size(400, 28);
            this.chkPrescription.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);

            // Price
            this.txtPrice.Location = new Point(20, 230);
            this.txtPrice.Size = new Size(340, 28);
            this.txtPrice.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.txtPrice.PlaceholderText = "Fiyat (₺)";

            // Quantity
            this.lblQuantity.Text = "🔢 Miktar";
            this.lblQuantity.Location = new Point(20, 270);
            this.lblQuantity.Size = new Size(100, 24);
            this.lblQuantity.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);

            this.nudQuantity.Location = new Point(140, 267);
            this.nudQuantity.Size = new Size(220, 28);
            this.nudQuantity.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.nudQuantity.Minimum = 1;
            this.nudQuantity.Maximum = 10000;
            this.nudQuantity.Value = 1;

            // Expiry
            this.lblExpiry.Text = "📅 SKT";
            this.lblExpiry.Location = new Point(20, 305);
            this.lblExpiry.Size = new Size(100, 24);
            this.lblExpiry.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);

            this.dtpExpiry.Location = new Point(140, 302);
            this.dtpExpiry.Size = new Size(220, 28);
            this.dtpExpiry.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.dtpExpiry.Format = DateTimePickerFormat.Short;

            // Supplier
            this.lblSupplier.Text = "🏭 Tedarikçi";
            this.lblSupplier.Location = new Point(20, 340);
            this.lblSupplier.Size = new Size(120, 24);
            this.lblSupplier.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);

            this.cmbSupplier.Location = new Point(140, 337);
            this.cmbSupplier.Size = new Size(220, 28);
            this.cmbSupplier.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

            // Buttons
            this.btnSave.Text = "💾 Kaydet";
            this.btnSave.Location = new Point(60, 390);
            this.btnSave.Size = new Size(120, 40);
            this.btnSave.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.btnSave.BackColor = Color.SeaGreen;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "❌ Vazgeç";
            this.btnCancel.Location = new Point(200, 390);
            this.btnCancel.Size = new Size(120, 40);
            this.btnCancel.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.btnCancel.BackColor = Color.IndianRed;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Click += BtnCancel_Click;

            // Form
            this.Text = "İlaç Tedarik Et";
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(390, 460);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Load += SupplyForm_Load;
            FormClosed += SupplyForm_FormClosed;
            this.MaximizeBox = false;
            this.Controls.AddRange(new Control[] {
                lblTitle, txtBarcode, txtName, txtIngredient, txtUsageAge, chkPrescription, txtPrice,
                lblQuantity, nudQuantity, lblExpiry, dtpExpiry, lblSupplier, cmbSupplier,
                btnSave, btnCancel
            });
            this.StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)this.nudQuantity).EndInit();
            this.ResumeLayout(false);
        }
    }
}
