using FitByBitService.Common;
using FitByBitService.Entities.Models;
using FitByBitService.Enum;

namespace FitByBitApiService.Entities.Models
{
    public class MealPlan : BaseEntity
    {
        public Guid MealId { get; set; }
        public MealType MealType { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }

    }

    public class MealPlanDataDto
    {
        public List<Guid> MealIds { get; set; }
        public MealType MealType { get; set; }
        public DateTime Date { get; set; }
    }

    public class UserMealPlanViewModel
    {
        public string UserId { get; set; }
        public List<UserMealPlanDateViewModel> MealPlans { get; set; }
    }

    public class UserMealPlanDateViewModel
    {
        public DateTime Date { get; set; }
        public Dictionary<MealType, List<string>> Meals { get; set; }
    }
}
