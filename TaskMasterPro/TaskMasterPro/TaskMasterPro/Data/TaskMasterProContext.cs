using Microsoft.EntityFrameworkCore;
using TaskMasterPro.Models;

namespace TaskMasterPro.Data
{
    public class TaskMasterProContext : DbContext
    {
        public TaskMasterProContext(DbContextOptions<TaskMasterProContext> options)
            : base(options)
        {
        }
        public DbSet<TaskMasterPro.Models.Tasks> Tasks { get; set; }
        public DbSet<TaskIdResult> TaskIdResult { get; set; }
        public DbSet<StatusResult> Statuses { get; set; }
        public DbSet<PriorityResult> Priorities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration if needed
        }
    }
}
