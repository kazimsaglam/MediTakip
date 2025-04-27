using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace MediTakipApp.Forms.DoctorPanelContent
{
    public partial class DrugDetailForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]  public int Quantity { get; private set; } = 1;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]  public string Dosage { get; private set; } = "Günde 2 kez";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]  public string UsagePeriod { get; private set; } = "7 gün";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]  public string SpecialInstructions { get; private set; } = "";

        public DrugDetailForm(string drugName)
        {
            InitializeComponent();
            lblDrugName.Text = $"💊 {drugName}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDosage.Text) || string.IsNullOrWhiteSpace(txtUsagePeriod.Text))
            {
                MessageBox.Show("Dozaj ve kullanım süresi boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Geçerli bir adet girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Quantity = qty;
            Dosage = txtDosage.Text.Trim();
            UsagePeriod = txtUsagePeriod.Text.Trim();
            SpecialInstructions = txtSpecialInstructions.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
