using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Models.Http
{
    public class HttpClientConfiguration : HttpRetryPolicy
    {
        public string BaseAddress { get; set; }

        public string ClientName { get; set; }

        public Dictionary<string, string> Headers { get; set; }
    }
}
