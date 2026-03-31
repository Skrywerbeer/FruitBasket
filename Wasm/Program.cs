using ApplicationCore;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Wasm.Components;
using Infrastructure;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
HttpClient client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddSingleton(client);
// builder.Services.AddSingleton<HttpClient>();
ProductHttpRepository repo = new(client);
builder.Services.AddSingleton<IProductRepository>(repo);
builder.Services.AddSingleton<ProductProvider>();

await builder.Build().RunAsync();
