using System.ComponentModel.DataAnnotations;
using FitByBitApiService.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace FitByBitApiService.Entities.Responses.RoleResponse;

public class AddUserToRoleDto
{
    [Required]
    public User UserId { get; set; } = null!;

    [Required]
    public IdentityRole RoleId { get; set; } = null!;
}