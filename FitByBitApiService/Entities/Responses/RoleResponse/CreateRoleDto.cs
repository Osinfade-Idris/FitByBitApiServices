using System.ComponentModel.DataAnnotations;

namespace FitByBitApiService.Entities.Responses.RoleResponse;

public class CreateRoleDto
{
    [Required(ErrorMessage = "Role name is required.")]
    public string Name { get; set; } = null!;
}