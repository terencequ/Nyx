using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Models
{
    /// <summary>
    /// A login request sent to the User Controller.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Plain text password
        /// </summary>
        public string Password { get; set; }
    }

}
