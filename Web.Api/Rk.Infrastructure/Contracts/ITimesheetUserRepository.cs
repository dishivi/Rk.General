using Rk.Domain.DbEntities;
using Rk.Domain.Request;

namespace Rk.Infrastructure.Contracts
{
    public interface ITimesheetUserRepository
    {
        Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag);
        Task<int> AddAsync(TimesheetUser request);
        Task<int> DeleteAsync(Guid id, Guid tenantId);
        Task<TimesheetUser?> FindAsync(Guid id, Guid tenantId);
        Task<List<TimesheetUser>> GetAsync(RequestGetTimesheetUsers request);
        Task<int> UpdateAsync(TimesheetUser request);
    }
}