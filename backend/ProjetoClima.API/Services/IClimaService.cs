using ProjetoClima.API.Models;

namespace ProjetoClima.API.Services
{
    public interface IClimaService
    {
        Task<DadosClima> ObterClimaAsync(string cidade);

        Task<DadosPrevisao> ObterPrevisaoAsync(string cidade);
    }
}
