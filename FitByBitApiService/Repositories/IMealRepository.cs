using FitByBitApiService.Entities.Models;
using FitByBitService.Entities.Responses.MealResponse;
using FitByBitService.Entities.Responses.UserResponse;
using FitByBitService.Helpers;

namespace FitByBitService.Repositories;

public interface IMealRepository
{
    Task<GenericResponse<List<FoodGroupDto>>> GetAllFoodGroupsAsync();
    Task<GenericResponse<List<MealDto>>> GetAllMealsByFoodGroupAsync(string id);
    Task<GenericResponse<MealPlan>> CreateMealPlan(IEnumerable<MealPlanDataDto> mealPlanDataList, string userId);



    Task<GenericResponse<List<UserMealPlanViewModel>>> GetMealPlansGroupedByUserIdAndDate(string userId);


}
