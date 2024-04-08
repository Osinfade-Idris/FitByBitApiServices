using System.ComponentModel.DataAnnotations;

namespace FitByBitService.Entities.Responses.UserResponse;

public class ResetPasswordDto
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
}
