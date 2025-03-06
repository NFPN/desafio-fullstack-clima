using ProjetoClima.API.Models;

namespace ProjetoClima.API.Services
{
    public interface IClimaService
    {
        Task<DadosClima> ObterClimaAsync(string cidade, string idioma = "pt_br");

        Task<DadosPrevisao> ObterPrevisaoAsync(string cidade, string idioma = "pt_br");
    }
}
