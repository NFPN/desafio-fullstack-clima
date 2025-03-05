using ProjetoClima.API.Endpoints;

namespace ProjetoClima.API.Extensions
{
    public static class MapEndpointsExtensions
    {
        /// <summary>
        /// Método de extensão para mapear os endpoints da aplicação
        /// </summary>
        /// <param name="app"></param>
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapAutenticacaoEndpoints();
            app.MapFavoritosEndpoints();
            app.MapClimaEndpoints();
        }
    }
}
