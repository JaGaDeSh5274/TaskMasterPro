using Microsoft.EntityFrameworkCore;
using TaskMasterPro.Data;
using TaskMasterPro.Models;

namespace TaskMasterPro.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskMasterProContext _context;

        public TaskRepository(TaskMasterProContext context)
        {
            _context = context;
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            return await _context.ExecuteStoredProcedure<Tasks>("Sp_GetAllTaskDetails");
        }

        public async Task<Tasks> GetTaskById(int id)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>() {
                {"@id",id}
            };
            var result = await _context.ExecuteStoredProcedure<Tasks>("Sp_GetTaskDetailsById", parameters);
            return result?.FirstOrDefault();
            ;
        }

        public async Task<int> AddTaskAsync(TaskDTO taskDTO)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>() {
                {"@Title", taskDTO.Title},
                {"@Description", taskDTO.Description},
                {"@DueDate", taskDTO.DueDate},
                {"@Priority", taskDTO.Priority},
                {"@Status", taskDTO.Status},
            };
            var result = await _context.ExecuteStoredProcedure<TaskIdResult>("Sp_CreateTask", parameters);
            return result?.FirstOrDefault()?.Id ?? 0;
        }

        public async Task<TaskIdResult> UpdateTaskAsync(int id, TaskDTO? taskDTO)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>() {
                {"@Id", id},
                {"@Title", taskDTO.Title},
                {"@Description", taskDTO.Description},
                {"@DueDate", taskDTO.DueDate},
                {"@Priority", taskDTO.Priority},
                {"@Status", taskDTO.Status},
            };
            var result = await _context.ExecuteStoredProcedure<TaskIdResult>("Sp_UpdateTask", parameters);
            return result?.FirstOrDefault() ?? new TaskIdResult { Id = 0 };
        }

        public async Task<bool> DeleteTask(int id)
        {
            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>()
        {
            {"@id", id}
        };
                var result = await _context.ExecuteStoredProcedure<TaskIdResult>("Sp_DeleteTask", parameters);
                // Check if any rows were affected
                return result?.Any() == true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<DropdownData> GetPriorityStatusDropdowns()
        {
            try
            {
                var (statusResults, priorityResults) = await _context.ExecuteStoredProcedureWithMultipleResults<StatusResult, PriorityResult>("Sp_GetDropdownData");
                return new DropdownData
                {
                    Statuses = statusResults.Select(s => new StatusResult { Id = s.Id, Status = s.Status }).ToList(),
                    Priorities = priorityResults.Select(p => new PriorityResult { Id = p.Id, Priority = p.Priority }).ToList()
                };
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPriorityStatusDropdowns: {ex.Message}");
                return new DropdownData
                {
                    Statuses = new List<StatusResult>(),
                    Priorities = new List<PriorityResult>()
                };
            }
        }

        public async Task<TaskIdResult> UpdateTaskStatusAsync(int id, string status)
        {
        Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>() {
                {"@Id", id},
                {"@Status", status},
            };
             var result = await _context.ExecuteStoredProcedure<TaskIdResult>("Sp_UpdateTaskStatus", parameters);
            return result?.FirstOrDefault() ?? new TaskIdResult { Id = 0 };
        }
    }
}
