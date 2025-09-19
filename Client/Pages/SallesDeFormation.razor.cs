using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dtos;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SallesDeFormationBase : ComponentBase
{
    [Inject] public HttpClient Http { get; set; }
    [Inject] public IJSRuntime JS { get; set; }

    protected List<SalleDeFormationDto> salles = new();
    protected SalleDeFormationDto editSalle = new();
    protected bool showForm = false;
    protected bool isLoading = true;
    protected bool isEdit = false;

    // List of formateurs for the combobox
    protected List<UtilisateurDto> formateurs = new();

    // List of clients for the combobox
    protected List<ClientDto> clients = new();

    private async Task<string> GetTokenAsync()
    {
        return await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadFormateurs();
        await LoadClients();
        await LoadSalles();
    }

    protected async Task LoadClients()
    {
        var token = await GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/clients");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            Http = new HttpClient();
            Http.BaseAddress = new Uri("http://localhost:5020/");
            var response = await Http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                clients = await response.Content.ReadFromJsonAsync<List<ClientDto>>();
                Console.WriteLine($"Clients chargés : {clients.Count}");
            }
            else
            {
                clients = new List<ClientDto>();
                // Optionally handle unauthorized or error cases here
                Console.WriteLine($"Erreur lors du chargement des clients : {response.StatusCode}");
            }
        }
        else
        {
            clients = new List<ClientDto>();
            // Optionally handle missing token here
            Console.WriteLine("Token JWT manquant ou invalide.");
        }
    }

    protected async Task LoadFormateurs()
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        var allUsers = await Http.GetFromJsonAsync<List<UtilisateurDto>>("api/users");
        formateurs = allUsers?.Where(u => u.Role == "Formateur").ToList() ?? new List<UtilisateurDto>();
    }

    protected async Task LoadSalles()
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        isLoading = true;
        salles = await Http.GetFromJsonAsync<List<SalleDeFormationDto>>("api/salles");
        isLoading = false;
    }

    protected void ShowAddSalle()
    {
        editSalle = new SalleDeFormationDto();
        showForm = true;
        isEdit = false;
    }

    protected void EditSalle(SalleDeFormationDto salle)
    {
        editSalle = new SalleDeFormationDto
        {
            Id = salle.Id,
            Nom = salle.Nom,
            Formateur = salle.Formateur,
            DateDebut = salle.DateDebut,
            DateFin = salle.DateFin,
            ClientId = salle.ClientId
        };
        showForm = true;
        isEdit = true;
    }

    protected async Task SaveSalle()
    {
        // Assign the selected formateur object from the list
        if (editSalle.Formateur != null && editSalle.Formateur.Id != 0)
        {
            var selected = formateurs.FirstOrDefault(f => f.Id == editSalle.Formateur.Id);
            if (selected != null)
            {
                editSalle.Formateur = new UtilisateurDto
                {
                    Id = selected.Id,
                    Email = selected.Email,
                    Nom = selected.Nom,
                    Prenom = selected.Prenom,
                    MotDePasse = selected.MotDePasse,
                    Role = selected.Role,
                    ClientId = selected.ClientId
                };
            }
        }

        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        if (isEdit)
        {
            await Http.PutAsJsonAsync($"api/salles/{editSalle.Id}", editSalle);
        }
        else
        {
            await Http.PostAsJsonAsync("api/salles", editSalle);
        }
        showForm = false;
        await LoadSalles();
    }

    protected async Task DeleteSalle(int id)
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        await Http.DeleteAsync($"api/salles/{id}");
        await LoadSalles();
    }

    protected void CancelEdit()
    {
        showForm = false;
    }
}
