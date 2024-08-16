using Core.ExceptionHandler.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Rk.Domain.DbEntities;
using Rk.Domain.Request;
using Rk.General.Utility.Common.Extensions;
using Rk.Infrastructure.Contracts;

namespace Rk.Infrastructure.Repositories
{
    public class TaskTimeLogRepository : ITaskTimeLogRepository
    {
        private readonly TimesheetContext _context;

        public TaskTimeLogRepository(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<List<TaskTimeLog>> GetAsync(RequestGetTaskTimeLogs request)
        {
            var data = _context.TimeLogs.Where(x => x.TenantId == request.TenantId && x.IsActive == request.IsActive && x.IsDeleted == request.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.Status))
                data = data.Where(x => x.Status == request.Status);

            if (request.TaskId is not null)
                data = data.Where(x => x.TaskId == request.TaskId);

            if (request.FromTimeLogDate is not null)
                data = data.Where(x => x.TimeLogDate >= request.FromTimeLogDate);

            if (request.ToTimeLogDate is not null)
                data = data.Where(x => x.TimeLogDate <= request.ToTimeLogDate);

            return await Task.FromResult(data.PageWithSort(request).AsNoTracking().ToList());
        }

        public async Task<TaskTimeLog?> FindAsync(Guid id, Guid tenantId)
        {
            var data = _context.TimeLogs.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId && x.IsActive && !x.IsDeleted);

            return await Task.FromResult(data);
        }

        public async Task<int> AddAsync(TaskTimeLog request)
        {
            _context.TimeLogs.Add(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> UpdateAsync(TaskTimeLog request)
        {
            if (_context.Entry(request).State == EntityState.Detached)
                _context.Attach(request);

            _context.TimeLogs.Update(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> DeleteAsync(Guid id, Guid tenantId)
        {
            var detail = _context.TimeLogs.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TaskTimeLog));

            _context.TimeLogs.Remove(detail);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> ActiveInActiveAsync(Guid id, Guid tenantId, bool flag)
        {
            var detail = _context.TimeLogs.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TaskTimeLog));

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
                _context.TimeLogs.Update(detail);
            }

            return await Task.FromResult(_context.SaveChanges());
        }
    }
}
