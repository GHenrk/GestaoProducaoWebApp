using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GestaoProducao_MVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestaoProducao_MVCContext") ?? throw new InvalidOperationException("Connection string 'GestaoProducao_MVCContext' not found.")));

builder.Services.AddScoped<OrdemProdutoService>();
builder.Services.AddScoped<ProcessoService>();
builder.Services.AddScoped<MaquinaService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<ApontamentoService>();


// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
LocalizationService();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


void LocalizationService()
{
    var ptBr = new CultureInfo("pt-BR");

    var localizationOptions = new RequestLocalizationOptions
    {
        DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ptBr),
        SupportedCultures = new List<CultureInfo> { ptBr },
        SupportedUICultures = new List<CultureInfo> { ptBr },
    };
    app.UseRequestLocalization(localizationOptions);


}