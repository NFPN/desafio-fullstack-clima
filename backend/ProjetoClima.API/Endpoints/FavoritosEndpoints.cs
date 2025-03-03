using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Data;
using ProjetoClima.API.Models;

namespace ProjetoClima.API.Endpoints
{
    public static class FavoritosEndpoints
    {
        public static void MapFavoritosEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            var nomeFavoritosEndpoint = "ObterFavoritos";
            var nomeFavoritoEndpoint = "ObterFavorito";
            var nomeAdicionarFavoritoEndpoint = "AdicionarFavorito";
            var nomeRemoverFavoritoEndpoint = "RemoverFavorito";

            // Adiciona um endpoint para obter todos os favoritos de um usuário
            routeBuilder.MapGet("/favoritos/{idUsuario}", async (CidadesFavoritasContext context, string idUsuario) =>
            {
                try
                {
                    var favoritos = await context.CidadesFavoritas.Where(f => f.IdUsuario == idUsuario).ToListAsync();
                    return Results.Ok(favoritos);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeFavoritosEndpoint);

            // Adiciona um endpoint para obter um favorito de um usuário
            routeBuilder.MapGet("/favorito/{idUsuario}/{nome}", async (CidadesFavoritasContext context, string idUsuario, string nome) =>
            {
                try
                {
                    var favorito = await context.CidadesFavoritas.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario && f.Nome == nome);
                    return Results.Ok(favorito);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeFavoritoEndpoint);

            // Adiciona um endpoint para adicionar um favorito a um usuário
            routeBuilder.MapPost("/favorito", async (CidadesFavoritasContext context, CidadeFavorita favorito) =>
            {
                try
                {
                    await context.CidadesFavoritas.AddAsync(favorito);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeAdicionarFavoritoEndpoint);

            // Adiciona um endpoint para remover um favorito de um usuário
            routeBuilder.MapDelete("/favorito/{idUsuario}/{nome}", async (CidadesFavoritasContext context, string idUsuario, string nome) =>
            {
                try
                {
                    var favorito = await context.CidadesFavoritas.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario && f.Nome == nome);
                    if (favorito == null)
                    {
                        return Results.NotFound();
                    }
                    context.CidadesFavoritas.Remove(favorito);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeRemoverFavoritoEndpoint);
        }
    }
}
