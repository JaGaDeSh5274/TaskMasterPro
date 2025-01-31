using System.ComponentModel.DataAnnotations;

namespace TaskMasterPro.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }

    }
    public class TasksResponse
    {
        public List<string> Headers { get; set; }
        public List<Tasks> Data { get; set; }
    }
    public class TaskIdResult
    {   
        public int Id { get; set; }
    }
    public class TaskDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
