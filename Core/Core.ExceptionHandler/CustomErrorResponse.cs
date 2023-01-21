using Newtonsoft.Json;

namespace Core.ExceptionHandler
{
    public class CustomErrorResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
