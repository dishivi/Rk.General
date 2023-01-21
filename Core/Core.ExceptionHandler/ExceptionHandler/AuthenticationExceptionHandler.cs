using Core.ExceptionHandler.Common;
using Core.ExceptionHandler.Enums;
using Core.ExceptionHandler.Extension;
using System.Net;

namespace Core.ExceptionHandler.ExceptionHandler
{
    public static class AuthenticationExceptionHandler
    {
        public static CustomExceptionHandler RaiseAccessDeniedException()
        {
            return new CustomExceptionHandler(ErrorMessages.AccessDenied)
            {
                ErrorCode = AuthenticationErrorCode.AccessDenied.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }

        public static CustomExceptionHandler RaiseUnauthorizedUserException()
        {
            return new CustomExceptionHandler(ErrorMessages.UnauthorizedUser)
            {
                ErrorCode = AuthenticationErrorCode.UnauthorizedUser.ToDescription(),
                HttpStatusCode = HttpStatusCode.Unauthorized
            };
        }

        public static CustomExceptionHandler RaiseInvalidAPIKeyException()
        {
            return new CustomExceptionHandler(ErrorMessages.InvalidAPIKey)
            {
                ErrorCode = AuthenticationErrorCode.InvalidAPIKey.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }

        public static CustomExceptionHandler RaiseTokenExpiredException()
        {
            return new CustomExceptionHandler(ErrorMessages.TokenExpired)
            {
                ErrorCode = AuthenticationErrorCode.TokenExpired.ToDescription(),
                HttpStatusCode = HttpStatusCode.Unauthorized
            };
        }
    }
}
