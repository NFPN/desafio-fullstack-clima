using Microsoft.EntityFrameworkCore;
using Moq;
using ProjetoClima.API.Data;
using ProjetoClima.API.Models;
using ProjetoClima.API.Services;
using System.Linq.Expressions;

namespace ProjetoClima.API.Testes
{
    public class FavoritoServiceTestes
    {
        private readonly Mock<ProjetoDbContext> contextMock;
        private readonly Mock<DbSet<CidadeFavorita>> mockCidades;

        public FavoritoServiceTestes()
        {
            // Mock DbSet<CidadeFavorita>
            mockCidades = new Mock<DbSet<CidadeFavorita>>();

            // Mock ApplicationDbContext
            contextMock = new Mock<ProjetoDbContext>(new DbContextOptions<ProjetoDbContext>());
            contextMock.Setup(m => m.CidadesFavoritas).Returns(mockCidades.Object);
        }

        [Fact]
        public async Task GetFavoritesAsync_ReturnsFavorites_ForIdUsuario()
        {
            // Arrange
            var usuario = "user1";
            var favorites = new List<CidadeFavorita>
            {
                new CidadeFavorita { Id = 1, NomeCidade = "London", IdUsuario = usuario },
                new CidadeFavorita { Id = 2, NomeCidade= "Paris", IdUsuario = usuario }
            }.AsQueryable();

            mockCidades.As<IQueryable<CidadeFavorita>>().Setup(m => m.Provider).Returns(favorites.Provider);
            mockCidades.As<IQueryable<CidadeFavorita>>().Setup(m => m.Expression).Returns(favorites.Expression);
            mockCidades.As<IQueryable<CidadeFavorita>>().Setup(m => m.ElementType).Returns(favorites.ElementType);
            mockCidades.As<IQueryable<CidadeFavorita>>().Setup(m => m.GetEnumerator()).Returns(favorites.GetEnumerator());

            var service = new FavoritoService(contextMock.Object);

            // Act
            var result = await service.ObterFavoritosAsync(usuario);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, f => f.NomeCidade == "London");
            Assert.Contains(result, f => f.NomeCidade == "Paris");
        }

        [Fact]
        public async Task AddFavoriteAsync_AddsAndReturnsFavorite()
        {
            // Arrange
            var usuario = "user1";
            var cityName = "Berlin";
            mockCidades.Setup(m => m.Add(It.IsAny<CidadeFavorita>())).Callback<CidadeFavorita>(f => f.Id = 1);
            contextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var FavoritoService = new FavoritoService(contextMock.Object);

            // Act
            var result = await FavoritoService.AdicionarFavoritoAsync(new CidadeFavorita { NomeCidade = cityName, IdUsuario = usuario });

            // Assert
            Assert.Equal(1, result);
            contextMock.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task RemoveFavoriteAsync_ReturnsValor_WhenCidadeFavoritaExiste()
        {
            // Arrange
            var usuario = "user1";
            var cityName = "Tokyo";
            var favorite = new CidadeFavorita { Id = 1, NomeCidade = cityName, IdUsuario = usuario };
            mockCidades.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<CidadeFavorita, bool>>>(), default))
                .ReturnsAsync(favorite);
            contextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var FavoritoService = new FavoritoService(contextMock.Object);

            // Act
            var result = await FavoritoService.RemoverFavoritoAsync(new CidadeFavorita { NomeCidade = cityName, IdUsuario = usuario });

            // Assert
            Assert.NotEqual(0, result);
            mockCidades.Verify(m => m.Remove(favorite), Times.Once());
            contextMock.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task RemoveFavoriteAsync_ReturnsFalse_WhenFavoriteDoesNotExist()
        {
            // Arrange
            var usuario = "user1";
            var cityName = "Moscow";
            mockCidades.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<CidadeFavorita, bool>>>(), default))
                .ReturnsAsync(null as CidadeFavorita);

            var FavoritoService = new FavoritoService(contextMock.Object);

            // Act
            var result = await FavoritoService.RemoverFavoritoAsync(new CidadeFavorita { NomeCidade = cityName, IdUsuario = usuario });

            // Assert
            Assert.Equal(0, result);
            contextMock.Verify(m => m.SaveChangesAsync(default), Times.Never());
        }
    }
}
