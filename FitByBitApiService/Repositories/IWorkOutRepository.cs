using FitByBitService.Entities.Responses.WorkOutResponse;
using FitByBitService.Helpers;

namespace FitByBitService.Repositories;

public interface IWorkOutRepository
{
    Task<GenericResponse<UserWorkOutDto>> GetUserWorkOutByIdAsync(string id);
}
