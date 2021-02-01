using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Data
{
    public class JanusContext : DbContext
    {
        public JanusContext(DbContextOptions<JanusContext> options) : base(options)
        {}

        /// <summary>
        /// User table, holds account information for users.
        /// </summary>
        public DbSet<UserEntity> User { get; set; } 
    }
}
