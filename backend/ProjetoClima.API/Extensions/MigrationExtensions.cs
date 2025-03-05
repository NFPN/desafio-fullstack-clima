using Microsoft.EntityFrameworkCore;
using ProjetoClima.API.Data;

namespace ProjetoClima.API.Extensions
{
    public static class MigrationExtensions
    {
        /// <summary>
        /// Método de extensão para migrar o banco de dados
        /// </summary>
        /// <param name="app"></param>
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ProjetoDbContext>();
            context.Database.Migrate();
        }
    }
}
