using FitByBitApiService.Entities.Models;
using FitByBitService.Entities.Models;
using FitByBitService.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FitByBitService.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    private double CalculateBmi(double height, double weight)
    {
        // Formula for BMI: BMI = weight (kg) / (height (m) * height (m))
        return Math.Round(weight / (height * height), 2);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<IdentityUserToken<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityRole>();
        builder.Ignore<IdentityUserRole<string>>();

/*        var customRoles = new List<IdentityRole>()
        {
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Superuser", NormalizedName = "Superuser".ToUpper()},
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "User".ToUpper()},
        };

        builder.Entity<IdentityRole>().HasData(customRoles);*/

        var customMeals = new List<Meal>()
        {
            new Meal { Id = Guid.NewGuid(), Name = "White Rice", FoodGroup = "1", Calories = "130"},
            new Meal { Id = Guid.NewGuid(), Name = "Medium Grain White Rice", FoodGroup = "1", Calories = "130"},
            new Meal { Id = Guid.NewGuid(), Name = "Rice Noodles (Cooked)", FoodGroup = "1", Calories = "108"},
            new Meal { Id = Guid.NewGuid(), Name = "Brown Rice", FoodGroup = "1", Calories = "123"},
            new Meal { Id = Guid.NewGuid(), Name = "Beef And Rice No Sauce", FoodGroup = "2", Calories = "145"},
            new Meal { Id = Guid.NewGuid(), Name = "Beef And Rice With Tomato-Based Sauce", FoodGroup = "2", Calories = "136"},
            new Meal { Id = Guid.NewGuid(), Name = "Chili Con Carne With Beans And Rice", FoodGroup = "2", Calories = "112"},
            new Meal { Id = Guid.NewGuid(), Name = "Pork And Rice With Tomato-Based Sauce", FoodGroup = "2", Calories = "139"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Or Turkey And Rice No Sauce", FoodGroup = "2", Calories = "148"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Or Turkey And Rice With Tomato-Based Sauce", FoodGroup = "2", Calories = "141"},
            new Meal { Id = Guid.NewGuid(), Name = "Fish And Rice With Tomato-Based Sauce", FoodGroup = "4", Calories = "135"},
            new Meal { Id = Guid.NewGuid(), Name = "Beef, Rice And Vegetables", FoodGroup = "2", Calories = "147"},
            new Meal { Id = Guid.NewGuid(), Name = "Pork, Rice And Vegetables ", FoodGroup = "2", Calories = "128"},
            new Meal { Id = Guid.NewGuid(), Name = "Cereal (Kellogg's Rice Krispies)", FoodGroup = "3", Calories = "381"},
            new Meal { Id = Guid.NewGuid(), Name = "Whole Wheat Pasta", FoodGroup = "1", Calories = "149"},
            new Meal { Id = Guid.NewGuid(), Name = "Pasta Cooked", FoodGroup = "1", Calories = "157"},
            new Meal { Id = Guid.NewGuid(), Name = "Pasta Whole Grain Cooked", FoodGroup = "1", Calories = "148"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Breast Baked", FoodGroup = "2", Calories = "192"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Breast Grilled", FoodGroup = "2", Calories = "176"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Leg Drumstick And Thigh Baked", FoodGroup = "2", Calories = "226"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Drumstick Baked", FoodGroup = "2", Calories = "190"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Drumstick Grilled", FoodGroup = "2", Calories = "200"},
            new Meal { Id = Guid.NewGuid(), Name = "Chicken Drumstick Fried", FoodGroup = "2", Calories = "211"},
            new Meal { Id = Guid.NewGuid(), Name = "Wild Atlantic Salmon", FoodGroup = "4", Calories = "182"},
            new Meal { Id = Guid.NewGuid(), Name = "Smoked Salmon", FoodGroup = "4", Calories = "117"},
            new Meal { Id = Guid.NewGuid(), Name = "Canned Salmon", FoodGroup = "4", Calories = "167"},
            new Meal { Id = Guid.NewGuid(), Name = "Canned Pink Salmon", FoodGroup = "4", Calories = "136"},
            new Meal { Id = Guid.NewGuid(), Name = "Farmed Atlantic Salmon", FoodGroup = "4", Calories = "206"},
            new Meal { Id = Guid.NewGuid(), Name = "Salmon Baked", FoodGroup = "4", Calories = "183"},
            new Meal { Id = Guid.NewGuid(), Name = "Fortified Fruit Juice Smoothie", FoodGroup = "5", Calories = "71"},
            new Meal { Id = Guid.NewGuid(), Name = "Apples", FoodGroup = "5", Calories = "52"},
            new Meal { Id = Guid.NewGuid(), Name = "Apples (Without Skin)", FoodGroup = "5", Calories = "48"},
            new Meal { Id = Guid.NewGuid(), Name = "Bananas", FoodGroup = "5", Calories = "89"}

        };

        builder.Entity<Meal>().HasData(customMeals);

        var customSuperUsers = new List<User>()
        {
            new User
            {
                FirstName = "Super",
                LastName = "User",
                Email = "superuserD@gmail.com",
                NormalizedEmail = "superuserD@gmail.com".ToUpper(),
                UserName = "superuser@gmail.com",
                NormalizedUserName = "superuser@gmail.com".ToUpper(),
                PhoneNumber = "08130927418",
                PhoneNumberConfirmed = true,
                IsActive = true,
                Age = 23,
                FitnessGoal = 1,
                CurrentFitness = 1,
                Height = 1.83,
                StartingWeight = 80,
                Bmi = CalculateBmi(6.5, 80), // Use the CalculateBmi method
                EmailConfirmed = true,

            },
            new User
            {
                FirstName = "Idris",
                LastName = "Osinfade",
                Email = "osinfadeidris@gmail.com",
                NormalizedEmail = "osinfadeidris@gmail.com".ToUpper(),
                UserName = "osinfadeidris@gmail.com",
                NormalizedUserName = "osinfadeidris@gmail.com".ToUpper(),
                PhoneNumber = "07069576909",
                PhoneNumberConfirmed = true,
                IsActive = true,
                Age = 23,
                FitnessGoal = 1,
                CurrentFitness = 1,
                Height = 1.83,
                StartingWeight = 80,
                Bmi = CalculateBmi(6.5, 80), // Use the CalculateBmi method
                EmailConfirmed = true,
            },
        };

        var passwordHasher = new PasswordHasher<User>();
        var hashedPassword = passwordHasher.HashPassword(customSuperUsers[0], "Password@123");

        foreach (var user in customSuperUsers)
        {
            user.PasswordHash = hashedPassword;
        }

        builder.Entity<User>().HasData(customSuperUsers);

/*        var identityUserRoles = new List<IdentityUserRole<string>>()
        {
            new IdentityUserRole<string> { UserId = customSuperUsers[0].Id.ToString(), RoleId = customRoles[0].Id.ToString() },
            new IdentityUserRole<string> { UserId = customSuperUsers[1].Id.ToString(), RoleId = customRoles[0].Id.ToString() }
        };
        builder.Entity<IdentityUserRole<string>>().HasData(identityUserRoles);
*/
        var customExercises = new List<Exercise>()
        {
            new Exercise { Name = "20 Minute HIIT Workout", ExerciseName= "Forward Lunge",  Time = "30 sec", Rest = "10 sec", Number = "1"},
            new Exercise { Name = "20 Minute HIIT Workout", ExerciseName= "Bodyweight Squat",  Time = "30 sec", Rest = "10 sec", Number = "2"},
            new Exercise { Name = "20 Minute HIIT Workout", ExerciseName= "Mountain Climber",  Time = "30 sec", Rest = "10 sec", Number = "3"},
            new Exercise { Name = "20 Minute HIIT Workout", ExerciseName= "Jogging in Place",  Time = "30 sec", Rest = "10 sec", Number = "4"},
            new Exercise { Name = "20 Minute HIIT Workout", ExerciseName= "Burpees",  Time = "30 sec", Rest = "10 sec", Number = "5"},
            new Exercise { Name = "20 Minute HIIT Workout", ExerciseName= "High Kick",  Time = "30 sec", Rest = "10 sec", Number = "6"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Jumping Jacks",  Time = "40-50 sec", Rest = "10-20 sec", Number = "1"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Push Ups",  Time = "40-50 sec", Rest = "10-20 sec", Number = "2"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Crunches",  Time = "40-50 sec", Rest = "10-20 sec", Number = "3"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Squat Jumps",  Time = "40-50 sec", Rest = "10-20 sec", Number = "4"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Mountain Climber",  Time = "40-50 sec", Rest = "10-20 sec", Number = "5"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Burpees",  Time = "40-50 sec", Rest = "10-20 sec", Number = "6"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Jogging in Place",  Time = "40-50 sec", Rest = "10-20 sec", Number = "7"},
            new Exercise { Name = "30 Minute Cardio Workout", ExerciseName= "Jumping Lunges",  Time = "40-50 sec", Rest = "10-20 sec", Number = "8"},
            new Exercise { Name = "45 Minute HIIT Workout", ExerciseName= "Burpees",  Time = "30 sec", Rest = "10 sec", Number = "1"},
            new Exercise { Name = "45 Minute HIIT Workout", ExerciseName= "Squat Jumps",  Time = "30 sec", Rest = "10 sec", Number = "2"},
            new Exercise { Name = "45 Minute HIIT Workout", ExerciseName= "Crunches",  Time = "30 sec", Rest = "10 sec", Number = "3"},
            new Exercise { Name = "45 Minute HIIT Workout", ExerciseName= "Jumping Jacks",  Time = "30 sec", Rest = "10 sec", Number = "4"},
            new Exercise { Name = "45 Minute HIIT Workout", ExerciseName= "Jumping Lunges",  Time = "30 sec", Rest = "10 sec", Number = "5"},
            new Exercise { Name = "45 Minute HIIT Workout", ExerciseName= "Push Ups",  Time = "30 sec", Rest = "10 sec", Number = "6"},
            new Exercise { Name = "Beginner Strength Training Workout", ExerciseName= "Deadlift",  Set = "3", Reps = "12, 10, 8", Number = "1"},
            new Exercise { Name = "Beginner Strength Training Workout", ExerciseName= "Leg Press",  Set = "3", Reps = "12, 10, 8", Number = "2"},
            new Exercise { Name = "Beginner Strength Training Workout", ExerciseName= "Walking Lunge",  Set = "3", Reps = "12, 10, 8", Number = "3"},
            new Exercise { Name = "Beginner Strength Training Workout", ExerciseName= "Cruches",  Set = "3", Reps = "12, 12, 12", Number = "4"},
            new Exercise { Name = "Beginner Strength Training Workout", ExerciseName= "Lateral Raise",  Set = "3", Reps = "12, 10, 8", Number = "5"},
            new Exercise { Name = "Beginner Strength Training Workout", ExerciseName= "Hammer Dumbbell Curl",  Set = "3", Reps = "12, 10, 9", Number = "6"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Goblet Squat",  Set = "3", Reps = "12, 10, 8", Number = "1"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Stiff Leg Deadlift",  Set = "3", Reps = "12, 10, 8", Number = "2"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Leg Press",  Set = "3", Reps = "12, 10, 8", Number = "3"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Walking Lunge",  Set = "3", Reps = "12, 10, 8", Number = "4"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Seated Calf Raise",  Set = "3", Reps = "12, 10, 8", Number = "5"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Crunches",  Set = "3", Reps = "12, 12, 12", Number = "6"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Lateral Raise",  Set = "3", Reps = "12, 10, 8", Number = "7"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Hammer Dumbbell Curl",  Set = "3", Reps = "12, 10, 9", Number = "8"},
            new Exercise { Name = "Intermediate Fat Loss Workout", ExerciseName= "Straight Bar Tricep Extension",  Set = "3", Reps = "12, 10, 10", Number = "9"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Burpee",  Set = "2", Reps = "10-15", Number = "1"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Push Up",  Set = "2", Reps = "10-15", Number = "2"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Floor Crunch",  Set = "2", Reps = "10-15", Number = "3"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Squat Jump",  Set = "2", Reps = "10-15", Number = "4"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Bicycle Crunch",  Set = "2", Reps = "10-15", Number = "5"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Mountain Climber",  Set = "2", Reps = "10-15", Number = "6"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Reverse or Walking Lunge",  Set = "2", Reps = "10-15", Number = "7"},
            new Exercise { Name = "Beginner At Home Workout", ExerciseName= "Star Jumps",  Set = "2", Time = "1 min", Number = "8"},
        };
        builder.Entity<Exercise>().HasData(customExercises);

        var customWorkouts = new List<WorkoutProgram>()
        {
            new WorkoutProgram {ExerciseName= "20 Minute HIIT Workout", Category = FitnessGoal.LoseWeight,  ExpertiseLevel = CurrentFitnessLevel.Intermediate },
            new WorkoutProgram {ExerciseName= "20 Minute HIIT Workout", Category = FitnessGoal.BuildAHealthyLifstyle,  ExpertiseLevel = CurrentFitnessLevel.Intermediate },
            new WorkoutProgram {ExerciseName= "30 Minute Cardio Workout", Category = FitnessGoal.LoseWeight,  ExpertiseLevel = CurrentFitnessLevel.Beginner },
            new WorkoutProgram {ExerciseName= "45 Minute HIIT Workout", Category = FitnessGoal.LoseWeight,  ExpertiseLevel = CurrentFitnessLevel.Intermediate },
            new WorkoutProgram {ExerciseName= "45 Minute HIIT Workout", Category = FitnessGoal.LoseWeight,  ExpertiseLevel = CurrentFitnessLevel.Expert },
            new WorkoutProgram {ExerciseName= "45 Minute HIIT Workout", Category = FitnessGoal.MaintainCurrentWeight,  ExpertiseLevel = CurrentFitnessLevel.Intermediate },
            new WorkoutProgram {ExerciseName= "45 Minute HIIT Workout", Category = FitnessGoal.MaintainCurrentWeight,  ExpertiseLevel = CurrentFitnessLevel.Expert },
            new WorkoutProgram {ExerciseName= "45 Minute HIIT Workout", Category = FitnessGoal.BuildAHealthyLifstyle,  ExpertiseLevel = CurrentFitnessLevel.Intermediate },
            new WorkoutProgram {ExerciseName= "45 Minute HIIT Workout", Category = FitnessGoal.BuildAHealthyLifstyle,  ExpertiseLevel = CurrentFitnessLevel.Expert },
            new WorkoutProgram {ExerciseName= "Beginner Strength Training Workout", Category = FitnessGoal.GainMuscleAndStrenght,  ExpertiseLevel = CurrentFitnessLevel.Beginner },
            new WorkoutProgram {ExerciseName= "Intermediate Fat Loss Workout", Category = FitnessGoal.LoseWeight,  ExpertiseLevel = CurrentFitnessLevel.Intermediate },
            new WorkoutProgram {ExerciseName= "Intermediate Fat Loss Workout", Category = FitnessGoal.LoseWeight,  ExpertiseLevel = CurrentFitnessLevel.Expert },
            new WorkoutProgram {ExerciseName= "Intermediate Fat Loss Workout", Category = FitnessGoal.BuildAHealthyLifstyle,  ExpertiseLevel = CurrentFitnessLevel.Intermediate },
            new WorkoutProgram {ExerciseName= "Intermediate Fat Loss Workout", Category = FitnessGoal.BuildAHealthyLifstyle,  ExpertiseLevel = CurrentFitnessLevel.Expert },
            new WorkoutProgram {ExerciseName= "Beginner At Home Workout", Category = FitnessGoal.LoseWeight,  ExpertiseLevel = CurrentFitnessLevel.Beginner },

        };
        builder.Entity<WorkoutProgram>().HasData(customWorkouts);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<VerificationOtp> VerificationOtps { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<WorkOutPlan> WorkoutPlans { get; set; }
}
