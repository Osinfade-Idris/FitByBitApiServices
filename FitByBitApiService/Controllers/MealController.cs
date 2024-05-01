using FitByBitApiService.Entities.Models;
using FitByBitApiService.Entities.Responses.MealResponse;
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
public class MealController : Controller
{
    private readonly IAuthRepository _authRepository;
    private readonly IWorkOutRepository _workOutRepository;
    private readonly IMealRepository _mealRepository;

    public MealController(IAuthRepository authRepository, IMealRepository mealRepository)
    {
        _authRepository = authRepository;
        _mealRepository = mealRepository;
    }
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<FoodGroupDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get all food groups.")]
    public async Task<ActionResult<GenericResponse<MealDto>>> GetAllFoodGroups()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _mealRepository.GetAllFoodGroupsAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<FoodGroupDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get all meals by food group id.")]
    public async Task<ActionResult<GenericResponse<MealDto>>> GetAllMealsByFoodGroupId(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _mealRepository.GetAllMealsByFoodGroupAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<FoodGroupDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Create Meal Plan for the logged in user.")]
    public async Task<ActionResult<GenericResponse<MealDto>>> CreateMealPlan([FromBody] IEnumerable<MealPlanDataDto> mealPlanDataList)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _mealRepository.CreateMealPlan(mealPlanDataList, userId);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<FoodGroupDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<>))]
    [SwaggerOperation(Summary = "Get the Meal Plans for the logged in user.")]
    public async Task<ActionResult<GenericResponse<MealDto>>> GetMealPlans(DateTime date)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _mealRepository.GetMealPlansGroupedByUserIdAndDate(date, userId);
        return StatusCode((int)response.StatusCode, response);
    }
}
