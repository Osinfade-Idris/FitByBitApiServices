using System.ComponentModel.DataAnnotations;

namespace FitByBitService.Entities.Responses.RoleResponse;

public class UpdateRoleDto
{
    [Required(ErrorMessage = "Role name is required.")]
    public string Name { get; set; } = null!;
}