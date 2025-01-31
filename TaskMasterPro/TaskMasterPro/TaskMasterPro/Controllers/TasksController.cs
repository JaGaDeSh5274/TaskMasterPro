using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMasterPro.Managers;
using TaskMasterPro.Models;

namespace TaskMasterPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskManager _taskManager;
        public TasksController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }
        [HttpGet]
        public async Task<ActionResult<TasksResponse>> GetAllTasks()
        {
            var tasks = await _taskManager.GetAllTasks();
            if (tasks == null || !tasks.Any())
            {
                return new JsonResult(new
                {
                    code = HttpStatusCode.NoContent,
                    data = " ",
                    messages = new string[] { "No tasks found." }
                });
            }
            else
            {
                var tableHeaders = typeof(Tasks).GetProperties()
                                        .Select(x => x.Name)
                                        .ToList();

                return new JsonResult(new
                {
                    code = HttpStatusCode.OK,
                    data = new
                    {
                        headers = tableHeaders,
                        rows = tasks,
                    },
                    messages = new string[] { "Task details fetched successfully." }
                });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksResponse>> GetTaskById(int id)
        {
            var task = await _taskManager.GetTaskById(id);
            if (task == null)
            {
                return new JsonResult(new
                {
                    code = HttpStatusCode.NoContent,
                    data = " ",
                    messages = new string[] { "Task not found." }
                });
            }
            else
            {
                var tableHeaders = typeof(Tasks).GetProperties()
                                        .Select(x => x.Name)
                                        .ToList();

                return new JsonResult(new
                {
                    code = HttpStatusCode.OK,
                    data = new
                    {
                        headers = tableHeaders,
                        rows = task
                    },
                    messages = new string[] { "Task details fetched successfully." }
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] TaskDTO taskDTO)
        {
            // Check if model validation passed
            if (taskDTO == null && !ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return new JsonResult(new
                {
                    code = HttpStatusCode.BadRequest,
                    data = " ",
                    messages = new string[] { "Invalid task details." }
                });
            }
            var newTaskId = await _taskManager.AddTaskAsync(taskDTO);
            // Add task creation logic here
            return new JsonResult(new
            {
                code = HttpStatusCode.Created,
                data = new
                {
                    TaskId = newTaskId
                },
                messages = new string[] { "Task created successfully." }
            });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaskAsync(int id, [FromBody] TaskDTO taskDTO)
        {
            // Check if model validation passed
            if (taskDTO == null && !ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return new JsonResult(new
                {
                    code = HttpStatusCode.BadRequest,
                    data = " ",
                    messages = new string[] { "Invalid task details." }
                });
            }
            try
            {
                // Call the repository method to execute the stored procedure and update the task
                var updatedTaskId = await _taskManager.UpdateTaskAsync(id, taskDTO);

                // If the task is updated successfully, return a 200 status
                return new JsonResult(new
                {
                    code = HttpStatusCode.OK,
                    data = new { taskId = updatedTaskId },
                    messages = new string[] { "Task updated successfully." }
                });

            }
            catch (Exception ex)
            {
                // Return a 500 internal server error if something goes wrong
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            // Add task update logic here

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskManager.DeleteTask(id);
                return new JsonResult(new
                {
                    code = HttpStatusCode.OK,
                    data = new { taskId = id },
                    messages = new string[] { "Task deleted successfully." }
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    code = HttpStatusCode.InternalServerError,
                    data = "",
                    messages = new string[] { "Please contact the technical team" }
                });
            }
        }
        [HttpGet("dropdowns")]
        public async Task<ActionResult> GetPriorityStatusDropdowns()
        {
            var data = await _taskManager.GetPriorityStatusDropdowns();
            if (data != null)
            {
                return new JsonResult(new
                {
                    code = HttpStatusCode.OK,
                    data = new
                    {
                        priorities = data.Priorities,
                        statuses = data.Statuses
                    },
                    messages = new string[] { "Priority and status dropdowns fetched successfully." }
                });
            }
            else
            {
                return new JsonResult(new
                {
                    code = HttpStatusCode.NoContent,
                    data = "",
                    messages = new string[] { "No data found." }
                });
            }
        }
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateTaskStatusAsync(int id, string status)
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    return new JsonResult(new
                    {
                        code = HttpStatusCode.BadRequest,
                        data = string.Empty,
                        messages = new string[] { "Status cannot be empty." }
                    });
                }
                var updatedTaskId = await _taskManager.UpdateTaskStatusAsync(id, status);
                if (updatedTaskId == null || updatedTaskId.Id == 0)
                {
                    return new JsonResult(new
                    {
                        code = HttpStatusCode.NotFound,
                        data = string.Empty,
                        messages = new string[] { "Task not found." }
                    });
                }
                return new JsonResult(new
                {
                    code = HttpStatusCode.OK,
                    data = new { taskId = updatedTaskId },
                    messages = new string[] { "Task status updated successfully." }
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    code = HttpStatusCode.InternalServerError,
                    data = "",
                    messages = new string[] { "Please contact the technical team" }
                });
            }
        }

    }
}
