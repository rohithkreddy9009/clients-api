namespace ClientApi
{
    public static class APIConstants
    {
            public static string NoRecordsFound => "No records found for country code: {0}";

            public static string NoCountryCode => "Country code parameter is missing or empty";

            public static string SucessFetchingRecords => "Successfully retrieved {0} clients for country code: {1}";

            public static string CsvFileNotFound => "Csv File Not Found";

            public static string ErrorRetrievingClients => "Unexpected error occurred while retrieving clients";
    }
}
