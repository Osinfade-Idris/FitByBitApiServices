using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FitByBitApiService.Entities.Models;
using FitByBitService.Data;
using FitByBitService.Entities.Models;
using FitByBitService.Entities.Responses;
using FitByBitService.Entities.Responses.UserResponse;
using FitByBitService.Entities.Responses.WorkOutResponse;
using FitByBitService.Enum;
using FitByBitService.Events;
using FitByBitService.Exceptions;
using FitByBitService.Helpers;
using FitByBitService.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CommonEncryptionHandler = FitByBitService.Handlers.CommonEncryptionHandler;
using IGenerateOtpHandler = FitByBitService.Handlers.IGenerateOtpHandler;


namespace FitByBitService.Services;

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
                var workouts = this.GetWorkoutsForUser(fitnessGoal, fitnessLevel);

                // Extract exercise names from workouts
                var exerciseNames = workouts.Select(w => w.ExerciseName).ToList();

                // Get exercises for the extracted exercise names
                var exercises = GetExercisesForWorkouts(exerciseNames);

                // Group exercises by their workout names and order them by number within each group
                var groupedExercises = exercises
                    .GroupBy(e => e.Name) // Group by the "name" property
                    .ToDictionary(
                        group => group.Key,
                        group => group.OrderBy(e => int.Parse(e.Number)).ToList()
                    );

                // Map the workouts to WorkoutDto objects
                var workoutDtos = workouts.Select(w => new WorkoutDto
                {
                    ExerciseName = w.ExerciseName,
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
                    GroupedExercises = groupedExercises // Assign grouped exercises to UserWorkOutDto
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

