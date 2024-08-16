using General.Models.Http;

namespace Rk.Domain.Request
{
    public class RequestGetTimesheetUsers : PaginationRequest
    {
        public string Name { get; set; }

        public Guid? TenantId { get; set; }

        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
    }
}
