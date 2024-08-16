using General.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Rk.Domain.DbEntities
{
    public class TaskTimeLog : DbOperations
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public DateOnly TimeLogDate { get; set; }

        public int LoggedMinutes { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        [ForeignKey(nameof(ProjectTask))]
        public Guid TaskId { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.