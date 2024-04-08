using System.ComponentModel.DataAnnotations;


namespace FitByBitService.Entities.Responses.UserResponse
{
    public class UserLogin
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
