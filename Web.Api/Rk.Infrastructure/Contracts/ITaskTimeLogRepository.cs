using Rk.Domain.DbEntities;
using Rk.Domain.Request;

namespace Rk.Infrastructure.Contracts
{
    public interface ITaskTimeLogRepository
    {
        Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag);
        Task<int> AddAsync(TaskTimeLog request);
        Task<int> DeleteAsync(Guid id, Guid tenantId);
        Task<TaskTimeLog?> FindAsync(Guid id, Guid tenantId);
        Task<List<TaskTimeLog>> GetAsync(RequestGetTaskTimeLogs request);
        Task<int> UpdateAsync(TaskTimeLog request);
    }
}