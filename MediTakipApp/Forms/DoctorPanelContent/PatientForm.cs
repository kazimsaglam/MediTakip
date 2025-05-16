using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace MediTakipApp.Forms
{
    public partial class PatientForm : Form
    {
        private string connStr = @"Server=ROGSTRIX;Database=MediTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool IsUpdateMode { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public int PatientId { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public int DoctorId { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string FirstName { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string LastName { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string TcNo { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string Insurance { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public DateTime BirthDate { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string Gender { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string City { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string District { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string Phone { get; set; }

        public PatientForm()
        {
            InitializeComponent();
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
            if (IsUpdateMode)
            {
                txtFirstName.Text = FirstName;
                txtLastName.Text = LastName;
                txtTcNo.Text = TcNo;
                cmbInsurance.SelectedItem = Insurance;
                dtpBirthDate.Value = BirthDate;
                cmbGender.SelectedItem = Gender;
                txtCity.Text = City;
                txtDistrict.Text = District;
                txtPhone.Text = Phone;
                btnSave.Text = "Güncelle";
            }
            else
            {
                btnSave.Text = "Kaydet";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd;

                if (IsUpdateMode)
                {
                    cmd = new SqlCommand("UPDATE Patients SET FirstName=@fn, LastName=@ln, TcNo=@tc, Insurance=@ins, BirthDate=@bd, Gender=@g, City=@city, District=@district, Phone=@phone WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", PatientId);
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Patients (FirstName, LastName, TcNo, Insurance, BirthDate, Gender, City, District, Phone, DoctorId) VALUES (@fn, @ln, @tc, @ins, @bd, @g, @city, @district, @phone, @docId)", conn);
                    cmd.Parameters.AddWithValue("@docId", DoctorId);
                }

                cmd.Parameters.AddWithValue("@fn", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@ln", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@tc", txtTcNo.Text.Trim());
                cmd.Parameters.AddWithValue("@ins", cmbInsurance.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@bd", dtpBirthDate.Value);
                cmd.Parameters.AddWithValue("@g", cmbGender.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
                cmd.Parameters.AddWithValue("@district", txtDistrict.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());

                cmd.ExecuteNonQuery();

                MessageBox.Show(IsUpdateMode ? "Hasta güncellendi!" : "Hasta eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtTcNo.Text) ||
                cmbInsurance.SelectedItem == null ||
                cmbGender.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtDistrict.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtTcNo.Text.Length != 11 || !long.TryParse(txtTcNo.Text, out _))
            {
                MessageBox.Show("TC Kimlik Numarası 11 haneli olmalıdır.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPhone.Text.Length < 11 || !long.TryParse(txtPhone.Text, out _) ||
                !System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^05\d{9}$"))
            {
                MessageBox.Show("Telefon numarası 05 ile başlamalı ve 11 haneli olmalıdır.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpBirthDate.Value > DateTime.Now)
            {
                MessageBox.Show("Doğum tarihi bugünden ileri olamaz.", "Hatalı Tarih", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsUpdateMode)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Patients WHERE TcNo = @tc", conn);
                    checkCmd.Parameters.AddWithValue("@tc", txtTcNo.Text.Trim());

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Bu TC numarası ile kayıtlı hasta zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}