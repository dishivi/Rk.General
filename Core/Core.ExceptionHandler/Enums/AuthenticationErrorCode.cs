using System.ComponentModel;

namespace Core.ExceptionHandler.Enums
{
    public enum AuthenticationErrorCode
    {
        /// <summary>
        /// The access denied
        /// </summary>
        [Description("A0x001")]
        AccessDenied = 1,

        /// <summary>
        /// The unauthorized user
        /// </summary>
        [Description("A0x002")]
        UnauthorizedUser = 2,

        /// <summary>
        /// The invalid API key
        /// </summary>
        [Description("A0x003")]
        InvalidAPIKey = 3,

        /// <summary>
        /// The token expired
        /// </summary>
        [Description("A0x004")]
        TokenExpired = 4,
    }
}
