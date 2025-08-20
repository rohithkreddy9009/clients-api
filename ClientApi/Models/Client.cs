using CsvHelper.Configuration.Attributes;

namespace ClientApi.Models;

public class Client
{
    [Name("client_id")]
    public int ClientId { get; set; }
    
    [Name("name")]
    public string Name { get; set; } = string.Empty;
    
    [Name("tax_id")]
    public string TaxId { get; set; } = string.Empty;
    
    [Name("country_code")]
    public string CountryCode { get; set; } = string.Empty;
} 