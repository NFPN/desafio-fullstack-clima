using Microsoft.Extensions.Caching.Memory;
using ProjetoClima.API.Extensions;
using ProjetoClima.API.Models;

namespace ProjetoClima.API.Services
{
    /// <summary>
    /// Serviço para obter informações do clima
    /// </summary>
    public class ClimaService : IClimaService
    {
        private readonly IMemoryCache cache;
        private readonly HttpClient? client;
        private readonly string apiKey;

        public ClimaService(HttpClient httpClient, IMemoryCache memoryCache, IConfiguration configuration)
        {
            cache = memoryCache;
            client = httpClient;
            apiKey = Environment.GetEnvironmentVariable("OPENWEATHERMAP_API_KEY")
                ?? configuration["OpenWeatherMap:ApiKey"]
                ?? string.Empty;

            if (string.IsNullOrEmpty(client?.BaseAddress?.ToString()) && client != null)
                client.BaseAddress = new Uri(ApiExtensions.WeatherApiUrl);

            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("API key not found");
        }

        /// <summary>
        /// Obtém o clima de uma cidade
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        public async Task<DadosClima> ObterClimaAsync(string cidade, string idioma = "pt_br")
        {
            if (client == null) throw new InvalidOperationException("HTTPClient não foi instanciado corretamente");

            var cacheKey = $"clima_{cidade}";
            if (cache.TryGetValue(cacheKey, out DadosClima? dadosCache) && dadosCache != null)
                return dadosCache;

            var url = $"weather?q={cidade}&lang={idioma}&appid={apiKey}&units=metric";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var dados = await response.Content.ReadFromJsonAsync<DadosClima>() ?? new();

            cache.Set(cacheKey, dados, TimeSpan.FromMinutes(10));

            return dados;
        }

        /// <summary>
        /// Obtém a previsão do tempo de uma cidade, para os próximos 5 dias
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        public async Task<DadosPrevisao> ObterPrevisaoAsync(string cidade, string idioma = "pt_br")
        {
            if (client == null) throw new InvalidOperationException("HTTPClient não foi instanciado corretamente");

            var cacheKey = $"previsao_{cidade}";
            if (cache.TryGetValue(cacheKey, out DadosPrevisao? dadosCache) && dadosCache != null)
                return dadosCache;

            var url = $"forecast?q={cidade}&lang={idioma}&appid={apiKey}&units=metric";
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var dados = await response.Content.ReadFromJsonAsync<DadosPrevisao>() ?? new();

            cache.Set(cacheKey, dados, TimeSpan.FromMinutes(10));

            return dados;
        }
    }
}
