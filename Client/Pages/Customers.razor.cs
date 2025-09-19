using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dtos;

namespace Client.Pages
{
    public partial class Customers : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        protected List<ClientDto> Clients { get; set; } = new();
        protected ClientDto EditingClient { get; set; } = new();
        protected bool ShowForm { get; set; } = false;
        protected bool IsEdit { get; set; } = false;

        private async Task<string> GetTokenAsync()
        {
            return await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClients();
        }

        protected async Task LoadClients()
        {
            Http = new HttpClient();
            Http.BaseAddress = new Uri("http://localhost:5020/");
            Clients = await Http.GetFromJsonAsync<List<ClientDto>>("api/clients");
        }

        protected void ShowCreateForm()
        {
            EditingClient = new ClientDto();
            ShowForm = true;
            IsEdit = false;
        }

        protected void EditClient(ClientDto client)
        {
            EditingClient = new ClientDto
            {
                Id = client.Id,
                NomSociete = client.NomSociete,
                Adresse = client.Adresse,
                CodePostal = client.CodePostal,
                Ville = client.Ville,
                Pays = client.Pays,
                EmailAdministrateur = client.EmailAdministrateur,
                Mobile = client.Mobile,
                MotDePasseAdministrateur = "" // Jamais pré-rempli
            };
            ShowForm = true;
            IsEdit = true;
        }

        protected void CancelEdit()
        {
            ShowForm = false;
            IsEdit = false;
        }

        protected async Task SaveClient()
        {
            Http = new HttpClient();
            Http.BaseAddress = new Uri("http://localhost:5020/");
            if (IsEdit)
                await Http.PutAsJsonAsync($"api/clients/{EditingClient.Id}", EditingClient);
            else
                await Http.PostAsJsonAsync("api/clients", EditingClient);

            ShowForm = false;
            await LoadClients();
        }

        protected async Task DeleteClient(int id)
        {
            Http = new HttpClient();
            Http.BaseAddress = new Uri("http://localhost:5020/");
            await Http.DeleteAsync($"api/clients/{id}");
            await LoadClients();
        }
    }
}
