using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using ProjetoClima.API.Models;
using ProjetoClima.API.Services;
using System.Net;
using System.Text.Json;

namespace ProjetoClima.API.Testes
{
    public class ClimaServiceTestes
    {
        private readonly Mock<HttpMessageHandler> messageHandlerMock;
        private readonly HttpClient client;
        private readonly Mock<IConfiguration> configurationMock;
        private readonly IMemoryCache memoryCache;

        public ClimaServiceTestes()
        {
            // Mock HttpMessageHandler for HttpClient
            messageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            client = new HttpClient(messageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/")
            };

            // Mock IConfiguration
            configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["OpenWeatherMap:ApiKey"]).Returns("fake-api-key");

            // Use real MemoryCache (in-memory, no external dependency)
            memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        [Fact]
        public async Task ObterClimaAsync_ReturnsDadosClima_WhenSucessoChamadaApi()
        {
            // Arrange
            var DadosClima = new DadosClima
            {
                Name = "London",
                Main = new DadosPrincipais { Temp = 15.0, FeelsLike = 14.0, Humidity = 80 },
                Weather = [new Clima { Description = "cloudy", Icon = "04d" }]
            };
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(DadosClima))
            };
            messageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var ClimaService = new ClimaService(client, memoryCache, configurationMock.Object);

            // Act
            var result = await ClimaService.ObterClimaAsync("London");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("London", result.Name);
            Assert.Equal(15.0, result.Main.Temp);
            Assert.Equal("cloudy", result.Weather[0].Description);
        }

        [Fact]
        public async Task ObterClimaAsync_ReturnsCachedData_WhenCidadeEstaEmCache()
        {
            // Arrange
            var cachedWeather = new DadosClima { Name = "Paris", Main = new DadosPrincipais { Temp = 20.0 } };
            memoryCache.Set("weather_Paris", cachedWeather, TimeSpan.FromMinutes(10));
            var ClimaService = new ClimaService(client, memoryCache, configurationMock.Object);

            // Act
            var result = await ClimaService.ObterClimaAsync("Paris");

            // Assert
            Assert.Equal(cachedWeather, result);
            messageHandlerMock
                .Protected()
                .Verify("SendAsync", Times.Never(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task ObterClimaAsync_ThrowsHttpRequestException_WhenFalhaChamadaApi()
        {
            // Arrange
            messageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("API failure"));
            var ClimaService = new ClimaService(client, memoryCache, configurationMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => ClimaService.ObterClimaAsync("Berlin"));
        }

        [Fact]
        public async Task ObterPrevisaoAsync_ReturnsDadosPrevisao_WhenSucessoChamadaApi()
        {
            // Arrange
            var DadosPrevisao = new DadosPrevisao
            {
                City = new Cidade { Name = "Tokyo" },
                List =
                [
                    new() { Dt = 1234567890, Main = new DadosPrincipais { Temp = 25.0 } }
                ]
            };
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(DadosPrevisao))
            };
            messageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var ClimaService = new ClimaService(client, memoryCache, configurationMock.Object);

            // Act
            var result = await ClimaService.ObterPrevisaoAsync("Tokyo");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tokyo", result.City.Name);
            Assert.Single(result.List);
            Assert.Equal(25.0, result.List[0].Main.Temp);
        }
    }
}
