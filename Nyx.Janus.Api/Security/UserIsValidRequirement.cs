using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Security
{
    public class UserIsValidRequirement : IAuthorizationRequirement
    {

        public UserIsValidRequirement()
        {

        }
    }
}
