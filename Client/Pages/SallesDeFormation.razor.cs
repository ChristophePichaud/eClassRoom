using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using System.Net.Http.Json;

public class SallesDeFormationBase : ComponentBase
{
    [Inject] protected HttpClient Http { get; set; }

    protected List<SalleDeFormationDto> salles = new();
    protected SalleDeFormationDto editSalle = new();
    protected bool showForm = false;
    protected bool isLoading = true;
    protected bool isEdit = false;

    // List of formateurs for the combobox
    protected List<UtilisateurDto> formateurs = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadFormateurs();
        await LoadSalles();
    }

    protected async Task LoadFormateurs()
    {
        var allUsers = await Http.GetFromJsonAsync<List<UtilisateurDto>>("/users");
        formateurs = allUsers?.Where(u => u.Role == "Formateur").ToList() ?? new List<UtilisateurDto>();
    }

    protected async Task LoadSalles()
    {
        isLoading = true;
        salles = await Http.GetFromJsonAsync<List<SalleDeFormationDto>>("/salles");
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
            DateFin = salle.DateFin
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
        if (isEdit)
        {
            await Http.PutAsJsonAsync($"/salles/{editSalle.Id}", editSalle);
        }
        else
        {
            await Http.PostAsJsonAsync("/salles", editSalle);
        }
        showForm = false;
        await LoadSalles();
    }

    protected async Task DeleteSalle(int id)
    {
        await Http.DeleteAsync($"/salles/{id}");
        await LoadSalles();
    }

    protected void CancelEdit()
    {
        showForm = false;
    }
}
