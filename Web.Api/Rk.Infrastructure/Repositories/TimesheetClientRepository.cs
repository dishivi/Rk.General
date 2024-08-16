using Core.ExceptionHandler.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Rk.Domain.DbEntities;
using Rk.Domain.Request;
using Rk.General.Utility.Common.Extensions;
using Rk.Infrastructure.Contracts;

namespace Rk.Infrastructure.Repositories
{
    public class TimesheetClientRepository : ITimesheetClientRepository
    {
        private readonly TimesheetContext _context;

        public TimesheetClientRepository(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<List<TimesheetClient>> GetAsync(RequestGetTimesheetClients request)
        {
            var data = _context.Clients.Where(x => x.TenantId == request.TenantId && x.IsActive == request.IsActive && x.IsDeleted == request.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.Name))
                data = data.Where(x => x.Name.Contains(request.Name));

            if (!string.IsNullOrWhiteSpace(request.Email))
                data = data.Where(x => x.Email.Contains(request.Email));

            return await Task.FromResult(data.PageWithSort(request).AsNoTracking().ToList());
        }

        public async Task<TimesheetClient?> FindAsync(Guid id, Guid tenantId)
        {
            var data = _context.Clients.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId && x.IsActive && !x.IsDeleted);

            return await Task.FromResult(data);
        }

        public async Task<int> AddAsync(TimesheetClient request)
        {
            _context.Clients.Add(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> UpdateAsync(TimesheetClient request)
        {
            if (_context.Entry(request).State == EntityState.Detached)
                _context.Attach(request);

            _context.Clients.Update(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> DeleteAsync(Guid id, Guid tenantId)
        {
            var detail = _context.Clients.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TimesheetClient));

            _context.Clients.Remove(detail);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag)
        {
            var detail = _context.Clients.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TimesheetClient));

            var projects = _context.Projects.Where(x => x.ClientId == detail.Id && x.IsActive != flag).ToList();
            if (projects.Count > 0)
            {
                foreach (var project in projects)
                {
                    project.IsActive = flag;

                    var tasks = _context.Tasks.Where(x => x.ProjectId == project.Id && x.IsActive != flag).ToList();
                    if (!tasks.Any()) continue;

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

                _context.Projects.UpdateRange(projects);
            }

            detail.IsActive = flag;
            _context.Clients.Update(detail);
            return await Task.FromResult(_context.SaveChanges());
        }
    }
}
