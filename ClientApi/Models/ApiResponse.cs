namespace ClientApi.Models;

public class ApiResponse<T>
{
    public bool Status { get; set; }    
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Status = true,            
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> CreateError(string message)
    {
        return new ApiResponse<T>
        {
            Status = false,            
            Message = message,       
        };
    }
} 