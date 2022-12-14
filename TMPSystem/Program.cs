using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using System.Globalization;
using GestaoProducao_MVC.Helper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GestaoProducao_MVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestaoProducao_MVCContext") ?? throw new InvalidOperationException("Connection string 'GestaoProducao_MVCContext' not found.")));
builder.Services.AddScoped<OrdemProdutoService>();
builder.Services.AddScoped<ProcessoService>();
builder.Services.AddScoped<MaquinaService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<ApontamentoService>();
builder.Services.AddScoped<CodigoParadaService>();
builder.Services.AddScoped<RegistroParadaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessaoService>();
builder.Services.AddSession(
    o => {
        o.Cookie.HttpOnly = true;
        o.Cookie.IsEssential = true;
});
builder.Services.AddScoped<EmailService>();


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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

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