using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MetiDataTsApi.Models;

namespace MetiDataTsApi
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://202.61.227.225:5598";

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> LoginAsync(string username, string password, string userType)
        {
            var body = new
            {
                username,
                password,
                userType
            };

            var response = await _httpClient.PostAsync(
                _baseUrl + "/api/auth/login",
                new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            );

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<ListPatientResponse> GetPatientListAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/api/doctor/patient/list");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ListPatientResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new ListPatientResponse
            {
                Success = false,
                Message = "Deserialization returned null.",
                Data = new List<Patient>()
            }; 
        }

        public async Task<DeletePatientResponse> DeletePatient(string patientId)
        {
            var body = new { patientId };

            var jsonBody = JsonSerializer.Serialize(body);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl + "/api/doctor/patient/delete", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<DeletePatientResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new DeletePatientResponse
            {
                Success = false,
                Message = "Deserialization returned null.",
                Data = null
            };
        }

        public async Task<UpdatePatientResponse> UpdatePatient(string Id, string firstName, string lastName, string tcNo, string insurance, string birthDate, string gender, string city, string district, string phone)
        {
            var body = new {  Id,  firstName,  lastName,  tcNo,  insurance,  birthDate,  gender,  city,  district,  phone };

            var jsonBody = JsonSerializer.Serialize(body);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl + "/api/doctor/patient/update", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UpdatePatientResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new UpdatePatientResponse
            {
                Success = false,
                Message = "Deserialization returned null.",
                Data = null
            };
        }

        public async Task<AddPatientResponse> AddPatient(string firstName, string lastName, string tcNo, string insurance, string birthDate, string gender, string city, string district, string phone, string doctorId)
        {
            var body = new { firstName, lastName, tcNo, insurance, birthDate, gender, city, district, phone, doctorId };

            var jsonBody = JsonSerializer.Serialize(body);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl + "/api/doctor/patient/add", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<AddPatientResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new AddPatientResponse
            {
                Success = false,
                Message = "Deserialization returned null.",
                Data = null
            };
        }

        public async Task<ListDrugResponse> GetDrugListAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/api/drug/list");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ListDrugResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new ListDrugResponse
            {
                Success = false,
                Message = "Deserialization returned null.",
                Data = new List<Drug>() 
            };
        }

        public async Task<PrescriptionListResponse> GetPrescriptionListWithPatientIdAsync(string patientId)
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/api/prescription/list/patient/" + patientId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<PrescriptionListResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new PrescriptionListResponse
            {
                Success = false,
                Message = "Deserialization returned null.",
                Data = new List<Prescription>()
            };
        }

        public async Task<PrescriptionByCodeResponse> GetPrescriptionByCodeAsync(string prescriptionCode)
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/api/prescription/list/code/" + prescriptionCode);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<PrescriptionByCodeResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new PrescriptionByCodeResponse
            {
                Success = false,
                Message = "Deserialization returned null.",
                Data = new Prescription()
            };
        }

        public async Task<string> GetDrugStockListAsync()
        {
            return await _httpClient.GetStringAsync(_baseUrl + "/api/drug/stock/list");
        }

        public async Task<string> RecommendDrugsAsync(object requestBody)
        {
            var json = JsonSerializer.Serialize(requestBody);
            var response = await _httpClient.PostAsync(
                _baseUrl + "/api/drug/recommend",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetPrescriptionListAsync()
        {
            return await _httpClient.GetStringAsync(_baseUrl + "/api/prescription/list");
        }

        public async Task<string> CreatePrescriptionAsync(object requestBody)
        {
            var json = JsonSerializer.Serialize(requestBody);
            var response = await _httpClient.PostAsync(
                _baseUrl + "/api/prescription/create",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            return await response.Content.ReadAsStringAsync();
        }
    }
}
