using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Models
{
    /// <summary>
    /// A register request sent to the User Controller.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// User display name (does not have to be unique)
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// User email (has to be unique)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Plain text password
        /// </summary>
        public string Password { get; set; }
    }
}
