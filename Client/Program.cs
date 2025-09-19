
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using System.Net.Http;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with JWT Bearer handler
builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    var handler = sp.GetRequiredService<CustomAuthorizationMessageHandler>();
    var httpClient = new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5020/") // Adresse de l'API
    };
    return httpClient;
});

await builder.Build().RunAsync();

// Handler personnalisé pour ajouter le token JWT
public class CustomAuthorizationMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;

    public CustomAuthorizationMessageHandler(IJSRuntime js)
    {
        _js = js;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
