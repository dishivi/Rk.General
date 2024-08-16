using General.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Rk.Domain.DbEntities
{
    public class ProjectAssignedUser : DbOperations
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public double? LaborRate { get; set; }
        public double? BillableRate { get; set; }

        [ForeignKey(nameof(TimesheetProject))]
        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(TimesheetUser))]
        public Guid UserId { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.