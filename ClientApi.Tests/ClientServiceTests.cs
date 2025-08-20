
using ClientApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace ClientApi.Tests
{
    public class ClientServiceTests
    {
        private readonly ILogger<ClientService> _logger;
        private IClientService _clientService;
        private readonly Mock<IConfiguration> mockConfiguration;

        public ClientServiceTests()
        {
            var logger = new Mock<ILogger<ClientService>>();
            var environment = new Mock<IWebHostEnvironment>();
              mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["CsvFilePath"]).Returns("C:\\Users\\Rohith Kethireddy\\source\\repos\\project-dot-net-csv-data\\Data\\clients.csv");

            environment.Setup(x => x.ContentRootPath).Returns(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\.."));
            _logger = logger.Object;
            _clientService = new ClientService(_logger, mockConfiguration.Object);
        }

        void Setup(ILogger<ClientService> logger, IWebHostEnvironment webHostEnvironment) {
            _clientService = new ClientService(logger, mockConfiguration.Object);
        }

        [Fact]
        public void  GetCountryCode_WhiteSpaceReturnEmpty()
        {
            Assert.Empty(_clientService.GetClientsByCountryCode(""));

        }

        [Fact]
        public void CSVFileNotFound_ThrowsException()
        {
            var webHostEnvironment = new Mock<IWebHostEnvironment>();
            mockConfiguration.Setup(c => c["CsvFilePath"]).Returns("/NotExist/");

            Setup(_logger, webHostEnvironment.Object);
            Assert.Throws<FileNotFoundException> (() => _clientService.GetClientsByCountryCode("AU"));
        }

        [Fact]
        public void CountryNotFound_ReturnsEmpty()
        {
            Assert.Empty(_clientService.GetClientsByCountryCode("NE"));
        }

        [Fact]
        public void ExpectedClientByCountryReturnsList()
        {
            Assert.NotEmpty( _clientService.GetClientsByCountryCode("AU"));
        }

    }
}
