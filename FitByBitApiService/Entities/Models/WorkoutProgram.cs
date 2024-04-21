using FitByBitApiService.Enum;

namespace FitByBitApiService.Entities.Models
{
    public class WorkoutProgram
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string WorkoutName { get; set; }
        public FitnessGoal Category { get; set; }
        public CurrentFitnessLevel ExpertiseLevel { get; set; }
    }
}
