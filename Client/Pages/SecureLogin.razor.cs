using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class SecureLogin : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; }
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
            catch
            {
                ErrorMessage = "Erreur lors de la connexion au serveur.";
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        private async Task HandleSecureLogin2()
        {
            ErrorMessage = null;
            IsLoading = true;

            try
            {
                Http.BaseAddress = new Uri("http://localhost:5020/");
                var url = $"api/security/login2?username={loginModel.Username}&password={loginModel.Password}";
                var response = await Http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResultDto>();
                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        await JS.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
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
            catch
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

        protected async Task CallProtectedApi()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/protected-endpoint");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await Http.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    // Traitez la réponse ici
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
