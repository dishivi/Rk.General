using Core.ExceptionHandler.ExceptionHandler;
using MediatR;
using Rk.Infrastructure;
using Rk.Infrastructure.Contracts;
using Rk.Web.Api.Application.Request.TimesheetClient;

namespace Rk.Web.Api.Application.Handler.TimesheetClient
{
    public class AddTimesheetClientHandler : IRequestHandler<RequestAddTimesheetClient, string>
    {
        private readonly ITimesheetClientRepository _timesheetClientRepository;


        public AddTimesheetClientHandler(ITimesheetClientRepository timesheetClientRepository)
        {
            _timesheetClientRepository = timesheetClientRepository;
        }
        public async Task<string> Handle(RequestAddTimesheetClient request, CancellationToken cancellationToken)
        {
            DateTime dtNow = DateTime.UtcNow;
            Guid id = Guid.NewGuid();

            var data = await _timesheetClientRepository.GetAsync(new Domain.Request.RequestGetTimesheetClients
            {
                Email = request.Email,
                TenantId = request.Tenant_Id
            });

            if (data.Count > 0) Business.RaiseAlreadyExistsException(nameof(TimesheetClient));

            await _timesheetClientRepository.AddAsync(new Domain.DbEntities.TimesheetClient
            {
                Address = request.Address,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
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
