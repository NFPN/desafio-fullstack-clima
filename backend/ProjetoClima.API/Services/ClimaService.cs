using Microsoft.Extensions.Caching.Memory;
using ProjetoClima.API.Models;

namespace ProjetoClima.API.Services
{
    /// <summary>
    /// Serviço para obter informações do clima
    /// </summary>
    public class ClimaService : IClimaService
    {
        private IMemoryCache cache;
        private readonly HttpClient client;
        private readonly string apiKey;

        public ClimaService(HttpClient httpClient, IMemoryCache memoryCache, IConfiguration configuration)
        {
            cache = memoryCache;
            client = httpClient;
            apiKey = Environment.GetEnvironmentVariable("OPENWEATHERMAP_API_KEY")
                ?? configuration["OpenWeatherMap:ApiKey"]
                ?? string.Empty;

            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("API key not found");
        }

        /// <summary>
        /// Obtém o clima de uma cidade
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        public async Task<DadosClima> ObterClimaAsync(string cidade)
        {
            string cacheKey = $"clima_{cidade}";
            if (cache.TryGetValue(cacheKey, out DadosClima? dadosCache) && dadosCache != null)
                return dadosCache;

            var url = $"weather?q={cidade}&appid={apiKey}&units=metric";
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DadosClima>() ?? new();
        }

        /// <summary>
        /// Obtém a previsão do tempo de uma cidade, para os próximos 5 dias
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        public async Task<DadosPrevisao> ObterPrevisaoAsync(string cidade)
        {
            string cacheKey = $"previsao_{cidade}";
            if (cache.TryGetValue(cacheKey, out DadosPrevisao? dadosCache) && dadosCache != null)
                return dadosCache;

            var url = $"forecast?q={cidade}&appid={apiKey}&units=metric";
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DadosPrevisao>() ?? new();
        }
    }
}
