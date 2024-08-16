using General.Models.Database;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Rk.Domain.DbEntities
{
    public class TimesheetUser : DbOperations
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string? Password { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }

        public string? Note { get; set; }

        public double? LaborRatePerHour { get; set; }

        public double? BillableRatePerHour { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
