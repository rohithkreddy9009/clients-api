using ClientApi.Models;

namespace ClientApi.Services;

public interface IClientService
{
    List<Client> GetClientsByCountryCode(string countryCode);
} 