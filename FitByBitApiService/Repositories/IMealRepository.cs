using FitByBitService.Entities.Responses.MealResponse;
using FitByBitService.Helpers;

namespace FitByBitService.Repositories;

public interface IMealRepository
{
    Task<GenericResponse<List<FoodGroupDto>>> GetAllFoodGroupsAsync();
    Task<GenericResponse<List<MealDto>>> GetAllMealsByFoodGroupAsync(string id);
}
