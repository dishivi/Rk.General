using Rk.Domain.DbEntities;
using Rk.Domain.Request;

namespace Rk.Infrastructure.Contracts
{
    public interface ITimesheetClientRepository
    {
        Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag);
        Task<int> AddAsync(TimesheetClient request);
        Task<int> DeleteAsync(Guid id, Guid tenantId);
        Task<TimesheetClient?> FindAsync(Guid id, Guid tenantId);
        Task<List<TimesheetClient>> GetAsync(RequestGetTimesheetClients request);
        Task<int> UpdateAsync(TimesheetClient request);
    }
}