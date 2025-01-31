using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaskMasterPro.Models;
using TaskMasterPro.Repositories;

namespace TaskMasterPro.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            
          return await _taskRepository.GetAllTasks();

        }

        public async Task<Tasks> GetTaskById(int id)
        {
           return await _taskRepository.GetTaskById(id);
        }
        
        public async Task<int> AddTaskAsync(TaskDTO taskDTO)
        {
            
           return await _taskRepository.AddTaskAsync(taskDTO);
        }

        public async Task<TaskIdResult> UpdateTaskAsync(int id, TaskDTO? taskDTO)
        {
             return await _taskRepository.UpdateTaskAsync(id , taskDTO);
        }

        public async Task<bool> DeleteTask(int id)
        {
            return await _taskRepository.DeleteTask(id);
        }

         public async Task<DropdownData> GetPriorityStatusDropdowns()
        {
             return await _taskRepository.GetPriorityStatusDropdowns();
        }

        public async Task<TaskIdResult> UpdateTaskStatusAsync(int id, string status)
        {
            return await _taskRepository.UpdateTaskStatusAsync(id, status);
        }
    }
}
