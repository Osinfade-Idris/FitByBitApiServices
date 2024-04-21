namespace FitByBitApiService.Helpers;

public class ExceptionGenericResponse
{
    public bool Success { get; set; }
    public string StatusCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; } = string.Empty;
}
