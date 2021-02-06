using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Models.Response
{
    /// <summary>
    /// Response containing user details that the user is allowed to see.
    /// This means no password.
    /// </summary>
    public class UserResponse: Response
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string CreationDate { get; set; }
    }
}
