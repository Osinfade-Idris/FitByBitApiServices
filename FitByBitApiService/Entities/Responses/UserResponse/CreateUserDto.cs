using System.ComponentModel.DataAnnotations;
namespace FitByBitService.Entities.Responses.UserResponse;

public class CreateUserDto
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email{ get; set; } = null!;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Fitness goal is required")]
    public int FitnessGoal { get; set; }

    [Required(ErrorMessage = "Current fitness goal is required")]
    public int CurrentFitness { get; set; }
    [Required]
    public double Height { get; set; }
    [Required]
    public uint Age { get; set; }
    [Required]
    public double Weight { get; set; }
}