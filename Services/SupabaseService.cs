using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnquiryManagementSystem.Services
{
    public class SupabaseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl;
        private readonly string _supabaseAnonKey;

        public SupabaseService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _supabaseUrl = configuration["Supabase:Url"] ?? "";
            _supabaseAnonKey = configuration["Supabase:AnonKey"] ?? "";
        }

        private void SetAuthHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseAnonKey);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseAnonKey}");
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        public async Task<List<T>> GetAsync<T>(string table, string? select = null)
        {
            try
            {
                SetAuthHeaders();
                var query = $"{_supabaseUrl}/rest/v1/{table}";
                if (!string.IsNullOrEmpty(select))
                    query += $"?select={select}";

                var response = await _httpClient.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                }
                return new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public async Task<T?> GetByIdAsync<T>(string table, string id)
        {
            try
            {
                SetAuthHeaders();
                var response = await _httpClient.GetAsync($"{_supabaseUrl}/rest/v1/{table}?id=eq.{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json);
                    return list?.FirstOrDefault();
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> InsertAsync<T>(string table, T data)
        {
            try
            {
                SetAuthHeaders();
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json);
                var response = await _httpClient.PostAsync($"{_supabaseUrl}/rest/v1/{table}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<T>>(responseJson);
                    return list?.FirstOrDefault();
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<bool> UpdateAsync<T>(string table, string id, T data)
        {
            try
            {
                SetAuthHeaders();
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json);
                var response = await _httpClient.PatchAsync($"{_supabaseUrl}/rest/v1/{table}?id=eq.{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string table, string id)
        {
            try
            {
                SetAuthHeaders();
                var response = await _httpClient.DeleteAsync($"{_supabaseUrl}/rest/v1/{table}?id=eq.{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
