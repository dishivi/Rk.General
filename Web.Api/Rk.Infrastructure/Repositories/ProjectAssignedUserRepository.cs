using Core.ExceptionHandler.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Rk.Domain.DbEntities;
using Rk.Domain.Request;
using Rk.General.Utility.Common.Extensions;
using Rk.Infrastructure.Contracts;

namespace Rk.Infrastructure.Repositories
{
    public class ProjectAssignedUserRepository : IProjectAssignedUserRepository
    {
        private readonly TimesheetContext _context;

        public ProjectAssignedUserRepository(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<ProjectAssignedUser?> FindAsync(Guid projectId, Guid userId, Guid tenantId)
        {
            var data = _context.ProjectAssignedUsers.SingleOrDefault(x => x.ProjectId == projectId && x.UserId == userId && x.TenantId == tenantId && x.IsActive && !x.IsDeleted);

            return await Task.FromResult(data);
        }

        public async Task<int> AddAsync(ProjectAssignedUser request)
        {
            _context.ProjectAssignedUsers.Add(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> DeleteAsync(Guid id, Guid tenantId)
        {
            var detail = _context.ProjectAssignedUsers.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(ProjectAssignedUser));

            _context.ProjectAssignedUsers.Remove(detail);
            return await Task.FromResult(_context.SaveChanges());
        }
    }
}
