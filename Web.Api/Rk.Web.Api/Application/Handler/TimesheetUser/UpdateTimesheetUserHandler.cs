using Core.ExceptionHandler.ExceptionHandler;
using MediatR;
using Rk.General.Utility.Common;
using Rk.Infrastructure.Contracts;
using Rk.Web.Api.Application.Request.TimesheetClient;
using Rk.Web.Api.Application.Request.TimesheetUser;

namespace Rk.Web.Api.Application.Handler.TimesheetUser
{
    public class UpdateTimesheetUserHandler : IRequestHandler<RequestUpdateTimesheetUser, string>
    {
        private readonly ITimesheetUserRepository _timesheetUserRepository;

        public UpdateTimesheetUserHandler(ITimesheetUserRepository timesheetUserRepository)
        {
            _timesheetUserRepository = timesheetUserRepository;
        }

        public async Task<string> Handle(RequestUpdateTimesheetUser request, CancellationToken cancellationToken)
        {
            DateTime dtNow = DateTime.UtcNow;

            var detail = await _timesheetUserRepository.FindAsync(request.Id.ToGuid() ?? Guid.Empty, request.Tenant_Id);

            if (detail is null) throw Business.RaiseNoDataFoundException(nameof(TimesheetClient));

            detail.Note = request.Note;
            detail.Name = request.Name;
            detail.Email = request.Email;
            detail.Password = request.Password;
            detail.Role = request.Role;
            detail.BillableRatePerHour = request.BillableRatePerHour;
            detail.LaborRatePerHour = request.LaborRatePerHour;
            detail.ModifiedBy = request.Identity_Id;
            detail.ModifiedDate = dtNow;

            await _timesheetUserRepository.UpdateAsync(detail);

            return request.Id;
        }
    }
}
