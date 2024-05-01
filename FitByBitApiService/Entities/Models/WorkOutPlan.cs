using FitByBitApiService.Common;

namespace FitByBitApiService.Entities.Models
{
    public class WorkOutPlan : BaseEntity
    {
        public Guid WorkoutId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; } = false;

    }
}
