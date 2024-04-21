using FitByBitApiService.Entities.Responses.WorkOutResponse;
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
    [SwaggerOperation(Summary = "Get user work out.")]
    public async Task<ActionResult<GenericResponse<UserWorkOutDto>>> GetUserWorkOut()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _workOutRepository.GetUserWorkOutByIdAsync(userId);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<AllWorkoutDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get all workouts.")]
    public async Task<ActionResult<GenericResponse<AllWorkoutDto>>> GetAllWorkOuts([FromQuery] WorkoutSearchParameters searchParameters = null)
    {
        var response = await _workOutRepository.GetAllWorkoutsAsync(searchParameters);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<AllWorkoutDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get workout by id.")]
    public async Task<ActionResult<GenericResponse<AllWorkoutDto>>> GetExercisesByWorkoutName(string name)
    {
        var response = await _workOutRepository.GetWorkoutExercisesByName(name);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Create a workout plan.")]
    public async Task<ActionResult<GenericResponse<string>>> CreateWorkoutPlan(CreateWorkoutPlanDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _workOutRepository.CreateWorkOutPlan(model.Date, Guid.Parse(userId), model.WorkoutIds);

        return StatusCode((int)response.StatusCode, response);
    }
}
