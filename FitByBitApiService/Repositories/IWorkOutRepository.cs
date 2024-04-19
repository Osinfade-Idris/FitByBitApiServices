using FitByBitService.Entities.Responses.WorkOutResponse;
using FitByBitService.Helpers;

namespace FitByBitService.Repositories;

public interface IWorkOutRepository
{
    Task<GenericResponse<UserWorkOutDto>> GetUserWorkOutByIdAsync(string id);
    Task<GenericResponse<IEnumerable<AllWorkoutDto>>> GetAllWorkoutsAsync(WorkoutSearchParameters searchParameters = null);
    Task<GenericResponse<AllWorkoutDto>> GetWorkoutByIdAsync(Guid id);
}
