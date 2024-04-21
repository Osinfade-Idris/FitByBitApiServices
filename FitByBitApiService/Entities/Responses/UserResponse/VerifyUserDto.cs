using System.ComponentModel.DataAnnotations;

namespace FitByBitApiService.Entities.Responses.UserResponse;

public class VerifyUserDto
{
    public string OtpCode { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}
