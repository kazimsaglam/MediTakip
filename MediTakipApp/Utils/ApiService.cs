using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MediTakipApp.Utils
{
    public static class ApiService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string baseUrl = "http://202.61.227.225:5598/api/";


        static ApiService()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await client.GetAsync(baseUrl + endpoint);
                if (!response.IsSuccessStatusCode)
                    return default;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GET Error: " + ex.Message);
                return default;
            }
        }

        public static async Task<List<T>> GetListAsync<T>(string endpoint)
        {
            var result = await GetAsync<ApiResult<List<T>>>(endpoint);
            return result?.Data ?? new List<T>();
        }


        public static async Task<T?> PostAsync<T>(string endpoint, object body)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(baseUrl + endpoint, content);
                if (!response.IsSuccessStatusCode)
                    return default;

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("POST Error: " + ex.Message);
                return default;
            }
        }


        public static async Task<T?> DeleteAsync<T>(string endpoint)
        {
            try
            {
                var response = await client.DeleteAsync(baseUrl + endpoint);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DELETE Error: " + ex.Message);
                return default;
            }
        }

    }
}
