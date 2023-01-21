using Core.Models.Application.Request;

namespace Rk.Web.Api.Models
{
    public class BaseRequest : RequestBase
    {
        public Guid CompanyId { get; set; }
    }
}
