using System.ComponentModel.DataAnnotations;

namespace FitByBitService.Entities.Responses.UserResponse;

public class VerifyUserDto
{
    public string OtpCode { get; set; } = null!;
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}
