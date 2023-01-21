using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.Webapi.Interface
{
    public interface IAuthenticationProcess
    {
        void DoingAuth(ActionExecutingContext context);
    }
}
