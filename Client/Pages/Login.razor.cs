using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dtos;

namespace Client.Pages
{
    public partial class Login
    {
        [Inject] protected HttpClient Http { get; set; } = default!;
        [Inject] protected IJSRuntime JS { get; set; } = default!;

        private async Task HandleLogin()
        {
            errorMessage = null;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/auth/login", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    if (tokenResponse is not null && !string.IsNullOrEmpty(tokenResponse.Token))
                    {
                        await JS.InvokeVoidAsync("localStorage.setItem", "jwt", tokenResponse.Token);
                        // Rediriger ou notifier l'utilisateur ici si besoin
                    }
                    else
                    {
                        errorMessage = "Réponse du serveur invalide.";
                    }
                }
                else
                {
                    errorMessage = "Identifiants invalides.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Erreur lors de la connexion : {ex.Message}";
            }
        }

        public class TokenResponse
        {
            public string? Token { get; set; }
        }
    }
}
