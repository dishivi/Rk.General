using Rk.Domain.DbEntities;
using Rk.Domain.Request;

namespace Rk.Infrastructure.Contracts
{
    public interface IProjectTaskRepository
    {
        Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag);
        Task<int> AddAsync(ProjectTask request);
        Task<int> DeleteAsync(Guid id, Guid tenantId);
        Task<ProjectTask?> FindAsync(Guid id, Guid tenantId);
        Task<List<ProjectTask>> GetAsync(RequestGetProjectTasks request);
        Task<int> UpdateAsync(ProjectTask request);
    }
}