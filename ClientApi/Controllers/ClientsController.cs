using ClientApi.Models;
using ClientApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<ApiResponse<IEnumerable<Client>>> GetClients([FromQuery] string country_code)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(country_code))
            {
                _logger.LogWarning("Country code parameter is missing or empty");
                return BadRequest(ApiResponse<object>.CreateError(
                    string.Format(APIConstants.NoCountryCode)));
            }

            var clients = _clientService.GetClientsByCountryCode(country_code);

            if (!clients.Any())
            {
                _logger.LogInformation($"No clients found for country code: {country_code}");
                return Ok(ApiResponse<IEnumerable<Client>>.Success(
                    Enumerable.Empty<Client>(),
                    string.Format(APIConstants.NoRecordsFound, country_code)
                  ));
            }

            _logger.LogInformation($"Successfully retrieved {clients.Count()} clients for country code: {country_code}");

            return Ok(ApiResponse<IEnumerable<Client>>.Success(
                clients,
                    string.Format(APIConstants.SucessFetchingRecords, clients.Count(), country_code)));
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError("CSV file not found", ex, this);
            return StatusCode(500, ApiResponse<object>.CreateError(
                    string.Format(APIConstants.CsvFileNotFound)));
        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected error occurred while retrieving clients", ex, this);
            return StatusCode(500, ApiResponse<object>.CreateError(
                    string.Format(APIConstants.ErrorRetrievingClients)));
        }
    }
}