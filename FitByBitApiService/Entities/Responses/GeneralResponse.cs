using System.Net;

namespace FitByBitService.Entities.Responses;

public class GeneralResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public HttpStatusCode StatusCode { get; set; }
    public object? Data { get; set; }
}