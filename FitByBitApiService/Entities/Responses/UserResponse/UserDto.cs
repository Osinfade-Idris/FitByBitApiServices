using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FitByBitService.Common;
using FitByBitService.Enum;

namespace FitByBitService.Entities.Responses.UserResponse;

public class UserDto: BaseEntity
{
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    [DataType(DataType.EmailAddress)] 
    public string Email { get; set; } = null!;
    
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;
    
    [DataType(DataType.Date)] 
    public DateTime Dob { get; set; }
    
    public bool IsActive { get; set; }
    // Use the defined enums
    public FitnessGoal FitnessGoal { get; set; }
    public CurrentFitnessLevel CurrentFitness { get; set; }

    public double StartingWeight { get; set; }
    public double TargetWeight { get; set; }
}