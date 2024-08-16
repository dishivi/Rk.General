using General.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Rk.Domain.DbEntities
{
    public class TimesheetProject : DbOperations
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int? TotalHours { get; set; }

        public int? TotalCost { get; set; }

        [MaxLength(30)]
        public string? BillingType { get; set; }

        [ForeignKey(nameof(TimesheetClient))]
        public Guid? ClientId { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.