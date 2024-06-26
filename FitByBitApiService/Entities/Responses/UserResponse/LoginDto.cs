using System.ComponentModel.DataAnnotations;

namespace FitByBitApiService.Entities.Responses.UserResponse;

public class LoginDto
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
