using FitByBitService.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace FitByBitService.Entities.Responses.RoleResponse;

public class RolesDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}