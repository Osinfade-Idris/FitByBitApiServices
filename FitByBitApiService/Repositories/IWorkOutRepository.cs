using FitByBitApiService.Entities.Models;
using FitByBitService.Entities.Responses.WorkOutResponse;
using FitByBitService.Helpers;

namespace FitByBitService.Repositories;

public interface IWorkOutRepository
{
    Task<GenericResponse<UserWorkOutDto>> GetUserWorkOutByIdAsync(string id);
    Task<GenericResponse<IEnumerable<AllWorkoutDto>>> GetAllWorkoutsAsync(WorkoutSearchParameters searchParameters = null);
    Task<GenericResponse<AllWorkoutDto>> GetWorkoutByIdAsync(Guid id);
    Task<GenericResponse<WorkOutPlan>> CreateWorkOutPlan(DateTime date, Guid userId, IEnumerable<Guid> workoutIds);
}
