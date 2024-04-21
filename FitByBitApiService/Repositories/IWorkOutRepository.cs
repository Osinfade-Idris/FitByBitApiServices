using FitByBitApiService.Entities.Models;
using FitByBitApiService.Entities.Responses.WorkOutResponse;
using FitByBitApiService.Helpers;

namespace FitByBitApiService.Repositories;

public interface IWorkOutRepository
{
    Task<GenericResponse<UserWorkOutDto>> GetUserWorkOutByIdAsync(string id);
    //Task<GenericResponse<IEnumerable<AllWorkoutDto>>> GetAllWorkoutsAsync(WorkoutSearchParameters searchParameters = null);
    Task<GenericResponse<IEnumerable<WorkoutListDto>>> GetAllWorkoutsAsync(WorkoutSearchParameters searchParameters = null);
    Task<GenericResponse<AllWorkoutDto>> GetWorkoutByIdAsync(Guid id);
    Task<GenericResponse<WorkOutPlan>> CreateWorkOutPlan(DateTime date, Guid userId, IEnumerable<Guid> workoutIds);

    Task<GenericResponse<AllExerciseDto>> GetWorkoutExercisesByName(string name);
}
