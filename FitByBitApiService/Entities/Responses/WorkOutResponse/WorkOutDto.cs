using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FitByBitApiService.Entities.Models;
using FitByBitService.Common;
using FitByBitService.Enum;

namespace FitByBitService.Entities.Responses.WorkOutResponse;

public class UserWorkOutDto
{
    public Guid UserId { get; set; } // ID of the user associated with these workouts

    public string FirstName { get; set; } // First name of the user
    public string LastName { get; set; } // Last name of the user
    public string Email { get; set; } // Email address of the user
    public string PhoneNumber { get; set; } // Phone number of the user
    public DateTime Dob { get; set; } // Date of birth of the user
    public bool IsActive { get; set; } // Indicates whether the user is active

    public WorkoutDto[] Workouts { get; set; } // Array of workouts associated with the user
    public Dictionary<string, List<Exercise>> GroupedExercises { get; set; }
}

public class WorkoutDto
{
    public string ExerciseName { get; set; } // Name of the exercise
    public FitnessGoal Category { get; set; } // Fitness goal category
    public CurrentFitnessLevel ExpertiseLevel { get; set; } // Expertise level
}

public class AllWorkoutDto
{
    public string ExerciseName { get; set; } // Name of the exercise
    public FitnessGoal Category { get; set; } // Fitness goal category
    public CurrentFitnessLevel ExpertiseLevel { get; set; } // Expertise level
    public object Exercises { get; internal set; }
}


public class WorkoutSearchParameters
{
    public CurrentFitnessLevel? FitnessLevel { get; set; }
    public FitnessGoal? Category { get; set; }
}
