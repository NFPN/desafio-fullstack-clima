using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Models;

namespace ProjetoClima.API.Data
{
    /// <summary>
    /// Contexto EF Core para gerenciar as cidades favoritadas no banco de dados
    /// </summary>
    /// Utilizando primary constructor
    public class CidadesFavoritasContext(DbContextOptions<CidadesFavoritasContext> options) : DbContext(options)
    {
        public DbSet<CidadeFavorita> CidadesFavoritas { get; set; }
    }
}
