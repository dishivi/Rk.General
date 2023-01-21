using Microsoft.AspNetCore.Mvc;
using Rk.General.Utility.Common;
using System.Reflection;

namespace Base.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {

        }

        protected virtual void SetIdentity<T>(ref T request)
        {
            foreach (var item in Request.Headers)
            {
                PropertyInfo property = request.GetType().GetProperty(item.Key);
                if (property != null)
                {
                    property.SetValue(request, CastConversion.ConvertTo(property.PropertyType, item.Value), null);
                }
            }
        }
    }
}
