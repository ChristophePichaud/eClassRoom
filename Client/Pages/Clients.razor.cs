using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Pages
{
    public class ClientsBase : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        protected List<ClientDto> clients;
        protected bool isLoading = true;
        protected bool showForm = false;
        protected bool isEdit = false;
        protected ClientDto editClient = new();

        private async Task<string> GetTokenAsync()
        {
            return await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClientsWithHandlerAsync();
        }

        protected async Task LoadClientsAsync()
        {
            isLoading = true;
            try
            {
                Console.WriteLine("Essai de récupération du Token stocké...");
                var token = await GetTokenAsync();
                Console.WriteLine("Token stocké : " + token);

                // Utilisez l'instance Http injectée (déjà configurée avec le handler d'authentification si configuré dans Program.cs)
                // Ne pas recréer un nouveau HttpClient ici

                if (!string.IsNullOrEmpty(token))
                {
                    Http = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "api/clients"); // <-- "clients" au pluriel
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    Http.BaseAddress = new Uri("http://localhost:5020/");
                    var response = await Http.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        clients = await response.Content.ReadFromJsonAsync<List<ClientDto>>();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine("Erreur 401 : Token JWT manquant ou invalide.");
                        // Afficher un message à l'utilisateur ou rediriger vers la page de login
                    }
                }
                else
                {
                    Console.WriteLine("Aucun token trouvé, accès refusé.");
                    // Afficher un message à l'utilisateur ou rediriger vers la page de login
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la récupération des clients : " + ex.Message);
            }
            isLoading = false;
        }

        protected async Task LoadClientsWithHandlerAsync()
        {
            isLoading = true;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/clients");
                // Le handler personnalisé ajoutera automatiquement le header Authorization

                var response = await Http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    clients = await response.Content.ReadFromJsonAsync<List<ClientDto>>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Erreur 401 : Token JWT manquant ou invalide.");
                    // Afficher un message à l'utilisateur ou rediriger vers la page de login
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la récupération des clients : " + ex.Message);
            }
            isLoading = false;
        }

        protected void ShowAddClient()
        {
            editClient = new ClientDto();
            showForm = true;
            isEdit = false;
        }

        protected void EditClient(ClientDto client)
        {
            // Clone pour éviter la modification directe dans la liste
            editClient = new ClientDto
            {
                Id = client.Id,
                NomSociete = client.NomSociete,
                Adresse = client.Adresse,
                CodePostal = client.CodePostal,
                Ville = client.Ville,
                Pays = client.Pays,
                EmailAdministrateur = client.EmailAdministrateur,
                Mobile = client.Mobile,
                MotDePasseAdministrateur = "" // Ne jamais pré-remplir le mot de passe
            };
            showForm = true;
            isEdit = true;
        }

        protected void CancelEdit()
        {
            showForm = false;
            isEdit = false;
        }

        protected async Task SaveClient()
        {
            if (isEdit)
            {
                var response = await Http.PutAsJsonAsync($"api/clients/{editClient.Id}", editClient);
                if (response.IsSuccessStatusCode)
                {
                    await LoadClientsAsync();
                    showForm = false;
                }
                // Gérer les erreurs si besoin
            }
            else
            {
                var response = await Http.PostAsJsonAsync("api/clients", editClient);
                if (response.IsSuccessStatusCode)
                {
                    await LoadClientsAsync();
                    showForm = false;
                }
                // Gérer les erreurs si besoin
            }
        }

        protected async Task DeleteClient(int id)
        {
            var response = await Http.DeleteAsync($"api/clients/{id}");
            if (response.IsSuccessStatusCode)
            {
                await LoadClientsAsync();
            }
            // Gérer les erreurs si besoin
        }
    }
}