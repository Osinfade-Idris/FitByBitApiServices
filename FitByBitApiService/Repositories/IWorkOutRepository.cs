using FitByBitApiService.Entities.Models;
using FitByBitApiService.Entities.Responses.WorkOutResponse;
using FitByBitApiService.Helpers;
using FitByBitApiService.Services;

namespace FitByBitApiService.Repositories;

public interface IWorkOutRepository
{
    Task<GenericResponse<UserWorkOutDto>> GetUserWorkOutByIdAsync(string id);
    //Task<GenericResponse<IEnumerable<AllWorkoutDto>>> GetAllWorkoutsAsync(WorkoutSearchParameters searchParameters = null);
    Task<GenericResponse<IEnumerable<WorkoutListDto>>> GetAllWorkoutsAsync(WorkoutSearchParameters searchParameters = null);
    Task<GenericResponse<AllWorkoutDto>> GetWorkoutByIdAsync(Guid id);
    Task<GenericResponse<CreateWorkoutListDto>> CreateWorkOutPlan(DateTime date, Guid userId, Guid workoutId);

    Task<GenericResponse<IEnumerable<GetWorkoutPlansByDateDto>>> GetWorkoutPlansByDate(DateTime date, Guid userId);

    Task<GenericResponse<AllExerciseDto>> GetWorkoutExercisesByName(string name);

    Task<GenericResponse<GetWorkoutPlansByDateDto>> UpdateDailyWorkOut(Guid userId, Guid workoutPlanId);

    Task<GenericResponse<IEnumerable<AllWorkoustDto>>> GetAllWorkouts();
}
