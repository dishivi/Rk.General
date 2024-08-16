using General.Models.Http;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Rk.Domain.Request
{
    public class RequestGetTaskTimeLogs : PaginationRequest
    {
        public DateOnly? FromTimeLogDate { get; set; }

        public DateOnly? ToTimeLogDate { get; set; }

        public Guid? TenantId { get; set; }

        public Guid? TaskId { get; set; }

        public string? Status { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.