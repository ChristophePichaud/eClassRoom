using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Net.Http.Json;

public class SimpleQueriesBase : ComponentBase
{
    [Inject] protected HttpClient Http { get; set; }
    [Inject] public IJSRuntime JS { get; set; }

    protected List<string> results;
    protected bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;
        var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (!string.IsNullOrEmpty(token))
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/simple/queries");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Http = new HttpClient();
            Http.BaseAddress = new Uri("http://localhost:5020/");
            var response = await Http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadFromJsonAsync<List<string>>();
            }
        }
        isLoading = false;
    }
}