namespace ProjetoClima.API.Services
{
    /// <summary>
    /// Serviço para obter informações do clima
    /// </summary>
    public class ClimaService
    {
        private readonly HttpClient client;
        private readonly string apiKey;

        public ClimaService(HttpClient httpClient)
        {
            client = httpClient;
            apiKey = Environment.GetEnvironmentVariable("OPENWEATHERMAP_API_KEY") ?? string.Empty;

            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("API key not found");
        }

        /// <summary>
        /// Obtém o clima de uma cidade
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        public async Task<string> ObterClimaAsync(string cidade)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric";
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Obtém a previsão do tempo de uma cidade, para os próximos 5 dias
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        public async Task<string> ObterPrevisaoAsync(string cidade)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={cidade}&appid={apiKey}&units=metric";
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
