
using Core.Authentication.Models;

namespace Core.Authentication.Interfaces
{
    public interface IAuthenticationWith
    {
        string GenerateToken(RequestAuthentication claims);

        ResponseAuthentication ValidateToken(string token);

        Task<ResponseAuthentication> ValidateGoogleToken(string token);
    }
}
