using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dtos;
using System.Net.Http.Json;

public class UtilisateursBase : ComponentBase
{
    [Inject] protected HttpClient Http { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

    protected List<UtilisateurDto> utilisateurs = new();
    protected UtilisateurDto editUtilisateur = new();
    protected List<ClientDto> clients = new();
    protected bool showForm = false;
    protected bool isLoading = true;
    protected bool isEdit = false;

    private async Task<string> GetTokenAsync()
    {
        return await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadClients();
        await LoadUtilisateurs();
    }

    protected async Task LoadClients()
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        clients = await Http.GetFromJsonAsync<List<ClientDto>>("api/clients");
    }

    protected async Task LoadUtilisateurs()
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        isLoading = true;
        utilisateurs = await Http.GetFromJsonAsync<List<UtilisateurDto>>("api/users");
        isLoading = false;
    }

    protected void ShowAddUtilisateur()
    {
        editUtilisateur = new UtilisateurDto();
        showForm = true;
        isEdit = false;
    }

    protected void EditUtilisateur(UtilisateurDto user)
    {
        editUtilisateur = new UtilisateurDto
        {
            Id = user.Id,
            Email = user.Email,
            Nom = user.Nom,
            Prenom = user.Prenom,
            MotDePasse = user.MotDePasse,
            Role = user.Role,
            ClientId = user.ClientId
        };
        showForm = true;
        isEdit = true;
    }

    protected async Task SaveUtilisateur()
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        if (isEdit)
        {
            await Http.PutAsJsonAsync($"api/users/{editUtilisateur.Id}", editUtilisateur);
        }
        else
        {
            await Http.PostAsJsonAsync("api/users", editUtilisateur);
        }
        showForm = false;
        await LoadUtilisateurs();
    }

    protected async Task DeleteUtilisateur(int id)
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri("http://localhost:5020/");
        await Http.DeleteAsync($"api/users/{id}");
        await LoadUtilisateurs();
    }

    protected void CancelEdit()
    {
        showForm = false;
    }
}
