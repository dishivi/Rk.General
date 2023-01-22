using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Models.Http
{
    public class BaseHttpRequest
    {
        public Dictionary<string, string> Headers { get; set; }
        public string BaseAddress { get; set; }
        public string Url { get; set; }
    }
}
