using System.Net;
using ForecastEvaluator.Services;
using ForecastEvaluator.DataModels;
using static TestingApp.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System.Globalization;
using Moq;
using Moq.Protected;

namespace TestingApp
{
    public class AnemometerServiceTests
    {
        private readonly ForecastContext _context;
        public AnemometerServiceTests()
        {
             var options = new DbContextOptionsBuilder<ForecastContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ForecastContext(options);
        }
        [Fact]
        public async Task FetchAnemometerDataAsync_ReturnsCorrectData()
        {
            {
                var mockHttpClientFactory = new Mock<IHttpClientFactory>();
                var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
                var contentString = "12/05/24 12:21:34 -4.6 19 -24.8 6.0 7.2 225 0.0 0.0 1016.0 SW 2 kts C hPa mm 35.6 -0.2 901.5 3548.7 888.9 23.8 37 -9.1 +2.1 -0.5 06:56 -11.0 02:33 7.2 09:33 8.6 09:27 1016.5 09:34 1015.2 00:01 3.9.6 3101 8.0 -4.6 -4.6 0.0 0.00 0 325 0.0 15 1 0 NW 8259 ft -10.5 0.0 803 0 -9.1";
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(contentString)
                };
                mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(response);

                var client = new HttpClient(mockHttpMessageHandler.Object);
                mockHttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);


                // Arrange
                var service = new AnemometerService(mockHttpClientFactory.Object, _context);

                // Act
                var result = await service.FetchAnemometerDataAsync("ANEMOMETER");

                // Assert
                Assert.NotNull(result);
                Assert.Contains("12/05/24 12:21:34 -4.6 19 -24.8 6.0 7.2 225 0.0 0.0 1016.0 SW 2 kts C hPa mm 35.6 -0.2 901.5 3548.7 888.9 23.8 37 -9.1 +2.1 -0.5 06:56 -11.0 02:33 7.2 09:33 8.6 09:27 1016.5 09:34 1015.2 00:01 3.9.6 3101 8.0 -4.6 -4.6 0.0 0.00 0 325 0.0 15 1 0 NW 8259 ft -10.5 0.0 803 0 -9.1", result);
            }
        }
        /*[Fact]
        public async Task SaveAnemometerDataAsync_SavesDataCorrectly()
        {
            //Arrange
            string[] testData = new string[50];
            for (int i = 0; i < testData.Length; i++)
            {
                testData[i] = "";
            }
            testData[0] = "12/05/24";
            testData[5] = "10.4";
            testData[40] = "12.5";
            testData[7] = "230";


            var httpClientFactory = A.Fake<IHttpClientFactory>();
            var service = new AnemometerService(httpClientFactory, _context);

            //Act
           await service.SaveAnemometerDataAsync(testData);

            //Assert
           var savedData = _context.AnemometerReadings.First();
            Assert.NotNull(savedData);
            Assert.Equal(DateOnly.ParseExact(testData[0], "dd/MM/yy", CultureInfo.InvariantCulture), savedData.ReadingDate);
            Assert.Equal(decimal.Parse(testData[5]), savedData.WindSpeed);
            Assert.Equal(decimal.Parse(testData[40]), savedData.WindGusts);
            Assert.Equal(int.Parse(testData[7]), savedData.WindDirection);
        }*/
    }
}
