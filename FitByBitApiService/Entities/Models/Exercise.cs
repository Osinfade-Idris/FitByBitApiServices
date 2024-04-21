namespace FitByBitApiService.Entities.Models
{
    public class Exercise 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string ExerciseName { get; internal set; }
        public string? Time { get; set; }
        public string? Rest { get; set; }
        public string? Set { get; set; }
        public string? Reps { get; set; }
        public string Number { get; internal set; }
        public string? ImageUrl { get; set; } = null;

    }
}
