using System.ComponentModel.DataAnnotations;

namespace FitByBitApiService.Entities.Responses.UserResponse;

public class SetPasswordDto
{
    public string OtpCode { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [MinLength(8)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
