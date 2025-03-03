using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Data;
using ProjetoClima.API.Endpoints;
using ProjetoClima.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<CidadesFavoritasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona os serviços de clima
builder.Services.AddHttpClient<ClimaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

//app.UseAuthorization();
app.UseHttpsRedirection();

// Adiciona os endpoints
app.MapClimaEndpoints();
app.MapFavoritosEndpoints();

app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();
