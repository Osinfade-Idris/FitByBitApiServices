using AutoMapper;
using FitByBitApiService.Data;
using FitByBitApiService.Entities.Models;
using FitByBitApiService.Entities.Responses.UserResponse;
using FitByBitApiService.Entities.Responses.WorkOutResponse;
using FitByBitApiService.Enum;
using FitByBitApiService.Exceptions;
using FitByBitApiService.Helpers;
using FitByBitApiService.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;
using IGenerateOtpHandler = FitByBitApiService.Handlers.IGenerateOtpHandler;

namespace FitByBitApiService.Services;

public class WorkOutService : IWorkOutRepository
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AuthService> _logger;
    private readonly IGenerateOtpHandler _generateOtpHandler;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly DateTime _dateTime;
    private readonly string _otpSecretKey;

    public WorkOutService(IMapper mapper, UserManager<User> userManager, ILogger<AuthService> logger, IMediator mediator,
        IGenerateOtpHandler generateOtpHandler, IConfiguration configuration, SignInManager<User> signInManager,
        ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
        _generateOtpHandler = generateOtpHandler;
        _mediator = mediator;
        _configuration = configuration;
        _otpSecretKey = _configuration["OtpEncryptionSettings:SecretKey"]!;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _dateTime = DateTime.Now;
    }

    public async Task<GenericResponse<UserWorkOutDto>> GetUserWorkOutByIdAsync(string id)
    {
        try
        {
            var userExist = await _userManager.FindByIdAsync(id);
            if (userExist != null)
            {
                var userObject = _mapper.Map<UserDto>(userExist);

                // Get the fitness goal and fitness level of the user (assuming they are stored in the user object)
                FitnessGoal fitnessGoal = userObject.FitnessGoal;
                CurrentFitnessLevel fitnessLevel = userObject.CurrentFitness;

                // Get the workouts available for the user's fitness goal and fitness level
                var workouts = GetWorkoutsForUser(fitnessGoal, fitnessLevel);

                // Extract exercise names from workouts
                var workOutNames = workouts.Select(w => w.WorkoutName).ToList();

                /*// Get exercises for the extracted exercise names
                var exercises = GetExercisesForWorkouts(exerciseNames);

                // Group exercises by their workout names and order them by number within each group
                var groupedExercises = exercises
                    .GroupBy(e => e.Name) // Group by the "name" property
                    .ToDictionary(
                        group => group.Key,
                        group => group.OrderBy(e => int.Parse(e.Number)).ToList()
                    );
*/
                // Map the workouts to WorkoutDto objects
                var workoutDtos = workouts.Select(w => new WorkoutDto
                {
                    //WorkoutId = w.Id,
                    WorkoutName = w.WorkoutName,
                    Category = w.Category,
                    ExpertiseLevel = w.ExpertiseLevel
                }).ToArray();

                // Create UserWorkOutDto object containing user information, available workouts, and grouped exercises
                var userWorkOutDto = new UserWorkOutDto
                {
                    UserId = userObject.Id,
                    FirstName = userObject.FirstName,
                    LastName = userObject.LastName,
                    Email = userObject.Email,
                    PhoneNumber = userObject.PhoneNumber,
                    Dob = userObject.Dob,
                    IsActive = userObject.IsActive,
                    Workouts = workoutDtos,
                    //GroupedExercises = groupedExercises // Assign grouped exercises to UserWorkOutDto
                };

                return new GenericResponse<UserWorkOutDto>()
                {
                    Success = true,
                    Message = "Success",
                    Data = userWorkOutDto,
                    StatusCode = HttpStatusCode.OK
                };
            }
            _logger.LogInformation($"\n----- User with Id {id} does not exist | {_dateTime} ------\n".ToUpper());
            throw new FitByBitNotFoundException($"User with Id {id} does not exist", HttpStatusCode.NotFound.ToString());
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<IEnumerable<WorkoutListDto>>> GetAllWorkoutsAsync(WorkoutSearchParameters searchParameters = null)
    {
        try
        {
            // Fetch all workouts from the database
            var workouts = await _dbContext.WorkoutPrograms.ToListAsync();

            // If search parameters are provided, filter the workouts
            if (searchParameters != null && (searchParameters.FitnessLevel != null || searchParameters.Category != null))
            {
                workouts = workouts.Where(w =>
                    (searchParameters.FitnessLevel == null || w.ExpertiseLevel == searchParameters.FitnessLevel) &&
                    (searchParameters.Category == null || w.Category == searchParameters.Category)
                ).ToList();
            }

            // Extract distinct names from workouts
            var workoutNames = workouts.Select(w => w.WorkoutName).Distinct().ToList();

            // Create a list to store AllWorkoutDto objects
            var allWorkoutDtos = workoutNames.Select(workoutName => new WorkoutListDto
            {
                WorkoutName = workoutName,
                WorkoutId = workouts.Where(w => w.WorkoutName == workoutName).Select(w => w.Id).FirstOrDefault(),
                /*              Category = workouts.Where(w => w.WorkoutName == workoutName).Select(w => w.Category).Distinct().FirstOrDefault(),
                                ExpertiseLevel = workouts.Where(w => w.WorkoutName == workoutName).Select(w => w.ExpertiseLevel).Distinct().FirstOrDefault()*/
            }).ToList();

            return new GenericResponse<IEnumerable<WorkoutListDto>>()
            {
                Success = true,
                Message = "Success",
                Data = allWorkoutDtos,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<AllWorkoutDto>> GetWorkoutByIdAsync(Guid id)
    {
        try
        {
            // Fetch the workout from the database by ID
            var workout = await _dbContext.WorkoutPrograms.FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
            {
                throw new FitByBitNotFoundException($"Workout with ID {id} not found", HttpStatusCode.NotFound.ToString());
            }

            // Fetch exercises for the workout directly from the database
            var exercises = await _dbContext.Exercises
                .Where(e => workout.WorkoutName.Contains(e.Name))
                .ToListAsync();

            // Group exercises by their workout names and order them by number within each group
            var groupedExercises = exercises
                .GroupBy(e => e.Name)
                .ToDictionary(
                    group => group.Key,
                    group => group.OrderBy(e => int.Parse(e.Number)).ToList()
                );

            // Create an AllWorkoutDto object for the current workout
            var allWorkoutDto = new AllWorkoutDto
            {
                // ExerciseName = workout.WorkoutName,
                Category = workout.Category,
                ExpertiseLevel = workout.ExpertiseLevel,
                //Exercises = groupedExercises
            };

            return new GenericResponse<AllWorkoutDto>()
            {
                Success = true,
                Message = "Success",
                Data = allWorkoutDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<AllExerciseDto>> GetWorkoutExercisesByName(string name)
    {
        try
        {
            // Fetch the workout from the database by name
            var workout = await _dbContext.WorkoutPrograms.FirstOrDefaultAsync(w => w.WorkoutName == name);

            if (workout == null)
            {
                throw new FitByBitNotFoundException($"Workout with name '{name}' not found", HttpStatusCode.NotFound.ToString());
            }

            // Fetch exercises for the workout
            var exercises = await _dbContext.Exercises
                .Where(e => e.Name == name)
                .ToListAsync();

            // Order the exercises by number
            var orderedExercises = exercises.OrderBy(e => int.Parse(e.Number)).ToList();

            // Group exercises by their workout names
            var groupedExercises = orderedExercises
                .GroupBy(e => e.Name)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToList()
                );

            // Create an AllWorkoutDto object for the current workout
            var allExerciseDto = new AllExerciseDto
            {
                WorkoutName = workout.WorkoutName,
                Exercises = groupedExercises
            };

            return new GenericResponse<AllExerciseDto>()
            {
                Success = true,
                Message = "Success",
                Data = allExerciseDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<CreateWorkoutListDto>> CreateWorkOutPlan(DateTime date, Guid userId, Guid workoutId)
    {
        try
        {
            // Check if the provided Workout Program ID exists
            var workouts = await _dbContext.WorkoutPrograms
                .Where(wp => wp.Id == workoutId) // Changed to check for a single ID
                .ToListAsync();

            if (workouts.Count != 1)
            {
                throw new Exception("Invalid Workout Program ID provided.");
            }

            // Check if a workout plan already exists for the user on the specified date and workout program ID
            var existingWorkoutPlans = _dbContext.WorkoutPlans
                .Where(wp => wp.UserId == userId && wp.Date.Date == date.Date && wp.WorkoutId == workoutId)
                .ToList();

            if (existingWorkoutPlans.Any())
            {
                throw new Exception($"A workout plan already exists for user {userId} on {date.ToShortDateString()} for the provided workout program ID.");
            }

            // Save the workout plan
            var workoutPlan = new WorkOutPlan
            {
                WorkoutId = workoutId,
                Date = date,
                UserId = userId
            };
            _dbContext.WorkoutPlans.Add(workoutPlan);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return new GenericResponse<CreateWorkoutListDto>()
            {
                Success = true,
                Message = "Workout plan created successfully.",
                StatusCode = HttpStatusCode.OK,
                Data = new CreateWorkoutListDto
                {
                    Id = workoutId,
                    // Assuming 'Name' property exists in WorkoutPlan
                    Name = workouts.FirstOrDefault(wp => wp.Id == workoutId)?.WorkoutName,
                    Date = date,
                    Status = false
                }
            };
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {DateTime.Now} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.", HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<IEnumerable<GetWorkoutPlansByDateDto>>> GetWorkoutPlansByDate(DateTime date, Guid userId)
    {
        try
        {
            // Fetch workout plans for the user and specified date
            var workoutPlans = await _dbContext.WorkoutPlans
                .Where(wp => wp.UserId == userId && wp.Date.Date == date.Date)
                .ToListAsync();


            // Convert workout plans to response DTOs
            var workoutsPlans = workoutPlans.Select(wp => new GetWorkoutPlansByDateDto
            {
                Id = wp.Id,
                WorkoutId = wp.WorkoutId,
                Date = wp.Date,
                WorkoutName = _dbContext.WorkoutPrograms.FirstOrDefault(p => p.Id == wp.WorkoutId)?.WorkoutName,
                Status = wp.Status
            });

            // Check for empty response and return appropriate message if needed
            if (!workoutsPlans.Any())
            {
                throw new FitByBitNotFoundException($"No workout plans found for the specified date: {date}");
            }
            return new GenericResponse<IEnumerable<GetWorkoutPlansByDateDto>>()
            {
                Success = true,
                Message = "Success",
                Data = (IEnumerable<GetWorkoutPlansByDateDto>)workoutsPlans.ToList(),
                StatusCode = HttpStatusCode.OK
            };

        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {DateTime.Now} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.", HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<GetWorkoutPlansByDateDto>> UpdateDailyWorkOut(Guid userId, Guid workoutPlanId)
    {
        try
        {

            // Fetch the workout plan by ID
            var workoutPlan = await _dbContext.WorkoutPlans.FirstOrDefaultAsync(wp => wp.Id == workoutPlanId);

            // Check if workout plan exists
            if (workoutPlan == null)
            {
                throw new Exception($"Workout plan with ID: {workoutPlanId} not found.");
            }

            // Save the workout plan
            workoutPlan.Status = true;

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return new GenericResponse<GetWorkoutPlansByDateDto>()
            {
                Success = true,
                Message = "Workout plan updated successfully.",
                StatusCode = HttpStatusCode.OK,
                Data = new GetWorkoutPlansByDateDto
                {
                    Id = workoutPlan.Id,
                    WorkoutId = workoutPlan.WorkoutId,
                    Date = workoutPlan.Date,
                    WorkoutName = _dbContext.WorkoutPrograms.FirstOrDefault(p => p.Id == workoutPlan.WorkoutId)?.WorkoutName,
                    Status = workoutPlan.Status
                }
            };
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {DateTime.Now} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.", HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<IEnumerable<AllWorkoustDto>>> GetAllWorkouts()
    {
        try
        {
            // Fetch all workouts from the database
            var workouts = await _dbContext.WorkoutPrograms.ToListAsync();


            // Extract distinct names from workouts
            var workoutNames = workouts.Select(w => w.WorkoutName).ToList();

            // Create a list to store AllWorkoutDto objects
            var allWorkoutDtos = workoutNames.Select(workoutName => new AllWorkoustDto
            {
                WorkoutId = workouts.Where(w => w.WorkoutName == workoutName).Select(w => w.Id).Distinct().FirstOrDefault(),
                WorkoutName = workoutName,
                Category = workouts.Where(w => w.WorkoutName == workoutName).Select(w => w.Category).Distinct().FirstOrDefault(),
                ExpertiseLevel = workouts.Where(w => w.WorkoutName == workoutName).Select(w => w.ExpertiseLevel).Distinct().FirstOrDefault()
            }).ToList();

            return new GenericResponse<IEnumerable<AllWorkoustDto>>()
            {
                Success = true,
                Message = "Success",
                Data = allWorkoutDtos,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }


    private List<Exercise> GetExercisesForWorkouts(List<string> exerciseNames)
    {
        try
        {
            // Query exercises based on exercise names
            var exercises = _dbContext.Exercises
                .Where(e => exerciseNames.Contains(e.Name))
                .ToList();

            return exercises;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private List<WorkoutProgram> GetWorkoutsForUser(FitnessGoal fitnessGoal, CurrentFitnessLevel fitnessLevel)
    {
        try
        {
            // Query workouts based on fitness goal and fitness level
            var workouts = _dbContext.WorkoutPrograms
                .Where(w => w.Category == fitnessGoal && w.ExpertiseLevel == fitnessLevel)
                .ToList();

            return workouts;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

