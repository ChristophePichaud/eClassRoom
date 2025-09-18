using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Dtos;

namespace Client.Pages
{
    public partial class Clients
    {
        [Inject] private HttpClient Http { get; set; }

        private List<ClientDto> clients;
        private bool showForm = false;
        private ClientDto newClient = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadClientsAsync();
        }

        private async Task LoadClientsAsync()
        {
            clients = await Http.GetFromJsonAsync<List<ClientDto>>("api/clients");
        }

        private void ToggleForm()
        {
            showForm = !showForm;
            if (showForm)
                newClient = new ClientDto();
        }

        private async Task CreateClientAsync()
        {
            var response = await Http.PostAsJsonAsync("api/clients", newClient);
            if (response.IsSuccessStatusCode)
            {
                await LoadClientsAsync();
                showForm = false;
            }
            // Gérer les erreurs si besoin
        }
    }
}