using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Config
{
    public class SecurityOptions
    {
        public const string Security = "Security";

        public string AuthTokenSecret { get; set; }

        public string AuthTokenIssuer { get; set; }

        public string AuthTokenAudience { get; set; }
    }
}
