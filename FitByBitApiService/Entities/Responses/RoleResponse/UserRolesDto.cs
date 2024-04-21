using FitByBitApiService.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace FitByBitApiService.Entities.Responses.RoleResponse;

public class UserRolesDto
{
    public User UserId { get; set; } = null!;
    public IdentityRole RoleId { get; set; } = null!;
}