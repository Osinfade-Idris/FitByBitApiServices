using FitByBitService.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace FitByBitService.Entities.Responses.RoleResponse;

public class UserRolesDto
{
    public User UserId { get; set; } = null!;
    public IdentityRole RoleId { get; set; } = null!;
}