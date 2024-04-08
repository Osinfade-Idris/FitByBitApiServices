namespace FitByBitService.Entities.Responses.UserResponse;

public class CreateJwtTokenDto
{
    public UserDto User { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    //public string[] Roles { get; internal set; }
}
