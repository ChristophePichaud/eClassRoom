using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Server.Services
{
    public class SimpleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public SimpleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<string>> GetAllQueriesAsync(string token)
        {
            var results = new List<string>();
            var baseUrl = _configuration["BackOfficeUrl"] ?? "http://localhost:5020/";
            var endpoints = new[] { "api/clients", "api/machines", "api/salles", "api/users" };

            foreach (var endpoint in endpoints)
            {
                var result = await SimpleCallAsync(baseUrl, endpoint, token);
                results.Add($"{endpoint}: {result}");
            }
            return results;
        }

        private async Task<string> SimpleCallAsync(string baseUrl, string address, string token)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new System.Uri(baseUrl);
                var request = new HttpRequestMessage(HttpMethod.Get, address);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return $"Status: {response.StatusCode}, Response: {jsonResponse}";
            }
            catch (System.Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}