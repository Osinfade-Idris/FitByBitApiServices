using System.Net;
using Newtonsoft.Json;

namespace FitByBitApiService.Helpers;

public class GenericResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; }

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }
}
