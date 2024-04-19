using FitByBitService.Common;
using FitByBitService.Enum;

namespace FitByBitApiService.Entities.Models
{
    public class WorkOutPlan : BaseEntity
    {
        public Guid WorkOutProgramId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }

    }
}
