using FitByBitApiService.Entities.Responses;
using FitByBitApiService.Entities.Responses.UserResponse;
using FitByBitApiService.Helpers;

namespace FitByBitApiService.Repositories;

public interface IAuthRepository
{
    Task<GenericResponse<CreateJwtTokenDto>> CreateUserAsync(CreateUserDto createUserDto);
    Task<GenericResponse<List<UserDto>>> GetAllUsersAsync();
    Task<GenericResponse<UserDto>> GetUserByIdAsync(string id);
    Task<GenericResponse<GeneralResponse>> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
    Task<GenericResponse<GeneralResponse>> DeleteUserAsync(string guid);
    public Task<GenericResponse<CreateJwtTokenDto>> LoginAsync(LoginDto loginDto);
    public Task<GenericResponse<GeneralResponse>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    public Task<GenericResponse<GeneralResponse>> SetPasswordAsync(SetPasswordDto setPasswordDto);
    public Task<GenericResponse<bool>> ValidateTokenAsync(HttpContext httpContext);
    public Task<GenericResponse<bool>> ValidateEmailAsync(string email);
}
