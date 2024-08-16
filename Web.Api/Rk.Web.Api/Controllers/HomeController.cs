using Base.Webapi.Attributes;
using Base.Webapi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Rk.Web.Api.Application.Request;

namespace Rk.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(AuthItAttribute))]
    public class HomeController : BaseApiController
    {
        public HomeController()
        {
        }

        //[HttpPost(Name = "Validate")]
        //public bool ValidateToken([FromBody] RequestAddEmployee request)
        //{
        //    SetIdentity(ref request);


        //    return true;
        //}
    }
}
