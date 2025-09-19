using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dtos;
using System.Net.Http.Json;

public class MachinesVirtuellesBase : ComponentBase
{
    [Inject] protected HttpClient Http { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

    protected List<MachineVirtuelleDto> machines = new();
    protected MachineVirtuelleDto editVm = new();
    protected bool showForm = false;
    protected bool isLoading = true;
    protected bool isEdit = false;

        private async Task<string> GetTokenAsync()
        {
            return await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

    protected override async Task OnInitializedAsync()
    {
        await LoadMachines();
    }

    protected async Task LoadMachines()
    {
        isLoading = true;
        machines = await Http.GetFromJsonAsync<List<MachineVirtuelleDto>>("api/machines");
        isLoading = false;
    }

    protected void ShowAddVm()
    {
    editVm = new MachineVirtuelleDto();
        showForm = true;
        isEdit = false;
    }

    protected void EditVm(MachineVirtuelleDto vm)
    {
        editVm = new MachineVirtuelleDto
        {
            Id = vm.Id,
            Name = vm.Name,
            TypeOS = vm.TypeOS,
            TypeVM = vm.TypeVM,
            Sku = vm.Sku,
            Offer = vm.Offer,
            Version = vm.Version,
            DiskISO = vm.DiskISO,
            NomMarketing = vm.NomMarketing
        };
        showForm = true;
        isEdit = true;
    }

    protected async Task SaveVm()
    {
        if (isEdit)
        {
            await Http.PutAsJsonAsync($"api/machines/{editVm.Id}", editVm);
        }
        else
        {
            await Http.PostAsJsonAsync("api/machines", editVm);
        }
        showForm = false;
        await LoadMachines();
    }

    protected async Task DeleteVm(int id)
    {
        await Http.DeleteAsync($"api/machines/{id}");
        await LoadMachines();
    }

    protected void CancelEdit()
    {
        showForm = false;
    }
}
