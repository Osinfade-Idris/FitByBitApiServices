using FitByBitService.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitByBitService.Entities.Responses.UserResponse;

public class UpdateUserDto
{
    public FitnessGoal FitnessGoal { get; set; }
    public double TargetWeight { get; set; }
    public double StartingWeight { get; set; }
    public CurrentFitnessLevel CurrentFitness { get; set; }
}