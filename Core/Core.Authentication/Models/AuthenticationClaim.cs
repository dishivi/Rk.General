using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authentication.Models
{
    public  class AuthenticationClaim
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
