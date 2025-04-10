using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace MediTakipApp.Forms
{
    public partial class PatientForm : Form
    {
        string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool IsUpdateMode { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public int PatientId { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public int DoctorId { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string FirstName { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string LastName { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string TcNo { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public DateTime BirthDate { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string Gender { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string Insurance { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string City { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string District { get; set; }

        public PatientForm()
        {
            InitializeComponent();
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
            cmbGender.Items.AddRange(new string[] { "Erkek", "Kadın" });
            this.cmbInsurance.Items.AddRange(new string[] { "SSK", "Bağ-Kur", "Özel", "Diğer" });

            if (IsUpdateMode)
            {
                txtFirstName.Text = FirstName;
                txtLastName.Text = LastName;
                txtTcNo.Text = TcNo;
                dtpBirthDate.Value = BirthDate;
                cmbGender.SelectedItem = Gender;
                this.cmbInsurance.SelectedItem = Insurance;
                txtCity.Text = City;
                txtDistrict.Text = District;
                btnSave.Text = "Güncelle";
            }
            else
            {
                btnSave.Text = "Kaydet";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Alan kontrolü
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtTcNo.Text) ||
                cmbGender.SelectedItem == null ||
                this.cmbInsurance.SelectedItem == null)
            {
                MessageBox.Show("Lütfen tüm gerekli alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    if (IsUpdateMode)
                    {
                        string updateQuery = @"UPDATE Patients 
                        SET FirstName=@fn, LastName=@ln, TcNo=@tc, BirthDate=@bd, Gender=@g, Insurance=@ins, City=@city, District=@district 
                        WHERE Id=@id";

                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@fn", txtFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ln", txtLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@tc", txtTcNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@bd", dtpBirthDate.Value);
                        cmd.Parameters.AddWithValue("@g", cmbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@ins", this.cmbInsurance.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
                        cmd.Parameters.AddWithValue("@district", txtDistrict.Text.Trim());
                        cmd.Parameters.AddWithValue("@id", PatientId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Hasta başarıyla güncellendi.");
                    }
                    else
                    {
                        string insertQuery = @"INSERT INTO Patients 
                        (FirstName, LastName, TcNo, BirthDate, Gender, Insurance, City, District, DoctorId) 
                        VALUES (@fn, @ln, @tc, @bd, @g, @ins, @city, @district, @docId)";

                        SqlCommand cmd = new SqlCommand(insertQuery, conn);
                        cmd.Parameters.AddWithValue("@fn", txtFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ln", txtLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@tc", txtTcNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@bd", dtpBirthDate.Value);
                        cmd.Parameters.AddWithValue("@g", cmbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@ins", this.cmbInsurance.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
                        cmd.Parameters.AddWithValue("@district", txtDistrict.Text.Trim());
                        cmd.Parameters.AddWithValue("@docId", DoctorId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Hasta başarıyla eklendi.");
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
