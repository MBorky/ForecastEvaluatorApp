using ForecastEvaluator.DataModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForecastEvaluator.Services;

namespace TestingApp
{
    public class TestingWeatherServices
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly IWeatherService _weatherService;
        private readonly ForecastContext _context;
        public TestingWeatherServices()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var options = new DbContextOptionsBuilder<ForecastContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;
            _context = new ForecastContext(options);
            _weatherService = new WeatherService(_httpClientFactoryMock.Object, _context);
        }
        [Fact]
        public async Task FetchWeatherDataAsync_ShouldThrowException_ApiUrlKeyNULL()
        {
            // Arrange
            string urlKey = null;
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _weatherService.FetchweatherDataAsync(urlKey));
            Assert.Equal("API URL is null or empty. (Parameter 'apiUrlKey')", exception.Message);
        }
        [Fact]
        public async Task FetchWeatherDataAsync_ShouldThrowException_ApiUrlKeyInvalid()
        {
            // Arrange
            string urlKey = "fsdf";
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _weatherService.FetchweatherDataAsync(urlKey));
            Assert.Equal("API URL is invalid. (Parameter 'apiUrlKey')", exception.Message);
        }
    }
}
