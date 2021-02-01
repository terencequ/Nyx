using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Nyx.Janus.Api.Config;

namespace Nyx.Janus.Api.Security
{
    public static class JWTUtils
    {
        /// <summary>
        /// Create a JWT token.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GenerateTokenFromUser(SecurityOptions options, Guid userId)
        {
            // Generate security key
            var mySecret = options.AuthTokenSecret;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            // Get issuer and audience
            var myIssuer = options.AuthTokenIssuer;
            var myAudience = options.AuthTokenAudience;

            // Generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            // Return token
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Create validation parameters for JWT tokens. To be used in validation.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static TokenValidationParameters GenerateTokenValidationParameters(SecurityOptions options)
        {
            var tokenSecret = options.AuthTokenSecret;
            var tokenIssuer = options.AuthTokenIssuer;
            var tokenAudience = options.AuthTokenAudience;

            return new TokenValidationParameters
            {
                // Clock skew compensates for server time drift.
                ClockSkew = TimeSpan.FromMinutes(2),

                // Specify the key used to sign the token:
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSecret)),
                RequireSignedTokens = true,

                // Ensure the token hasn't expired:
                RequireExpirationTime = true,
                ValidateLifetime = true,

                // Ensure the token audience matches our audience value (default true):
                ValidateAudience = true,
                ValidAudience = tokenAudience,

                // Ensure the token was issued by a trusted authorization server (default true):
                ValidateIssuer = true,
                ValidIssuer = tokenIssuer
            };
        }
    }
}
