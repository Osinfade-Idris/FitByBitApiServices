using FitByBitApiService.Entities.Models;
using FitByBitApiService.Entities.Responses.MealResponse;
using FitByBitApiService.Helpers;

namespace FitByBitApiService.Repositories;

public interface IMealRepository
{
    Task<GenericResponse<List<FoodGroupDto>>> GetAllFoodGroupsAsync();
    Task<GenericResponse<List<MealDto>>> GetAllMealsByFoodGroupAsync(string id);
    Task<GenericResponse<MealPlan>> CreateMealPlan(IEnumerable<MealPlanDataDto> mealPlanDataList, string userId);
    Task<GenericResponse<List<UserMealPlanViewModel>>> GetMealPlansGroupedByUserIdAndDate(DateTime date, string userId);
}
