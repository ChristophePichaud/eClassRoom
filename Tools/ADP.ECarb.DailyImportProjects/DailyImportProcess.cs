using Microsoft.Extensions.Logging;
using Shared.Dtos;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ADP.Ecarb.DailyImportProjects
{
    public class DailyImportProcess
    {
        public string BackOfficeUrl = String.Empty;
        public string Email = "christophe.pichaud@aurera.fr";
        public string Password = "admin";
        public string token = String.Empty;

        public DailyImportProcess()
        {
        }

        public async Task DoWorkAsync()
        {
            await ConnectAsync();
        }

        private async Task ConnectAsync()
        {
            BackOfficeUrl = ConfigurationManager.AppSettings["BackOfficeUrl"];
            Email = ConfigurationManager.AppSettings["Email"];
            Password = ConfigurationManager.AppSettings["Password"];

            try
            {
                //
                // Login
                //

                // NTLM Secured URL
                string urli = String.Format("{0}api/security/login", BackOfficeUrl);
                var uri = new Uri(urli);
                Logger.Info(String.Format("calling {0}", urli));

                using HttpClientHandler handler = new()
                {
                    Credentials = CredentialCache.DefaultNetworkCredentials, //.DefaultCredentials,
                };
                using HttpClient httpClient0 = new(handler);
                LoginDto loginModel = new LoginDto
                {
                    Username = "admin@samplecorp.com",
                    Password = "admin123"
                };
                httpClient0.BaseAddress = new Uri("http://localhost:5020/");
                var response0 = await httpClient0.PostAsJsonAsync("api/security/login", loginModel);

                //var response0 = await httpClient0.GetAsync(uri);

                string jsonResponse = await response0.Content.ReadAsStringAsync();
                Logger.Info("json Response: " + jsonResponse);

                var authResult0 = await response0.Content.ReadFromJsonAsync<LoginResultDto>();
                if (authResult0 == null)
                {
                    Logger.Info("Token is null");
                    return;
                }
                string jsonResponse0 = await response0.Content.ReadAsStringAsync();
                Logger.Info("json Response: " + jsonResponse0);
                Logger.Info("AccessToken: " + authResult0.Token);


                //
                // Get Clients
                //

                string url = String.Format("{0}api/clients", BackOfficeUrl);
                Logger.Info(String.Format("calling {0}", url));

                HttpClient httpClient2 = new HttpClient(handler);
                var requestMessage2 = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };
                requestMessage2.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult0.Token);
                requestMessage2.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                var response2 = await httpClient2.SendAsync(requestMessage2);
                var c = response2.Content;
                string jsonResponse2 = await response2.Content.ReadAsStringAsync();
                Logger.Info("json Response: " + jsonResponse2);
                Logger.Info("StatusCode: " + response2.StatusCode);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Azure login failed: " + ex.Message);
            }
        }
    }
}