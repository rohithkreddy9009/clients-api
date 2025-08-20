# Client API - .NET 8 Web API

A clean, well-structured .NET 8 Web API that provides client data filtering by country code from a CSV file.

## Features

- **.NET 8 LTS**: Built with the latest long-term support version
- **Clean Architecture**: Follows dependency injection and separation of concerns
- **CSV Data Source**: Reads client data from `Data/clients.csv`
- **RESTful API**: GET endpoint with query parameter filtering
- **Swagger Documentation**: Interactive API documentation
- **Comprehensive Logging**: Structured logging with Serilog
- **Error Handling**: Proper HTTP status codes and error responses
- **CORS Support**: Cross-origin resource sharing enabled

## Project Structure

```
project_dot_net/
├── ClientApi/
│   ├── Controllers/
│   │   └── ClientsController.cs
│   ├── Models/
│   │   └── Client.cs
│   ├── Services/
│   │   ├── IClientService.cs
│   │   └── ClientService.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── ClientApi.csproj
│   └── Program.cs
├── Data/
│   └── clients.csv
├── ClientApi.sln
└── README.md
```

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022, VS Code, or any .NET-compatible IDE

## Getting Started

1. **Clone or download the project**

2. **Navigate to the project directory**
   ```bash
   cd project_dot_net
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run --project ClientApi
   ```

6. **Access the API**
   - API Base URL: `https://localhost:7001` or `http://localhost:5000`
   - Swagger UI: `https://localhost:7001/swagger` or `http://localhost:5000/swagger`

## Authentication

The API is protected with a static bearer token authentication system.

### API Key
- **Header Name**: `X-API-Key`
- **Format**: `Bearer {token}`
- **Valid Token**: `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkNsaWVudEFQSSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c`

### Example Usage
```bash
curl -H "X-API-Key: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkNsaWVudEFQSSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" \
     http://localhost:5000/clients?country_code=US
```

## API Endpoints

### GET /clients

Retrieves clients filtered by country code.

**Authentication Required**: Yes

**Query Parameters:**
- `country_code` (required): The country code to filter by (e.g., US, CA, DE, AU, UK)

**Example Requests:**
```bash
# Get all US clients
GET /clients?country_code=US

# Get all Canadian clients
GET /clients?country_code=CA

# Get all German clients
GET /clients?country_code=DE
```

**Response Format:**
```json
{
  "status": true,
  "code": 200,
  "message": "Successfully retrieved 5 clients for country code: US",
  "data": [
    {
      "clientId": 4,
      "name": "John Smith",
      "taxId": "TX8543",
      "countryCode": "US"
    },
    {
      "clientId": 7,
      "name": "Liam Martinez",
      "taxId": "TX3458",
      "countryCode": "US"
    }
  ]
}
```

**Empty Response Format:**
```json
{
  "status": true,
  "code": 200,
  "message": "No records found for country code: INVALID",
  "data": []
}
```

**Error Response Format:**
```json
{
  "status": false,
  "code": 400,
  "message": "Country code parameter is required",
  "error": null
}
```

**HTTP Status Codes:**
- `200 OK`: Successfully retrieved clients
- `400 Bad Request`: Missing or invalid country_code parameter
- `401 Unauthorized`: Missing or invalid API key
- `500 Internal Server Error`: Server error or data source unavailable

## Data Structure

The CSV file (`Data/clients.csv`) contains the following columns:

| Column | Type | Description |
|--------|------|-------------|
| client_id | int | Unique client identifier |
| name | string | Client's full name |
| tax_id | string | Client's tax identification number |
| country_code | string | Two-letter country code (US, CA, DE, AU, UK) |

## Design Patterns & Best Practices

### 1. **Dependency Injection**
- Services are registered in the DI container
- Controllers receive dependencies through constructor injection

### 2. **Interface Segregation**
- `IClientService` interface defines the contract
- Implementation can be easily swapped or mocked

### 3. **Single Responsibility Principle**
- `ClientService` handles only CSV reading and filtering
- `ClientsController` handles only HTTP request/response logic

### 4. **Error Handling**
- Comprehensive try-catch blocks
- Proper HTTP status codes
- Structured logging for debugging

### 5. **Configuration Management**
- Environment-specific settings
- Proper logging configuration

### 6. **Async/Await Pattern**
- Non-blocking I/O operations
- Scalable for high-concurrency scenarios

## Testing the API

### Using Swagger UI
1. Navigate to `https://localhost:7001/swagger`
2. Click on the GET /clients endpoint
3. Click "Try it out"
4. Enter a country code (e.g., "US", "CA", "DE")
5. Click "Execute"

### Using curl
```bash
# Get US clients
curl -H "X-API-Key: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkNsaWVudEFQSSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" \
     -X GET "http://localhost:5000/clients?country_code=US"

# Get Canadian clients
curl -H "X-API-Key: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkNsaWVudEFQSSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" \
     -X GET "http://localhost:5000/clients?country_code=CA"
```

### Using PowerShell
```powershell
# Get US clients
$headers = @{
    "X-API-Key" = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkNsaWVudEFQSSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
Invoke-RestMethod -Uri "http://localhost:5000/clients?country_code=US" -Method Get -Headers $headers
```

## Available Country Codes

Based on the sample data, the following country codes are available:
- **US**: United States
- **CA**: Canada
- **DE**: Germany
- **AU**: Australia
- **UK**: United Kingdom

## Dependencies

- **CsvHelper**: For reading and parsing CSV files
- **Microsoft.AspNetCore.OpenApi**: For OpenAPI/Swagger support
- **Swashbuckle.AspNetCore**: For Swagger UI generation

## Future Enhancements

1. **Database Integration**: Replace CSV with a proper database
2. **Caching**: Implement response caching for better performance
3. **Authentication**: Add JWT-based authentication
4. **Validation**: Add more comprehensive input validation
5. **Pagination**: Support for large datasets
6. **Unit Tests**: Add comprehensive test coverage
7. **Docker Support**: Containerize the application

## Troubleshooting

### Common Issues

1. **CSV file not found**
   - Ensure `Data/clients.csv` exists in the project root
   - Check file permissions

2. **Port already in use**
   - Change ports in `launchSettings.json`
   - Or kill the process using the port

3. **SSL certificate issues**
   - Run `dotnet dev-certs https --trust` to trust the development certificate

## License

This project is provided as-is for demonstration purposes. 