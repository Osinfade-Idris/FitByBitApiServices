using FitByBitService.Entities.Responses;
using FitByBitService.Entities.Responses.UserResponse;
using FitByBitService.Entities.Responses.WorkOutResponse;
using FitByBitService.Helpers;
using FitByBitService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace FitByBitService.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]/[action]")]
public class WorkOutController : Controller
{
    private readonly IAuthRepository _authRepository;
    private readonly IWorkOutRepository _workOutRepository;

    public WorkOutController(IAuthRepository authRepository, IWorkOutRepository workOutRepository)
    {
        _authRepository = authRepository;
        _workOutRepository = workOutRepository;
    }
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<UserWorkOutDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get user work out by id.")]
    public async Task<ActionResult<GenericResponse<UserWorkOutDto>>> GetUserWorkOutById()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _workOutRepository.GetUserWorkOutByIdAsync(userId);
        return StatusCode((int)response.StatusCode, response);
    }
}
