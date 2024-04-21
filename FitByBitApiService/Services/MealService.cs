using AutoMapper;
using FitByBitApiService.Data;
using FitByBitApiService.Entities.Models;
using FitByBitApiService.Entities.Responses.MealResponse;
using FitByBitApiService.Enum;
using FitByBitApiService.Exceptions;
using FitByBitApiService.Helpers;
using FitByBitApiService.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using IGenerateOtpHandler = FitByBitApiService.Handlers.IGenerateOtpHandler;


namespace FitByBitApiService.Services;

public class MealService : IMealRepository
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<MealService> _logger;
    private readonly IGenerateOtpHandler _generateOtpHandler;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly DateTime _dateTime;
    private readonly string _otpSecretKey;

    public MealService(IMapper mapper, UserManager<User> userManager, ILogger<MealService> logger, IMediator mediator,
        IGenerateOtpHandler generateOtpHandler, IConfiguration configuration, SignInManager<User> signInManager,
        ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<GenericResponse<MealPlan>> CreateMealPlan(IEnumerable<MealPlanDataDto> mealPlanDataList, string userId)
    {
        try
        {
            // Check for duplicate meal types within the provided meal plans
            var mealTypeCounts = new Dictionary<MealType, int>();
            foreach (var mealPlan in mealPlanDataList)
            {
                if (!mealTypeCounts.ContainsKey(mealPlan.MealType))
                {
                    mealTypeCounts[mealPlan.MealType] = 1;
                }
                else
                {
                    mealTypeCounts[mealPlan.MealType]++;
                }
            }

            foreach (var count in mealTypeCounts)
            {
                if (count.Value != 1)
                {
                    throw new Exception($"Duplicate meal type {count.Key} found.");
                }
            }

            foreach (var mealPlanData in mealPlanDataList)
            {
                ValidateMealIds(mealPlanData.MealIds, mealPlanData.MealType);
                CreateMealPlanForMeals(mealPlanData.MealIds, mealPlanData.MealType, mealPlanData.Date, userId);
            }

            return new GenericResponse<MealPlan>()
            {
                Success = true,
                Message = "Meal plan created successfully.",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            throw new FitByBitServiceUnavailableException($"{ex.Message}: service unavailable.");
        }
    }

    public async Task<GenericResponse<List<FoodGroupDto>>> GetAllFoodGroupsAsync()
    {
        /*var distinctFoodGroupIntegers = await _dbContext.Meals
            .Select(m => m.FoodGroup)
            .Distinct()
            .ToListAsync();

        // Map integers to enum values
        *//*var foodGroupEnumValues = distinctFoodGroupIntegers
            .Select(value => (FoodGroup)int.Parse(value))
            .ToList();*//*

        // Map integers to enum values using IntToFoodGroup method
        var foodGroupEnumValues = distinctFoodGroupIntegers
            .Select(value => IntToFoodGroup(int.Parse(value)))
            .ToList();

        // Map enum values to DTO objects
        var foodGroupDtos = foodGroupEnumValues
            .Select(enumValue => new FoodGroupDto
            {
                Id = int.TryParse(enumValue), // Assigning the integer value of the enum
                Name = enumValue.ToString() // Assigning the string representation of the enum
            })
            .ToList();

        return new GenericResponse<List<FoodGroupDto>>()
        {
            Success = true,
            Message = "Success",
            Data = foodGroupDtos,
            StatusCode = HttpStatusCode.OK
        };*/

        var distinctFoodGroupIntegers = await _dbContext.Meals
       .Select(m => m.FoodGroup)
       .Distinct()
       .ToListAsync();

        // Map integers to enum values using IntToFoodGroup method
        var foodGroupEnumValues = distinctFoodGroupIntegers
            .Select(value => IntToFoodGroup(int.Parse(value)))
            .ToList();

        // Map enum values to DTO objects
        var foodGroupDtos = foodGroupEnumValues
            .Select((enumValue, index) => new FoodGroupDto
            {
                Id = index + 1, // Assuming you want 1-based indexing
                Name = enumValue // Assigning the string representation of the enum
            })
            .ToList();

        return new GenericResponse<List<FoodGroupDto>>()
        {
            Success = true,
            Message = "Success",
            Data = foodGroupDtos,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<GenericResponse<List<MealDto>>> GetAllMealsByFoodGroupAsync(string id)
    {
        int foodGroupId = int.Parse(id);

        // Fetch meals from the database filtered by the provided food group ID
        var meals = await _dbContext.Meals
            .Where(m => m.FoodGroup == foodGroupId.ToString())
            .ToListAsync();

        // Convert meals to MealDto objects
        var mealDtos = meals.Select(meal => new MealDto
        {
            Id = meal.Id,
            Name = meal.Name,
            FoodGroup = meal.FoodGroup,
            Calories = meal.Calories,
            ImageUrl = meal.ImageUrl
        }).ToList();

        return new GenericResponse<List<MealDto>>
        {
            Success = true,
            Message = "Success",
            Data = mealDtos,
            StatusCode = HttpStatusCode.OK
        };

    }

    public async Task<GenericResponse<List<UserMealPlanViewModel>>> GetMealPlansGroupedByUserIdAndDate(string userId)
    {
        var mealPlans = _dbContext.MealPlans.Where(mp => mp.UserId == userId).ToList();

        // Group meal plans by userId
        var groupedMealPlans = mealPlans.GroupBy(mp => mp.UserId)
            .Select(g => new UserMealPlanViewModel
            {
                UserId = userId,
                MealPlans = g.OrderBy(mp => mp.Date)
                            .GroupBy(mp => mp.Date.Date)
                            .Select(gg => new UserMealPlanDateViewModel
                            {
                                Date = gg.Key,
                                Meals = gg.GroupBy(mp => mp.MealType)
                                         .ToDictionary(
                                            mg => mg.Key,
                                            mg => mg.Select(mp => GetMealName(mp.MealId)).ToList()
                                         )
                            }).ToList()
            }).ToList();

        return new GenericResponse<List<UserMealPlanViewModel>>
        {
            Success = true,
            Message = "Success",
            Data = groupedMealPlans,
            StatusCode = HttpStatusCode.OK
        };
    }

    private string GetMealName(Guid mealId)
    {
        // Fetch the meal from the database by its ID and return its name
        var meal = _dbContext.Meals.FirstOrDefault(m => m.Id == mealId);
        return meal?.Name ?? "Unknown";
    }


    // Define a helper method to convert integer to enum
    private string IntToFoodGroup(int value)
    {
        switch (value)
        {
            case 0: return "Unknown";
            case 1: return "Grains and Pasta";
            case 2: return "Meats";
            case 3: return "Breakfast Cereals";
            case 4: return "Fish";
            case 5: return "Fruits";
            default: return "Unknown"; // Handle unknown values
        }
    }

    private void ValidateMealIds(IEnumerable<Guid> mealIds, MealType mealType)
    {
        foreach (var mealId in mealIds)
        {
            // Fetch the meal details
            var meal = _dbContext.Meals.Where(m => m.Id == mealId);
            if (meal == null)
            {
                throw new Exception($"Meal with ID {mealId} not found for {mealType}");
            }
        }
    }

    private void CreateMealPlanForMeals(IEnumerable<Guid> mealIds, MealType mealType, DateTime date, string userId)
    {
        // Validate meal type
        if (mealType == MealType.Unknown || mealType != MealType.Breakfast && mealType != MealType.Lunch && mealType != MealType.Dinner)
        {
            throw new Exception("Invalid meal type.");
        }

        // Check if a meal plan already exists for the user on the specified date and meal type
        var existingMealPlan = _dbContext.MealPlans.FirstOrDefault(mp => mp.UserId == userId && mp.Date.Date == date.Date && mp.MealType == mealType);
        if (existingMealPlan != null)
        {
            throw new Exception($"A meal plan already exists for user {userId} on {date.ToShortDateString()} for meal type {mealType}.");
        }

        // Check if a meal plan with the same meal type already exists for the user on the specified date
        var mealPlansForDateAndType = _dbContext.MealPlans.Where(mp => mp.UserId == userId && mp.Date.Date == date.Date && mp.MealType == mealType);
        if (mealPlansForDateAndType.Any())
        {
            throw new Exception($"A meal plan already exists for user {userId} on {date.ToShortDateString()} for meal type {mealType}.");
        }

        foreach (var mealId in mealIds)
        {
            // Fetch the meal details
            var meal = _dbContext.Meals.FirstOrDefault(m => m.Id == mealId);
            if (meal == null)
            {
                throw new Exception($"Meal with ID {mealId} not found.");
            }

            // Save the meal plan
            var mealPlan = new MealPlan
            {
                MealId = mealId,
                MealType = mealType,
                Date = date,
                UserId = userId // You should replace this with the actual user ID
            };
            _dbContext.MealPlans.Add(mealPlan);
            _dbContext.SaveChanges();
        }
    }

}

