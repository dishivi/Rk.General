using MediatR;
using Rk.Web.Api.Application.Request;

namespace Rk.Web.Api.Application.Handler
{
    public class AddEmployeeHandler : IRequestHandler<RequestAddEmployee, string>
    {

        public async Task<string> Handle(RequestAddEmployee request, CancellationToken cancellationToken)
        {
            return await Task.FromResult("");
        }
    }
}
