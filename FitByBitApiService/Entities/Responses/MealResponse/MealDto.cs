using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FitByBitApiService.Entities.Models;
using FitByBitService.Common;
using FitByBitService.Enum;

namespace FitByBitService.Entities.Responses.MealResponse;

public class MealDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FoodGroup { get; set; }
    public string Calories { get; set; }
    public string? ImageUrl { get; set; } = null;

}

public class FoodGroupDto
{
    public int Id { get; internal set; }
    public object Name { get; internal set; }
}

public class CreateMealPlanDto 
{
    public Guid MealPlanId { get; set; }
    public Guid MealId { get; set; } 
    public MealType MealType { get; set; } 
    public DateTime Date { get; set; }
}
