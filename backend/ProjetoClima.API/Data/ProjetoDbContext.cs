using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Models;

namespace ProjetoClima.API.Data
{
    /// <summary>
    /// Contexto EF Core para gerenciar as cidades favoritadas no banco de dados
    /// </summary>
    /// Utilizando primary constructor
    public class ProjetoDbContext(DbContextOptions<ProjetoDbContext> options) : IdentityDbContext<Usuario>(options)
    {
        /// <summary>
        /// DbSet para gerenciar as cidades favoritadas no banco de dados
        /// </summary>
        public DbSet<CidadeFavorita> CidadesFavoritas { get; set; }

        /// <summary>
        /// Configuração do modelo de dados para o EF Core (relacionamento entre CidadeFavorita e Usuario)
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CidadeFavorita>(entidade =>
            {
                entidade.HasKey(cf => cf.Id);

                entidade.Property(cf => cf.NomeCidade).IsRequired();
                entidade.Property(cf => cf.IdUsuario).IsRequired();

                entidade.HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey(cidadeFavorita => cidadeFavorita.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade);

                entidade.HasIndex(cf => cf.IdUsuario);
            });
        }
    }
}
