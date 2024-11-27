using Blazor_Server_App_Login.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Blazor_Server_App_Login.Extensions;
using Blazor_Server_App_Login.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddHttpClient();
builder.Services.AddBlazoredSessionStorage();
// Add MVC Controllers
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationExt>();
builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddSingleton<ISDigito, SDigitoData>();
builder.Services.AddScoped<ISDigito, SDigitoData>(); // Se usa en lugar de singleton porque da error el DbContext
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");

});


app.Run();
