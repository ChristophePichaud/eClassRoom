using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Client.Pages
{
    public partial class SecureLogin : ComponentBase
    {
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        protected LoginDto loginModel = new();
        protected string ErrorMessage { get; set; }
        protected bool IsLoading { get; set; }

        protected async Task HandleSecureLogin()
        {
            ErrorMessage = null;
            IsLoading = true;

            try
            {
                HttpClient Http = new HttpClient();
                Http.BaseAddress = new Uri("http://localhost:5020/");
                var response = await Http.PostAsJsonAsync("api/security/login", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResultDto>();
                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        await JS.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
                        Console.WriteLine("Token stocké : " + result.Token);
                        Navigation.NavigateTo("/");
                    }
                    else
                    {
                        ErrorMessage = "Erreur d'authentification.";
                    }
                }
                else
                {
                    ErrorMessage = "Identifiants invalides.";
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = "Erreur lors de la connexion au serveur.";
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        private async Task<string> GetTokenAsync()
        {
            return await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        protected async Task CallProtectedApiWithPost()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/clients/1");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:5020/");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    // Traitez la réponse ici
                    Console.WriteLine("Réponse reçue avec succès.");
                }
                else
                {
                    ErrorMessage = "Accès refusé ou token invalide.";
                }
            }
            else
            {
                ErrorMessage = "Aucun token trouvé, veuillez vous connecter.";
            }
        }

    }
}
