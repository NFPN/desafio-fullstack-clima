using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Data;
using ProjetoClima.API.Models;
using System.Security.Claims;

namespace ProjetoClima.API.Endpoints
{
    public static class FavoritosEndpoints
    {
        public static void MapFavoritosEndpoints(this IEndpointRouteBuilder app)
        {
            var nomeFavoritosEndpoint = "ObterFavoritos";
            var nomeFavoritoEndpoint = "ObterFavorito";
            var nomeAdicionarFavoritoEndpoint = "AdicionarFavorito";
            var nomeRemoverFavoritoEndpoint = "RemoverFavorito";

            // Adiciona um endpoint para obter todos os favoritos de um usuário
            app.MapGet("/favoritos", async (HttpContext context, [FromServices] ProjetoDbContext projetoContext) =>
            {
                try
                {
                    var idUsuario = ObterUsuarioDoToken(context);

                    var favoritos = await projetoContext.CidadesFavoritas.Where(f => f.IdUsuario == idUsuario).ToListAsync();
                    return Results.Ok(favoritos);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeFavoritosEndpoint)
            .RequireAuthorization();

            // Adiciona um endpoint para obter um favorito de um usuário
            app.MapGet("/favorito/{nome}", async (HttpContext context, [FromServices] ProjetoDbContext projetoContext, string nome) =>
            {
                try
                {
                    var idUsuario = ObterUsuarioDoToken(context);

                    if (string.IsNullOrEmpty(idUsuario))
                        return Results.Unauthorized();

                    var favorito = await projetoContext.CidadesFavoritas.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario && f.NomeCidade == nome);
                    return Results.Ok(favorito);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeFavoritoEndpoint)
            .RequireAuthorization();

            // Adiciona um endpoint para adicionar um favorito a um usuário
            app.MapPost("/favorito", async (HttpContext context, [FromServices] ProjetoDbContext projetoContext, CidadeFavorita favorito) =>
            {
                try
                {
                    favorito.IdUsuario = ObterUsuarioDoToken(context);
                    await projetoContext.CidadesFavoritas.AddAsync(favorito);
                    await projetoContext.SaveChangesAsync();
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeAdicionarFavoritoEndpoint)
            .RequireAuthorization();

            // Adiciona um endpoint para remover um favorito de um usuário
            app.MapDelete("/favorito/{nome}", async (HttpContext context, [FromServices] ProjetoDbContext projetoContext, string nome) =>
            {
                try
                {
                    var idUsuario = ObterUsuarioDoToken(context);

                    var favorito = await projetoContext.CidadesFavoritas.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario && f.NomeCidade == nome);
                    if (favorito == null)
                    {
                        return Results.NotFound();
                    }
                    projetoContext.CidadesFavoritas.Remove(favorito);
                    await projetoContext.SaveChangesAsync();
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(nomeRemoverFavoritoEndpoint)
            .RequireAuthorization();
        }

        private static string ObterUsuarioDoToken(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
