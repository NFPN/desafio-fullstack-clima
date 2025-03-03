using ProjetoClima.API.Services;

namespace ProjetoClima.API.Endpoints
{
    public static class ClimaEndpoints
    {
        public static void MapClimaEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            var nomeClimaEndpoint = "ObterClima";
            var nomePrevisaoEndpoint = "ObterPrevisao";

            // Adiciona um endpoint para obter o clima de uma cidade
            routeBuilder.MapGet("/clima/{cidade}", async (ClimaService climaService, string cidade) =>
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
            routeBuilder.MapGet("/previsao/{cidade}", async (ClimaService climaService, string cidade) =>
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
