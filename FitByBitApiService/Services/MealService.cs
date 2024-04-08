using System.Net;
using AutoMapper;
using FitByBitService.Data;
using FitByBitService.Entities.Models;
using FitByBitService.Entities.Responses.MealResponse;
using FitByBitService.Enum;
using FitByBitService.Helpers;
using FitByBitService.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IGenerateOtpHandler = FitByBitService.Handlers.IGenerateOtpHandler;


namespace FitByBitService.Services;

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
}

