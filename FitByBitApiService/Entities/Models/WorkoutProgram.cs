using FitByBitService.Common;
using FitByBitService.Enum;

namespace FitByBitApiService.Entities.Models
{
    public class WorkoutProgram
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ExerciseName { get; set; }
        public FitnessGoal Category { get; set; }
        public CurrentFitnessLevel ExpertiseLevel { get; set; }
    }
}
