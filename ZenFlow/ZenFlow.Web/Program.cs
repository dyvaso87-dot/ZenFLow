using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ZenFlow.Web;
using ZenFlow.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    }
);

builder.Services.AddScoped<LocalStorageService>();

await builder.Build().RunAsync();