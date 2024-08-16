using Core.ExceptionHandler.ExceptionHandler;
using MediatR;
using Rk.General.Utility.Common;
using Rk.Infrastructure;
using Rk.Infrastructure.Contracts;
using Rk.Web.Api.Application.Request.TimesheetClient;

namespace Rk.Web.Api.Application.Handler.TimesheetClient
{
    public class UpdateTimesheetClientHandler : IRequestHandler<RequestUpdateTimesheetClient, string>
    {
        private readonly ITimesheetClientRepository _timesheetClientRepository;

        public UpdateTimesheetClientHandler(ITimesheetClientRepository timesheetClientRepository)
        {
            _timesheetClientRepository = timesheetClientRepository;
        }

        public async Task<string> Handle(RequestUpdateTimesheetClient request, CancellationToken cancellationToken)
        {
            DateTime dtNow = DateTime.UtcNow;

            var detail = await _timesheetClientRepository.FindAsync(request.Id.ToGuid() ?? Guid.Empty, request.Tenant_Id);

            if (detail is null) throw Business.RaiseNoDataFoundException(nameof(TimesheetClient));

            detail.Address = request.Address;
            detail.Name = request.Name;
            detail.Email = request.Email;
            detail.Phone = request.Phone;
            detail.ModifiedBy = request.Identity_Id;
            detail.ModifiedDate = dtNow;

            await _timesheetClientRepository.UpdateAsync(detail);

            return request.Id;
        }
    }
}
