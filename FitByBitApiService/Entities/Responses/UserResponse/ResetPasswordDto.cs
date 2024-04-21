using System.ComponentModel.DataAnnotations;

namespace FitByBitApiService.Entities.Responses.UserResponse;

public class ResetPasswordDto
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
}
