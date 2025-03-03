using ProjetoClima.API.Endpoints;
using ProjetoClima.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o serviço de clima
builder.Services.AddHttpClient<ClimaService>();

var app = builder.Build();

//app.UseAuthorization();
app.UseHttpsRedirection();

app.MapClimaEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();
