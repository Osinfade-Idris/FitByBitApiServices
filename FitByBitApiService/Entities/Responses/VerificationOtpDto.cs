using System.ComponentModel.DataAnnotations;
using FitByBitService.Common;

namespace FitByBitService.Entities.Responses;

public class VerificationOtpDto : BaseEntity
{
    public Models.User User { get; set; }

    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public DateTimeOffset Expiry { get; set; }
}
