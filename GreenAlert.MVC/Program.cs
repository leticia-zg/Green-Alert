using GreenAlert.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("GreenAlertAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5246");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Estacoes}/{action=Index}/{id?}");

await app.RunAsync();
