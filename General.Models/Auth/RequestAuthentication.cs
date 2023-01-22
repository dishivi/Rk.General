
namespace General.Models.Auth
{
    public class RequestAuthentication : AuthenticationProperties
    {
        public List<AuthenticationClaim> Claims { get; set; }
    }
}
