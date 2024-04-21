using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FitByBitApiService.Entities.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Fitness goal is required")]
    public int FitnessGoal { get; set; }

    [Required(ErrorMessage = "Current fitness goal is required")]
    public int CurrentFitness { get; set; }

    public double Height { get; set; }

    public double StartingWeight { get; set; }
    public double TargetWeight { get; set; }

    public double Bmi { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }

    [DefaultValue(false)]
    public bool IsDeleted { get; set; }

    public DateTime LastLogin { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    public int Age { get; internal set; }
}