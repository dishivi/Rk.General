using Core.Models.Application.Request;
using MediatR;
using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Rk.Web.Api.Application.Request.TimesheetClient
{
    public class RequestUpdateStatusTimesheetClient : RequestBase, IRequest<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.