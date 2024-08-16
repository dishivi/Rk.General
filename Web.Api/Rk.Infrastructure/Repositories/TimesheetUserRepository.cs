using Core.ExceptionHandler.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Rk.Domain.DbEntities;
using Rk.Domain.Request;
using Rk.General.Utility.Common.Extensions;
using Rk.Infrastructure.Contracts;

namespace Rk.Infrastructure.Repositories
{
    public class TimesheetUserRepository : ITimesheetUserRepository
    {
        private readonly TimesheetContext _context;

        public TimesheetUserRepository(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<List<TimesheetUser>> GetAsync(RequestGetTimesheetUsers request)
        {
            var data = _context.Users.Where(x => x.TenantId == request.TenantId && x.IsActive == request.IsActive && x.IsDeleted == request.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.Name))
                data = data.Where(x => x.Name.Contains(request.Name));

            if (!string.IsNullOrWhiteSpace(request.Email))
                data = data.Where(x => x.Email.Contains(request.Email));

            return await Task.FromResult(data.PageWithSort(request).AsNoTracking().ToList());
        }

        public async Task<TimesheetUser?> FindAsync(Guid id, Guid tenantId)
        {
            var data = _context.Users.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId && x.IsActive && !x.IsDeleted);

            return await Task.FromResult(data);
        }

        public async Task<TimesheetUser?> ValidateUserAuthAsync(string username, string password, Guid tenantId)
        {
            var data = _context.Users.SingleOrDefault(x => x.Email == username && x.Password == password && x.TenantId == tenantId && x.IsActive && !x.IsDeleted);

            return await Task.FromResult(data);
        }

        public async Task<int> AddAsync(TimesheetUser request)
        {
            _context.Users.Add(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> UpdateAsync(TimesheetUser request)
        {
            if (_context.Entry(request).State == EntityState.Detached)
                _context.Attach(request);

            _context.Users.Update(request);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> DeleteAsync(Guid id, Guid tenantId)
        {
            var detail = _context.Users.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TimesheetClient));

            _context.Users.Remove(detail);
            return await Task.FromResult(_context.SaveChanges());
        }

        public async Task<int> UpdateStatusAsync(Guid id, Guid tenantId, bool flag)   
        {
            var detail = _context.Users.SingleOrDefault(x => x.Id == id && x.TenantId == tenantId);
            if (detail is null) throw DataBaseExceptionHandler.RaiseNoDataFoundExceptionForEntity(nameof(TimesheetClient));

            detail.IsActive = flag;
            _context.Users.Update(detail);
            return await Task.FromResult(_context.SaveChanges());
        }
    }
}
