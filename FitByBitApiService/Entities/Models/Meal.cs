using System.ComponentModel.DataAnnotations;
using FitByBitService.Common;

namespace FitByBitService.Entities.Models;

public class Meal : BaseEntity
{
    public string Name { get; set; }
    public string FoodGroup { get; set; }
    public string Calories { get; set; }
    public string? ImageUrl { get; set; } = null;
}
