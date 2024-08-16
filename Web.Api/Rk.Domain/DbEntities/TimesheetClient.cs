using General.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Rk.Domain.DbEntities
{
    public class TimesheetClient : DbOperations
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }
    }
}
