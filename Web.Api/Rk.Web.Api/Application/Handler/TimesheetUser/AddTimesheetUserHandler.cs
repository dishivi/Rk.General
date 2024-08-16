using Core.ExceptionHandler.ExceptionHandler;
using MediatR;
using Rk.Infrastructure;
using Rk.Infrastructure.Contracts;
using Rk.Web.Api.Application.Request.TimesheetUser;

namespace Rk.Web.Api.Application.Handler.TimesheetUser
{
    public class AddTimesheetUserHandler : IRequestHandler<RequestAddTimesheetUser, string>
    {
        private readonly ITimesheetUserRepository _timesheetUserRepository;


        public AddTimesheetUserHandler(ITimesheetUserRepository timesheetUserRepository)
        {
            _timesheetUserRepository = timesheetUserRepository;
        }
        public async Task<string> Handle(RequestAddTimesheetUser request, CancellationToken cancellationToken)
        {
            DateTime dtNow = DateTime.UtcNow;
            Guid id = Guid.NewGuid();

            var data = await _timesheetUserRepository.GetAsync(new Domain.Request.RequestGetTimesheetUsers
            {
                Email = request.Email,
                TenantId = request.Tenant_Id
            });

            if (data.Count > 0) Business.RaiseAlreadyExistsException(nameof(TimesheetUser));

            await _timesheetUserRepository.AddAsync(new Domain.DbEntities.TimesheetUser
            {
                BillableRatePerHour = request.BillableRatePerHour,
                LaborRatePerHour = request.LaborRatePerHour,
                Email = request.Email,
                Name = request.Name,
                Role = request.Role,
                Password = request.Password,
                Note = request.Note,
                Id = id,
                CreatedBy = request.Identity_Id,
                CreatedDate = dtNow,
                ModifiedBy = request.Identity_Id,
                ModifiedDate = dtNow,
                TenantId = request.Tenant_Id
            });

            return id.ToString();
        }
    }
}
