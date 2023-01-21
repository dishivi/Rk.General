using System.ComponentModel;

namespace Core.ExceptionHandler.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum BusinessErrorCode
    {
        /// <summary>
        /// The success
        /// </summary>
        [Description("B0x001")]
        Success = 1,

        /// <summary>
        /// The entity can not be empty
        /// </summary>
        [Description("B0x002")]
        EntityCanNotBeEmpty = 2,

        /// <summary>
        /// The invalid entity
        /// </summary>
        [Description("B0x003")]
        InvalidEntity = 3,

        /// <summary>
        /// The request can not be null
        /// </summary>
        [Description("B0x004")]
        RequestCanNotBeNull = 4,

        /// <summary>
        /// The operational error
        /// </summary>
        [Description("B0x005")]
        OperationalError = 5,

        /// <summary>
        /// The created
        /// </summary>
        [Description("B0x006")]
        Created = 6,

        /// <summary>
        /// The updated
        /// </summary>
        [Description("B0x007")]
        Updated = 7,

        /// <summary>
        /// The deleted
        /// </summary>
        [Description("B0x008")]
        Deleted = 8,

        /// <summary>
        /// The no content
        /// </summary>
        [Description("B0x009")]
        NoContent = 9,

        /// <summary>
        /// The invalid data
        /// </summary>
        [Description("B0x010")]
        InvalidData = 10,

        /// <summary>
        /// The invalid length
        /// </summary>
        [Description("B0x011")]
        InvalidLength = 11,


    }
}
