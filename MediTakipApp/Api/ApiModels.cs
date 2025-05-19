using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MetiDataTsApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TcNo { get; set; } = string.Empty;
        public string Insurance { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.MinValue;
        public string Gender { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int DoctorId { get; set; }
        public DateTime? LastPrescriptionDate { get; set; } = DateTime.MinValue;
    }

    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public string PrescriptionCode { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public DateTime PrescriptionDate { get; set; }

        public List<PrescriptionDetail> Details { get; set; } = new List<PrescriptionDetail>();
    }

    public class PrescriptionDetail
    {
        public int DetailId { get; set; }
        public int DrugId { get; set; }
        public string DrugName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string UsagePeriod { get; set; } = string.Empty;
        public string SpecialInstructions { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
    public class Drug
    {
        public int Id { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ActiveIngredient { get; set; } = string.Empty;
        public string UsageAge { get; set; } = string.Empty;
        public int? Price { get; set; } = null;
        public bool? IsPrescription { get; set; } = null;
        public string Description { get; set; } = string.Empty;
        public bool? IsActive { get; set; }= null;
        public int TotalStock { get; set; }
    }

    public class ListPatientResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("messsage")]
        public string Message { get; set; } = string.Empty;

        public List<Patient> Data { get; set; } = new List<Patient>();
    }


    public class DeletePatientResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("messsage")]
        public string Message { get; set; } = string.Empty;

        public Patient? Data { get; set; } = null;
    }

    public class UpdatePatientResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("messsage")]
        public string Message { get; set; } = string.Empty;

        public Patient? Data { get; set; } = null;
    }

    public class AddPatientResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("messsage")]
        public string Message { get; set; } = string.Empty;

        public Patient? Data { get; set; } = null;
    }

    public class ListDrugResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("messsage")]
        public string Message { get; set; } = string.Empty;

        public List<Drug> Data { get; set; } = new List<Drug>();
    }

    public class PrescriptionListResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("messsage")]
        public string Message { get; set; } = string.Empty;

        public List<Prescription> Data { get; set; } = new List<Prescription>();
    }

    public class PrescriptionByCodeResponse
    {
        public bool Success { get; set; }

        [JsonPropertyName("messsage")]
        public string Message { get; set; } = string.Empty;

        public Prescription Data { get; set; } = new Prescription();
    }

    
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
