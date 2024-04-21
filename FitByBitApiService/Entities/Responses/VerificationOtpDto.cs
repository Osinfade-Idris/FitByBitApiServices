using System.ComponentModel.DataAnnotations;
using FitByBitApiService.Common;

namespace FitByBitApiService.Entities.Responses;

public class VerificationOtpDto : BaseEntity
{
    public Models.User User { get; set; }

    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public DateTimeOffset Expiry { get; set; }
}
