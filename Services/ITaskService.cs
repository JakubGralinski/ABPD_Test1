using ABPD_Test1.Models;

namespace ABPD_Test1.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDTO>>  GetAllTasks(int id);
    Task<int> DeleteTask(int IdTask, int IdProject);
}