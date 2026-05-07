using Blazored.LocalStorage;
using ECommerce.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<ECommerce.Web.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// URL base de tu API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5255/")
});

// Servicios
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddSingleton<ToastService>();
builder.Services.AddScoped<DashboardService>();

await builder.Build().RunAsync();