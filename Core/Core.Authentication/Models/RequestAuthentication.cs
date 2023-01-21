
namespace Core.Authentication.Models
{
    public class RequestAuthentication : AuthenticationProperties
    {
        public List<AuthenticationClaim> Claims { get; set; }
    }
}
