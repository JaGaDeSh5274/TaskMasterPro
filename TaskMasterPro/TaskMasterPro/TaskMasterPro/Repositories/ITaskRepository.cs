using TaskMasterPro.Models;

namespace TaskMasterPro.Repositories
{
    public interface ITaskRepository
    {
 
        Task<List<Tasks>> GetAllTasks();
        Task<Tasks> GetTaskById(int id);
       Task<int> AddTaskAsync(TaskDTO taskDTO);
        Task<TaskIdResult> UpdateTaskAsync(int id, TaskDTO? taskDTO);
        Task<bool> DeleteTask(int id);
        Task<DropdownData> GetPriorityStatusDropdowns();
        Task<TaskIdResult> UpdateTaskStatusAsync(int id, string status);
    }
}
