using MediatR;
using Rk.General.Utility.Common;
using Rk.Infrastructure.Contracts;
using Rk.Web.Api.Application.Request.TimesheetUser;

namespace Rk.Web.Api.Application.Handler.TimesheetUser
{
    public class UpdateStatusTimesheetUserHandler : IRequestHandler<RequestUpdateStatusTimesheetUser, string>
    {
        private readonly ITimesheetUserRepository _timesheetUserRepository;

        public UpdateStatusTimesheetUserHandler(ITimesheetUserRepository timesheetUserRepository)
        {
            _timesheetUserRepository = timesheetUserRepository;
        }

        public async Task<string> Handle(RequestUpdateStatusTimesheetUser request, CancellationToken cancellationToken)
        {
            DateTime dtNow = DateTime.UtcNow;

            var detail = await _timesheetUserRepository.UpdateStatusAsync(request.Id.ToGuid() ?? Guid.Empty, request.Tenant_Id, request.Status);

            return request.Id;
        }
    }
}
