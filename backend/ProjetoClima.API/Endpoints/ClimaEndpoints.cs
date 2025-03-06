using ProjetoClima.API.Services;

namespace ProjetoClima.API.Endpoints
{
    public static class ClimaEndpoints
    {
        public static void MapClimaEndpoints(this IEndpointRouteBuilder app)
        {
            var nomeClimaEndpoint = "ObterClima";
            var nomePrevisaoEndpoint = "ObterPrevisao";

            // Adiciona um endpoint para obter o clima de uma cidade
            app.MapGet("/clima/{cidade}", async (IClimaService climaService, string cidade) =>
            {
                try
                {
                    return Results.Ok(await climaService.ObterClimaAsync(cidade));
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeClimaEndpoint);

            // Adiciona um endpoint para obter a previsão do tempo de uma cidade
            app.MapGet("/previsao/{cidade}", async (IClimaService climaService, string cidade) =>
            {
                try
                {
                    return Results.Ok(await climaService.ObterPrevisaoAsync(cidade));
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomePrevisaoEndpoint);
        }
    }
}
