using System.ComponentModel;

namespace Core.ExceptionHandler.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum GeneralErrorCode
    {
        /// <summary>
        /// The HTTP version not supported
        /// </summary>
        [Description("C0x001")]
        HttpVersionNotSupported = 1,

        /// <summary>
        /// The request timeout
        /// </summary>
        [Description("C0x002")]
        RequestTimeout = 2,

        /// <summary>
        /// The bad request
        /// </summary>
        [Description("C0x003")]
        BadRequest = 3,

        /// <summary>
        /// The configuration missing
        /// </summary>
        [Description("C0x004")]
        ConfigurationMissing = 4,

        /// <summary>
        /// The third party service not available
        /// </summary>
        [Description("C0x005")]
        ThirdPartyServiceNotAvailable = 5,

        /// <summary>
        /// The third party service not implemented
        /// </summary>
        [Description("C0x006")]
        ThirdPartyServiceNotImplemented = 6,

        /// <summary>
        /// The third party operation fail error
        /// </summary>
        [Description("C0x007")]
        ThirdPartyOperationFailError = 7,

        /// <summary>
        /// The third party service token null
        /// </summary>
        [Description("C0x008")]
        ThirdPartyServiceTokenNull = 8,
    }
}
