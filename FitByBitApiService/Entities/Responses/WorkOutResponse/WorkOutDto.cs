using FitByBitApiService.Entities.Models;
using FitByBitApiService.Enum;

namespace FitByBitApiService.Entities.Responses.WorkOutResponse;

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
    //public Dictionary<string, List<Exercise>> GroupedExercises { get; set; }
}

public class WorkoutDto
{
    public string WorkoutName { get; set; } // Name of the exercise
    public FitnessGoal Category { get; set; } // Fitness goal category
    public CurrentFitnessLevel ExpertiseLevel { get; set; } // Expertise level
    //public Guid WorkoutId { get; set; }
}

public class AllWorkoutDto
{
    public string WorkoutName { get; set; } // Name of the exercise
    public FitnessGoal Category { get; set; } // Fitness goal category
    public CurrentFitnessLevel ExpertiseLevel { get; set; } // Expertise level
}

public class WorkoutListDto
{
    public string WorkoutName { get; set; } // Name of the exercise
    public Guid WorkoutId { get; internal set; }
}

public class AllExerciseDto
{
    public string WorkoutName { get; set; }

    public Dictionary<string, List<Exercise>> Exercises { get; set; }

}


public class WorkoutSearchParameters
{
    public CurrentFitnessLevel? FitnessLevel { get; set; }
    public FitnessGoal? Category { get; set; }
}


public class CreateWorkoutPlanDto
{
    public Guid WorkoutId { get; set; }
}


public class ExerciseDto
{
    public string ExerciseName { get; set; }
    public string Time { get; set; }
    public string Rest { get; set; }
    public string Set { get; set; }
    public string Reps { get; set; }
    public string Number { get; set; }
    public string ImageUrl { get; set; }
}


public class CreateWorkoutListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } // Assuming a 'Name' property exists in WorkoutPlan
    public DateTime Date { get; set; }
    public bool Status { get; set; }
}


public class UpdateWorkoutPlanDto
{
    public Guid WorkoutPlanId { get; set; }
}


public class GetWorkoutPlansByDateDto
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public string WorkoutName { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }
}



public class AllWorkoustDto
{
    public string WorkoutName { get; set; } // Name of the exercise
    public FitnessGoal Category { get; set; } // Fitness goal category
    public CurrentFitnessLevel ExpertiseLevel { get; set; } // Expertise level
    public Guid WorkoutId { get; set; }
}