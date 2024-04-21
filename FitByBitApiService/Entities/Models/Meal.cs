using System.ComponentModel.DataAnnotations;
using FitByBitApiService.Common;

namespace FitByBitApiService.Entities.Models;

public class Meal : BaseEntity
{
    public string Name { get; set; }
    public string FoodGroup { get; set; }
    public string Calories { get; set; }
    public string? ImageUrl { get; set; } = null;
}
