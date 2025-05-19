using MediTakipApp.Utils;
using MetiDataTsApi;
using MetiDataTsApi.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic; // Add this if not already present
using System.ComponentModel;
using System.Windows.Forms;

namespace MediTakipApp.Forms
{
    public partial class PatientForm : Form
    {


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


        private List<Patient> _allPatients;
        public PatientForm(List<Patient> allPatients)
        {
            InitializeComponent();
            _allPatients = allPatients;
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

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            if (IsUpdateMode)
            {
                var api = new ApiClient();
                var response = await api.UpdatePatient(
                    PatientId.ToString(),
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtTcNo.Text.Trim(),
                    cmbInsurance.SelectedItem?.ToString(),
                    dtpBirthDate.Value.ToString(),
                    cmbGender.SelectedItem?.ToString(),
                    txtCity.Text.Trim(),
                    txtDistrict.Text.Trim(),
                    txtPhone.Text.Trim()
                    );
                if (response.Success)
                {
                    var updatedPatient = new Patient
                    {
                        Id = PatientId,
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        TcNo = txtTcNo.Text.Trim(),
                        Insurance = cmbInsurance.SelectedItem?.ToString(),
                        BirthDate = dtpBirthDate.Value,
                        Gender = cmbGender.SelectedItem?.ToString(),
                        City = txtCity.Text.Trim(),
                        District = txtDistrict.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        DoctorId = DoctorId,
                        LastPrescriptionDate = _allPatients.FirstOrDefault(p => p.Id == PatientId)?.LastPrescriptionDate
                    };

                    var index = _allPatients.FindIndex(p => p.Id == PatientId);
                    if (index >= 0)
                    {
                        _allPatients[index] = updatedPatient;
                    }
                    MessageBox.Show(IsUpdateMode ? "Hasta güncellendi!" : "Hasta eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Güncelleme sırasında hata oluştu: ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                var api = new ApiClient();
                var response = await api.AddPatient(
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtTcNo.Text.Trim(),
                    cmbInsurance.SelectedItem?.ToString(),
                    dtpBirthDate.Value.ToString(),
                    cmbGender.SelectedItem?.ToString(),
                    txtCity.Text.Trim(),
                    txtDistrict.Text.Trim(),
                    txtPhone.Text.Trim(),
                    DoctorId.ToString()
                    );

                if (response.Success)
                {
                    var newPatient = new Patient
                    {
                        Id = response.Data.Id,
                        FirstName = response.Data.FirstName,
                        LastName = response.Data.LastName,
                        TcNo = response.Data.TcNo,
                        Insurance = response.Data.Insurance,
                        BirthDate = response.Data.BirthDate,
                        Gender = response.Data.Gender,
                        City = response.Data.City,
                        District = response.Data.District,
                        Phone = response.Data.Phone,
                        DoctorId = response.Data.DoctorId,
                        LastPrescriptionDate = response.Data.LastPrescriptionDate ?? null
                    };

                    _allPatients.Add(newPatient);
                    MessageBox.Show(IsUpdateMode ? "Hasta güncellendi!" : "Hasta eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Hasta ekleme sırasında hata oluştu: ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
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
                string enteredTc = txtTcNo.Text.Trim();

                bool exists = _allPatients.Any(p => p.TcNo == enteredTc);

                if (exists)
                {
                    MessageBox.Show("Bu TC numarası ile kayıtlı hasta zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
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