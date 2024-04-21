using System.ComponentModel.DataAnnotations;
using FitByBitApiService.Common;

namespace FitByBitApiService.Entities.Models;

public class VerificationOtp : BaseEntity
{
    public virtual User User { get; set; }
    public string UserId { get; set; }

    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public DateTimeOffset Expiry { get; set; }
}
