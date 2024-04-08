using Newtonsoft.Json;

namespace FitByBitService.Configuration;

public class SmtpConfiguration
{
    [JsonProperty("SenderName")]
    public string SenderName { get; set; } = null!;

    [JsonProperty("FromEmail")]
    public string FromEmail { get; set; } = null!;

    [JsonProperty("SmtpHost")]
    public string SmtpHost { get; set; } = null!;

    [JsonProperty("SmtpPort")]
    public int SmtpPort { get; set; }

    [JsonProperty("SmtpUser")]
    public string SmtpUser { get; set; } = null!;

    [JsonProperty("SmtpPassword")]
    public string SmtpPassword { get; set; } = null!;
}
