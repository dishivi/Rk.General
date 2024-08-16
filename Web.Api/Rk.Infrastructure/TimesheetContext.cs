using Microsoft.EntityFrameworkCore;
using Rk.Domain.DbEntities;

namespace Rk.Infrastructure
{
    public class TimesheetContext : DbContext
    {
        public TimesheetContext(DbContextOptions<TimesheetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TimesheetClient> Clients { get; set; }
        public DbSet<TimesheetProject> Projects { get; set; }
        public DbSet<TimesheetUser> Users { get; set; }
        public DbSet<ProjectAssignedUser> ProjectAssignedUsers { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<TaskTimeLog> TimeLogs { get; set; }
    }
}
