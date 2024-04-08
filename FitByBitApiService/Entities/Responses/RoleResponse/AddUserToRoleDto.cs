using System.ComponentModel.DataAnnotations;
using FitByBitService.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace FitByBitService.Entities.Responses.RoleResponse;

public class AddUserToRoleDto
{
    [Required]
    public User UserId { get; set; } = null!;
    
    [Required]
    public IdentityRole RoleId { get; set; } = null!;
}