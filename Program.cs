using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimulacaoEmprestimo.Services;
using SimulacaoEmprestimo.Repositories;
using Microsoft.OpenApi.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ISimulacaoService, SimulacaoService>();
builder.Services.AddScoped<ISimulacaoRepository, SimulacaoInMemoryRepository>();

// Adiciona controladores para endpoints da API
builder.Services.AddControllers().AddNewtonsoftJson();

// Adiciona serviços do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimulacaoEmprestimo API", Version = "v1" });
});

// Configura TempData para usar cookies
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Garante que o middleware de sessão seja adicionado ao pipeline

app.UseAuthorization();

// Habilita middleware para servir o Swagger gerado como um endpoint JSON
app.UseSwagger(c =>
{
    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
});

// Habilita middleware para servir swagger-ui (HTML, JS, CSS, etc.), especificando o endpoint JSON do Swagger
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "SimulacaoEmprestimo API V1");
    c.RoutePrefix = "api"; // Define o prefixo da rota para acessar o Swagger em /api
});

// Mapea Razor Pages
app.MapRazorPages();

// Mapea controladores da API
app.MapControllers();

app.Run();
