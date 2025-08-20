using ClientApi.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ClientApi.Services;

public class ClientService : IClientService
{
    private readonly ILogger<ClientService> _logger;
    private readonly string _csvFilePath;

    public ClientService(ILogger<ClientService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _csvFilePath = configuration["CsvFilePath"];
    }

    public List<Client> GetClientsByCountryCode(string countryCode)
    {
        try
        {            
            if (!File.Exists(_csvFilePath))
            {
                _logger.LogError("CSV file not found at path: {FilePath}", _csvFilePath);
                throw new FileNotFoundException($"CSV file not found at path: {_csvFilePath}");
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            using var reader = new StreamReader(_csvFilePath);
            using var csv = new CsvReader(reader, config);

            var clients = csv.GetRecords<Client>().ToList();

            if (clients.Any())
            {

                var filteredClients = clients
                    .Where(c => string.Equals(c.CountryCode, countryCode, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                _logger.LogInformation("Found {Count} clients for country code: {CountryCode}",
                    filteredClients.Count, countryCode);

                return filteredClients;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while reading clients for country code: {CountryCode}", countryCode);
            throw;
        }

        return null;
    }
} 