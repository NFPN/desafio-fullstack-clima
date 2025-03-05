using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Data;
using ProjetoClima.API.Extensions;
using ProjetoClima.API.Models;
using ProjetoClima.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddHttpClient<IClimaService, ClimaService>(client => client.BaseAddress = new Uri(ObterApiClima()));

// Adiciona autenticação jwt
builder.ConfigurarJwtAuthentication();

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<ProjetoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o Identity
builder.Services.AddIdentityCore<Usuario>()
    .AddEntityFrameworkStores<ProjetoDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<Usuario>>();

// Adiciona os serviços
builder.Services.AddHttpClient<ClimaService>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod()
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseCors(c => c.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
);

app.MapEndpoints();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

await app.RunAsync();

static string ObterApiClima() => "https://api.openweathermap.org/data/2.5/";
