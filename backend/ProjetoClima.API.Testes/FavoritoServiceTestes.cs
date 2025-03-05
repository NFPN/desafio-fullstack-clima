using Microsoft.EntityFrameworkCore;
using Moq;
using ProjetoClima.API.Data;
using ProjetoClima.API.Models;
using ProjetoClima.API.Services;

namespace ProjetoClima.API.Testes
{
    public class FavoritoServiceTestes
    {
        private readonly Mock<ProjetoDbContext> contextMock;
        private readonly Mock<DbSet<CidadeFavorita>> mockCidades;

        public FavoritoServiceTestes()
        {
            // Mock DbSet<FavoriteCity>
            mockCidades = new Mock<DbSet<CidadeFavorita>>();

            // Mock ApplicationDbContext
            contextMock = new Mock<ProjetoDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMock.Setup(m => m.CidadesFavoritas).Returns(mockCidades.Object);
        }

        [Fact]
        public async Task ObterFavoritosAsync_RetornaFavoritos()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProjetoDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            using var context = new ProjetoDbContext(options);
            context.CidadesFavoritas.Add(new CidadeFavorita { IdUsuario = "1", NomeCidade = "São Paulo" });

            await context.SaveChangesAsync();

            var favoritoService = new FavoritoService(context);

            // Act
            var result = (await favoritoService.ObterFavoritos("1")).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("São Paulo", result[0].NomeCidade);
        }
    }
}
