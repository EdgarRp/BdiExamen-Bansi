using WebAppBdiExamen.Components;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using apiexamen;
using apiexamen.Data.Repositories.Interfaces;
using apiexamen.Data.Repositories;
using apiexamen.Data.Services.Interfaces;
using apiexamen.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<IConfiguration>( builder.Configuration );
builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( builder.Configuration["AppSettings:WsApiService"]! ) } );
builder.Services.AddScoped<IStoredProcedureRepository, StoredProcedureRepository>();
builder.Services.AddScoped<IWsApiExamenService, WsApiExamenService>();



#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


await app.RunAsync();
