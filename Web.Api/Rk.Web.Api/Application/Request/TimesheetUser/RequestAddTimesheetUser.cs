using Core.Models.Application.Request;
using MediatR;
using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Rk.Web.Api.Application.Request.TimesheetUser
{
    public class RequestAddTimesheetUser : RequestBase, IRequest<string>
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("laborRatePerHour")]
        public double? LaborRatePerHour { get; set; }

        [JsonProperty("billableRatePerHour")]
        public double? BillableRatePerHour { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.