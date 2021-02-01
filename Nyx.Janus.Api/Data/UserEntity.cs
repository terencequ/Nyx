using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Data
{
    /// <summary>
    /// The User class represents a user within the Chicken Voyage system.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Unique identifier for user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name that will be displayed to other users.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Email, used to log into the system.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password, used to log in to the system. Stored as hashed version.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// When this user's account was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// If the user's account is enabled or disabled.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
