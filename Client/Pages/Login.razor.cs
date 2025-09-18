using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IJSRuntime JS { get; set; } // Ajout de l'injection de JS

        protected LoginDto loginModel = new();
        protected string ErrorMessage { get; set; }
        protected bool IsLoading { get; set; }

        protected async Task HandleLogin()
        {
            ErrorMessage = null;
            IsLoading = true;

            try
            {
                // Appel POST avec loginModel dans le corps de la requête
                var response = await Http.PostAsJsonAsync("api/auth/login", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResultDto>();
                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        // Stockage du token dans le localStorage
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
            catch(Exception ex)
            {
                ErrorMessage = "Erreur lors de la connexion au serveur.";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
