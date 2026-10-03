using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace LibraryManagementSystem.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<T>(endpoint);
        }

        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync<T>(string endpoint, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);

            return response.IsSuccessStatusCode;
        }

        public async Task<(bool Success, string? ErrorMessage)> DeleteWithMessageAsync(
      string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            string? message = null;

            if (!string.IsNullOrWhiteSpace(body))
            {
                try
                {
                    using var doc = JsonDocument.Parse(body);
                    var root = doc.RootElement;

                    if (root.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var key in new[] { "message", "detail", "title", "error" })
                        {
                            if (root.TryGetProperty(key, out var prop) &&
                                prop.ValueKind == JsonValueKind.String &&
                                !string.IsNullOrWhiteSpace(prop.GetString()))
                            {
                                message = prop.GetString();
                                break;
                            }
                        }
                    }
                    else if (root.ValueKind == JsonValueKind.String)
                    {
                        message = root.GetString();
                    }
                }
                catch (JsonException)
                {
                    
                    message = body.Trim('"');
                }
            }

            message ??= response.StatusCode switch
            {
                HttpStatusCode.Conflict => "This item can't be deleted because it is linked to other records.",
                HttpStatusCode.NotFound => "The item no longer exists.",
                _ => "An error occurred while deleting the item."
            };

            return (false, message);
        }

        public async Task<bool> ReturnBookAsync(string endpoint)
        {
            var response = await _httpClient.PutAsync(endpoint, null);

            return response.IsSuccessStatusCode;
        }
    }
}