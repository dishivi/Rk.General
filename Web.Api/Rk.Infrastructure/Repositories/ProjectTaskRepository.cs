using Core.ExceptionHandler.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Rk.Domain.DbEntities;
using Rk.Domain.Request;
using Rk.General.Utility.Common.Extensions;
using Rk.Infrastructure.Contracts;

namespace Rk.Infrastructure.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TimesheetContext _context;

        public ProjectTaskRepository(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectTask>> GetAsync(RequestGetProjectTasks request)
        {
            var data = _context.Tasks.Where(x => x.TenantId == request.TenantId && x.IsActive == request.IsActive && x.IsDeleted == request.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.Name))
                data = data.Where(x => x.Name.Contains(request.Name));

            if (request.ProjectId is not null)
                data = data.Where(x => x.ProjectId == request.ProjectId);

            return await Task.FromResult(data.PageWithSort(request).AsNoTracking().ToList());
        }

        public async Task<ProjectTask?> FindAsync(Guid id, Guid tenantId)
        {
            var data = _context.Tasks.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId && x.IsActive && !x.IsDeleted);

            return await Task.FromResult(data);
        }

        public async Task<int> AddAsync(ProjectTask request)
        {
            _context.Tasks.Add(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> UpdateAsync(ProjectTask request)
        {
            if (_context.Entry(request).State == EntityState.Detached)
                _context.Attach(request);

            _context.Tasks.Update(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> DeleteAsync(Guid id, Guid tenantId)
        {
            var detail = _context.Tasks.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(ProjectTask));

            _context.Tasks.Remove(detail);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag)
        {
            var detail = _context.Tasks.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(ProjectTask));

            var logs = _context.TimeLogs.Where(x => x.TaskId == id && x.IsActive != flag).ToList();
            if (logs.Any())
            {
                foreach (var log in logs)
                    log.IsActive = flag;

                _context.TimeLogs.UpdateRange(logs);
            }

            if (detail.IsActive != flag)
            {
                detail.IsActive = flag;
                _context.Tasks.Update(detail);
            }

            return await Task.FromResult(_context.SaveChanges());
        }
    }
}
