using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Nyx.Janus.Api.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using Nyx.Janus.Api.Security;
using Nyx.Janus.Api.Data;
using Nyx.Janus.Api.Config;
using Microsoft.Extensions.Options;
using Nyx.Janus.Api.Models.Response;

namespace Nyx.Janus.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JanusContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly SecurityOptions _securityOptions;

        public UserController(
            JanusContext context,
            ILogger<UserController> logger,
            IOptions<SecurityOptions> securityOptions)
        {
            _context = context;
            _logger = logger;
            _securityOptions = securityOptions.Value;
        }

        /// <summary>
        /// Create a new user account, when supplied with a new email.
        /// </summary>
        /// <param name="registerRequest">New account details</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            _logger.LogInformation("Register request", registerRequest);
            // Check for duplicate user
            UserEntity duplicateUser = _context.User.SingleOrDefault(user => user.Email == registerRequest.Email);
            if (duplicateUser != null)
            {
                return Unauthorized(new { Message = "Email already exists." });
            }

            // Generate a new user and add it to the database
            UserEntity user = new UserEntity()
            {
                DisplayName = registerRequest.DisplayName,
                Email = registerRequest.Email,
                Password = HashUtils.HashAndSaltPassword(registerRequest.Password),
                CreationDate = DateTime.Now,
                IsActive = true
            };

            _context.User.Add(user);
            _context.SaveChanges();

            return Ok(
                new UserResponse
                {
                    Message = "Registered user successfully.",
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    CreationDate = user.CreationDate.ToString()
                }
            );
        }

        /// <summary>
        /// Attempt to login, and retrieve a JWT token.
        /// </summary>
        /// <param name="loginRequest">Login details</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            UserEntity user = _context.User.SingleOrDefault(user => user.Email == loginRequest.Email);
            if (user == null)
            {
                _logger.LogInformation($"{loginRequest.Email} tried to log in, but no user was found under that email. ");
                return Unauthorized(new { Message = "Email is not registered to any user." });
            }
            bool verified = HashUtils.VerifyHashedPassword(user.Password, loginRequest.Password);
            if (!verified)
            {
                _logger.LogInformation($"{loginRequest.Email} tried to log in, but failed to provide correct password. ");
                return Unauthorized(new { Message = "Incorrect password." });
            }

            string token = JWTUtils.GenerateTokenFromUser(_securityOptions, user.Id);

            _logger.LogInformation($"{loginRequest.Email} logged in successfully, and generated a token.");
            return Ok(
                new AuthTokenResponse
                {
                    Message = "User logged in successfully.",
                    Token = token,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    CreationDate = user.CreationDate.ToString()
                }
            );

        }

        /// <summary>
        /// Attempt to get the user's profile.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetUserProfile()
        {
            // Get userId from JWT identity
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = identity.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;

            try
            {
                // Get user from context
                UserEntity user = _context.User.Find(Guid.Parse(userId));

                _logger.LogInformation($"{user.Email} retrieved their profile information. ");
                // Return OK
                return Ok(
                    new UserResponse
                    {
                        Message = "Profile obtained successfully.",
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        CreationDate = user.CreationDate.ToString()
                    }
                );
            }
            catch
            {
                _logger.LogInformation($" {userId} tried to retrieve their profile information, but failed. ");
                return NotFound(
                    new Response
                    {
                        Message = "Could not find profile."
                    }
                );
            }
        }
    }
}
