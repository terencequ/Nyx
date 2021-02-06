using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Models.Response
{
    /// <summary>
    /// Response containing provisioned auth token, and user details.
    /// </summary>
    public class AuthTokenResponse : UserResponse
    {
        public string Token { get; set; }
    }
}
