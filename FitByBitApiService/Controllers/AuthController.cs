using FitByBitApiService.Entities.Responses;
using FitByBitApiService.Entities.Responses.UserResponse;
using FitByBitApiService.Helpers;
using FitByBitApiService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace FitByBitApiService.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]/[action]")]
public class AuthController : Controller
{
    private readonly IAuthRepository _authRepository;
    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Create Users.")]
    public async Task<ActionResult<GenericResponse<GeneralResponse>>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var response = await _authRepository.CreateUserAsync(createUserDto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<List<UserDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get all users.")]
    public async Task<ActionResult<GenericResponse<List<UserDto>>>> GetAllUsers()
    {
        var response = await _authRepository.GetAllUsersAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<UserDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get user by id.")]
    public async Task<ActionResult<GenericResponse<UserDto>>> GetUser()
    {
        // Retrieve the user ID from the claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Use the userId to get the user details from repository
        var response = await _authRepository.GetUserByIdAsync(userId);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Update user data.")]
    public async Task<ActionResult<GenericResponse<UserDto>>> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _authRepository.UpdateUserAsync(userId, updateUserDto);
        return StatusCode((int)response.StatusCode, response);
    }
    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Delete user record.")]
    public async Task<ActionResult<GenericResponse<GeneralResponse>>> DeleteUser(string id)
    {
        var response = await _authRepository.DeleteUserAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<CreateJwtTokenDto>))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Login to generate jwt token.")]
    public async Task<ActionResult<GenericResponse<GeneralResponse>>> UserLogin(LoginDto login)
    {
        var response = await _authRepository.LoginAsync(login);
        return StatusCode((int)response.StatusCode, response);
    }
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Password reset request.")]
    public async Task<ActionResult<GenericResponse<ResetPasswordDto>>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var response = await _authRepository.ResetPasswordAsync(resetPasswordDto);
        return StatusCode((int)response.StatusCode, response);
    }
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Set new password.")]
    public async Task<ActionResult<GenericResponse<GeneralResponse>>> SetPassword(SetPasswordDto setPassword)
    {
        var response = await _authRepository.SetPasswordAsync(setPassword);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Validate token.")]
    public async Task<ActionResult<GenericResponse<bool>>> ValidateToken()
    {
        var response = await _authRepository.ValidateTokenAsync(HttpContext);
        return StatusCode((int)response.StatusCode, response);

    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Validate email address.")]
    public async Task<ActionResult<GenericResponse<bool>>> ValidateEmail(string email)
    {
        var response = await _authRepository.ValidateEmailAsync(email);
        return StatusCode((int)response.StatusCode, response);

    }
}
