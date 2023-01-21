using System.ComponentModel;

namespace Core.ExceptionHandler.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum DatabaseErrorCode
    {
        /// <summary>
        /// The entity already exists
        /// </summary>
        [Description("D0x001")]
        EntityAlreadyExists = 1,

        /// <summary>
        /// The no data found
        /// </summary>
        [Description("D0x002")]
        NoDataFound = 2,

        /// <summary>
        /// The invalid data
        /// </summary>
        [Description("D0x003")]
        InvalidData = 3,

        /// <summary>
        /// The can not delete
        /// </summary>
        [Description("D0x004")]
        CanNotDelete = 4,

        /// <summary>
        /// The duplicate data
        /// </summary>
        [Description("D0x005")]
        DuplicateData = 5
    }
}
