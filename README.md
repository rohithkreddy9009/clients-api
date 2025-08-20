# Clients API

A production-ready **.NET 9 Web API** that retrieves client records from a CSV file.  
The API supports filtering by `country_code`, implements authentication, includes unit tests, and follows clean coding practices.

---

## 📌 Project Overview
This project was built as part of an assessment with the following goals:
- Create a **.NET Core Web API** using the latest LTS version.
- Read client records from a **CSV file** (`clients.csv`) placed in the `Data/` directory.
- Implement a `GET` endpoint to return all clients for a given `country_code`.
- Ensure the code reflects **production readiness**: edge cases handled, authentication added, and test coverage included.

---

## ⚙️ Features
- **CSV Data Handling**
  - Reads client data from `Data/clients.csv`.
  - Validates CSV structure and missing/invalid values.
  - Gracefully handles empty or corrupted CSV rows.

- **Filtering by Country**
  - `GET /api/clients?country=US`
  - Returns only clients matching the specified country code.
  - Case-insensitive match (`us`, `Us`, `US` are all valid).

- **Authentication**
  - API endpoints secured with an **API Key**.
  - Example:  
    ```
    GET /api/clients?country=US
    Header: X-Api-Key: <your_api_key>
    ```

- **Error Handling & Edge Cases**
  - Invalid or missing `country_code` returns a `400 Bad Request`.
  - Missing/invalid API Key returns a `401 Unauthorized`.
  - Empty results for a valid country return `200 OK` with an empty array.
  - Global exception handling middleware to return structured error responses.

- **Unit Tests**
  - Includes xUnit test project (`ClientApi.Tests`).
  - Covers service logic, CSV parsing, and controller responses.
  - Mocking used for services to isolate test cases.

- **API Documentation**
  - Integrated **Swagger/OpenAPI** UI available at:
    ```
    https://localhost:<port>/swagger
    ```

---

## 🗂️ Project Structure

```
ClientsApi/
│   ClientApi.sln
│   README.md
│
├── ClientApi/                # Main Web API project
│   ├── Controllers/          # API controllers (ClientsController, TokenController)
│   ├── Models/               # DTOs and domain models
│   ├── Services/             # Business logic & CSV handling
│   ├── Data/                 # CSV data file
│   ├── appsettings.json
│   └── Program.cs
│
└── ClientApi.Tests/          # Unit test project (xUnit)
```
---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 (or VS Code with C# extension)

### Setup & Run
1. Clone this repository:
   ```bash
   git clone https://github.com/rohithkreddy9009/clients-api.git
   cd clients-api
2. Build the solution:
   ```bash
   dotnet build
   ```
3. Run the API:
   ```bash
   dotnet run --project ClientApi/ClientApi.csproj
   ```
4. Open Swagger UI:
   https://localhost:7001/swagger

### 🔑 Authentication

The API requires an API Key for all requests.

1. The key is configured in appsettings.json:
{
  "ApiKey": "your_secret_key"
}

2. Include the key in the request header:
   ```
   X-Api-Key: your_secret_key
   ```
### 🧪 Running Tests

Unit tests are included in the ClientApi.Tests project.

Run them using: dotnet test


## ✅ Highlights (Production Readiness):

🔐 Authentication via API Key

🧪 Unit tests with xUnit

📊 Swagger/OpenAPI documentation

⚡ Edge case handling (invalid input, empty results, bad CSV)

🏗️ Clean architecture with Controllers → Services → Models

🧹 Configurable appsettings.json for API Key & CSV path


## 🔮 Future Improvements:

📦 Containerization with Docker

📈 Logging & monitoring (Serilog + Application Insights)

🔄 CI/CD pipeline (GitHub Actions or Azure DevOps)

🗄️ Switch to a database backend for larger datasets

🛡️ JWT-based authentication for multi-user scenarios