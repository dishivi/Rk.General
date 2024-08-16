
using Newtonsoft.Json;

namespace Core.Models.Application.Request
{
    public abstract class RequestBase
    {
        /// <summary>
        /// Identifier id
        /// </summary>
        [JsonIgnore]
        public Guid Identity_Id { get; set; }

        /// <summary>
        /// Identifier name
        /// </summary>
        [JsonIgnore]
        public string Identity_Name { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        [JsonIgnore]
        public Guid Tenant_Id { get; set; }
    }
}
