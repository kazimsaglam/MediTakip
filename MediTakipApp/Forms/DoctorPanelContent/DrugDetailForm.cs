using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace MediTakipApp.Forms.DoctorPanelContent
{
    public partial class DrugDetailForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public int Quantity { get; private set; } = 1;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string Dosage { get; private set; } = "Günde 2 kez";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string UsagePeriod { get; private set; } = "7 gün";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string SpecialInstructions { get; private set; } = "";

        public DrugDetailForm(string drugName)
        {
            InitializeComponent();

            // Varsayılan değerleri textboxlara yaz
            txtDosage.Text = "Günde 2 kez";
            txtUsagePeriod.Text = "7 gün";
            nudQuantity.Value = 1;

            lblDrugName.Text = $"💊 {drugName}";

            // Enter tuşuyla ekleme
            this.AcceptButton = btnAdd;
            this.CancelButton = btnCancel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDosage.Text) || string.IsNullOrWhiteSpace(txtUsagePeriod.Text))
            {
                MessageBox.Show("Dozaj ve kullanım süresi boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Quantity = (int)nudQuantity.Value;
            Dosage = txtDosage.Text.Trim();
            UsagePeriod = txtUsagePeriod.Text.Trim();
            SpecialInstructions = txtSpecialInstructions.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
