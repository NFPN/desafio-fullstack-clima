using ProjetoClima.API.Models;

namespace ProjetoClima.API.Services
{
    public interface IFavoritoService
    {
        Task<int> AdicionarFavoritoAsync(CidadeFavorita cidadeFavorita);

        Task<int> RemoverFavoritoAsync(CidadeFavorita cidadeFavorita);

        Task<IEnumerable<CidadeFavorita>> ObterFavoritosAsync(string idUsuario);
    }
}
