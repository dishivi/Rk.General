using Core.ExceptionHandler.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Rk.Domain.DbEntities;
using Rk.Domain.Request;
using Rk.General.Utility.Common.Extensions;
using Rk.Infrastructure.Contracts;

namespace Rk.Infrastructure.Repositories
{
    public class TimesheetProjectRepository : ITimesheetProjectRepository
    {
        private readonly TimesheetContext _context;

        public TimesheetProjectRepository(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<List<TimesheetProject>> GetAsync(RequestGetTimesheetProjects request)
        {
            var data = _context.Projects.Where(x => x.TenantId == request.TenantId && x.IsActive == request.IsActive && x.IsDeleted == request.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.Name))
                data = data.Where(x => x.Name.Contains(request.Name));

            if (request.ClientId is not null)
                data = data.Where(x => x.ClientId == request.ClientId);

            return await Task.FromResult(data.PageWithSort(request).AsNoTracking().ToList());
        }

        public async Task<TimesheetProject?> FindAsync(Guid id, Guid tenantId)
        {
            var data = _context.Projects.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId && x.IsActive && !x.IsDeleted);

            return await Task.FromResult(data);
        }

        public async Task<int> AddAsync(TimesheetProject request)
        {
            _context.Projects.Add(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> UpdateAsync(TimesheetProject request)
        {
            if (_context.Entry(request).State == EntityState.Detached)
                _context.Attach(request);

            _context.Projects.Update(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> DeleteAsync(Guid id, Guid tenantId)
        {
            var detail = _context.Projects.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TimesheetProject));

            _context.Projects.Remove(detail);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag)
        {
            var detail = _context.Projects.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TimesheetProject));

            var tasks = _context.Tasks.Where(x => x.ProjectId == detail.Id && x.IsActive != flag).ToList();
            if (tasks.Any())
            {
                foreach (var task in tasks)
                {
                    task.IsActive = flag;

                    var logs = _context.TimeLogs.Where(x => x.TaskId == task.Id && x.IsActive != flag).ToList();
                    if (!logs.Any()) continue;

                    foreach (var log in logs)
                        log.IsActive = flag;

                    _context.TimeLogs.UpdateRange(logs);
                }

                _context.Tasks.UpdateRange(tasks);
            }

            if (detail.IsActive != flag)
            {
                detail.IsActive = flag;
                _context.Projects.Update(detail);
            }

            return await Task.FromResult(_context.SaveChanges());
        }
    }
}
