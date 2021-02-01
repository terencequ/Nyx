using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nyx.Janus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController: ControllerBase
    {
        private readonly ILogger _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
