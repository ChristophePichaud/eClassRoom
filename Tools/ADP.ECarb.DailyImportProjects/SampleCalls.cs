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
    public class SampleCalls
    {
        public string BackOfficeUrl = String.Empty;
        public string Email = "christophe.pichaud@aurera.fr";
        public string Password = "admin";
        public string Token = String.Empty;
        public HttpClientHandler Handler;

        public SampleCalls()
        {
            BackOfficeUrl = ConfigurationManager.AppSettings["BackOfficeUrl"];
            Email = ConfigurationManager.AppSettings["Email"];
            Password = ConfigurationManager.AppSettings["Password"];
        }

        public async Task DoWorkAsync()
        {
            await ConnectAsync();
        }

        private async Task ConnectAsync()
        {
            try
            {
                //
                // Login
                //

                // NTLM Secured URL
                string urli = String.Format("{0}api/security/login", BackOfficeUrl);
                var uri = new Uri(urli);
                Logger.Info(String.Format("calling {0}", urli));

                Handler = new HttpClientHandler();
                Handler.Credentials = CredentialCache.DefaultNetworkCredentials; //.DefaultCredentials,

                HttpClient httpClient = new HttpClient(Handler);
                LoginDto loginModel = new LoginDto
                {
                    Username = "admin@samplecorp.com",
                    Password = "admin123"
                };
                
                httpClient.BaseAddress = new Uri("http://localhost:5020/");
                var response = await httpClient.PostAsJsonAsync("api/security/login", loginModel);

                string jsonResponse = await response.Content.ReadAsStringAsync();
                Logger.Info("json Response: " + jsonResponse);
                var authResult = await response.Content.ReadFromJsonAsync<LoginResultDto>();
                if (authResult == null)
                {
                    Logger.Info("Token is null");
                    return;
                }

                Token = authResult.Token;
                Logger.Info("AccessToken: " + Token);

                Task t = SimpleCall("api/clients");
                t.Wait();

                t = SimpleCall("api/machines");
                t.Wait();

                t = SimpleCall("api/salles");
                t.Wait();

                t = SimpleCall("api/users");
                t.Wait();

                /*
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
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine("Azure login failed: " + ex.Message);
            }
        }

        private async Task SimpleCall(string address)
        {
            try
            {
                string url = String.Format("{0}{1}", BackOfficeUrl, address);
                Logger.Info(String.Format("\n\n======================================> calling {0}", url));

                HttpClient httpClient = new HttpClient(Handler);
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };

                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                requestMessage.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(requestMessage);
                var c = response.Content;
                string jsonResponse = await response.Content.ReadAsStringAsync();

                Logger.Info("json Response: " + jsonResponse);
                Logger.Info("StatusCode: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                Logger.Info("SimpleCall failed: " + ex.Message);
            }
        }
    }
}