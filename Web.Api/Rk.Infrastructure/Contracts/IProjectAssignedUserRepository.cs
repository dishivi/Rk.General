using Rk.Domain.DbEntities;

namespace Rk.Infrastructure.Contracts
{
    public interface IProjectAssignedUserRepository
    {
        Task<int> AddAsync(ProjectAssignedUser request);
        Task<int> DeleteAsync(Guid id, Guid tenantId);
        Task<ProjectAssignedUser?> FindAsync(Guid projectId, Guid userId, Guid tenantId);
    }
}