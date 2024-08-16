using Rk.Domain.DbEntities;
using Rk.Domain.Request;

namespace Rk.Infrastructure.Contracts
{
    public interface ITimesheetProjectRepository
    {
        Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag);
        Task<int> AddAsync(TimesheetProject request);
        Task<int> DeleteAsync(Guid id, Guid tenantId);
        Task<TimesheetProject?> FindAsync(Guid id, Guid tenantId);
        Task<List<TimesheetProject>> GetAsync(RequestGetTimesheetProjects request);
        Task<int> UpdateAsync(TimesheetProject request);
    }
}