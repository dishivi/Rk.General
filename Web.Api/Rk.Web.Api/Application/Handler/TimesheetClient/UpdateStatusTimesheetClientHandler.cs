using Core.ExceptionHandler.ExceptionHandler;
using MediatR;
using Rk.General.Utility.Common;
using Rk.Infrastructure;
using Rk.Infrastructure.Contracts;
using Rk.Web.Api.Application.Request.TimesheetClient;

namespace Rk.Web.Api.Application.Handler.TimesheetClient
{
    public class UpdateStatusTimesheetClientHandler : IRequestHandler<RequestUpdateStatusTimesheetClient, string>
    {
        private readonly ITimesheetClientRepository _timesheetClientRepository;

        public UpdateStatusTimesheetClientHandler(ITimesheetClientRepository timesheetClientRepository)
        {
            _timesheetClientRepository = timesheetClientRepository;
        }

        public async Task<string> Handle(RequestUpdateStatusTimesheetClient request, CancellationToken cancellationToken)
        {
            DateTime dtNow = DateTime.UtcNow;

            var detail = await _timesheetClientRepository.UpdateStatusAsync(request.Id.ToGuid() ?? Guid.Empty, request.Tenant_Id, request.Status);

            return request.Id;
        }
    }
}
