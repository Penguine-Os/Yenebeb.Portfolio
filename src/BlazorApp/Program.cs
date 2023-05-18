using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using BlazorApp.Services; 
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    // opt.AddPolicy("Policy1",
    //     policy =>
    //     {
    //         policy.AllowAnyOrigin()
    //             .AllowAnyHeader()
    //             .AllowAnyMethod();
    //     });
}); 

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<HeroImageService>();
builder.Services.AddScoped<IGithubService,GithubService>();
 
builder.Services.AddMudServices();
await builder.Build().RunAsync();
