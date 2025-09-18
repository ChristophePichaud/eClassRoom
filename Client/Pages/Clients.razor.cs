using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Dtos;

namespace Client.Pages
{
    public class ClientsBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }

        protected List<ClientDto> clients;
        protected bool isLoading = true;
        protected bool showForm = false;
        protected bool isEdit = false;
        protected ClientDto editClient = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadClientsAsync();
        }

        protected async Task LoadClientsAsync()
        {
            isLoading = true;
            clients = await Http.GetFromJsonAsync<List<ClientDto>>("api/clients");
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