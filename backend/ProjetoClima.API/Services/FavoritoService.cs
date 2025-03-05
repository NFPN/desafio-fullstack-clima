using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Data;
using ProjetoClima.API.Models;

namespace ProjetoClima.API.Services
{
    /// <summary>
    /// Serviço para gerenciar as cidades favoritadas pelos usuários
    /// </summary>
    /// <remarks>
    /// Construtor com injeção de dependência do contexto do banco de dados
    /// </remarks>
    /// <param name="context"></param>
    public class FavoritoService(ProjetoDbContext context)
    {
        /// <summary>
        /// Adiciona uma cidade favorita para um usuário
        /// </summary>
        /// <param name="cidadeFavorita"></param>
        public async Task AdicionarFavorito(CidadeFavorita cidadeFavorita)
        {
            context.CidadesFavoritas.Add(cidadeFavorita);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma cidade favorita de um usuário
        /// </summary>
        /// <param name="cidadeFavorita"></param>
        public async Task RemoverFavorito(CidadeFavorita cidadeFavorita)
        {
            context.CidadesFavoritas.Remove(cidadeFavorita);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém as cidades favoritas de um usuário
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CidadeFavorita>> ObterFavoritos(string idUsuario)
        {
            return await context.CidadesFavoritas
                .Where(c => c.IdUsuario == idUsuario)
                .ToListAsync();
        }
    }
}
