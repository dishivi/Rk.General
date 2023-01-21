using Core.Models.Application.Request;
using MediatR;

namespace Rk.Web.Api.Application.Request
{
    public class RequestAddEmployee : RequestBase, IRequest<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
