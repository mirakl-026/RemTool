using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using RemTool.Models;
using RemTool.Infrastructure.Additional;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _options;
        public AuthController(IOptions<AuthOptions> options)
        {
            this._options = options;
        }

        private List<Account> Accounts => new List<Account>
        {
            new Account()
            {
                Id = Guid.Parse("12345678"),
                Email = "user1@gmail.com",
                Password = "user1",
                Roles = new Role[] { Role.User }
            },
            new Account()
            {
                Id = Guid.Parse("23456789"),
                Email = "user2@gmail.com",
                Password = "user2",
                Roles = new Role[] { Role.User }
            },
            new Account()
            {
                Id = Guid.Parse("34567890"),
                Email = "admin@gmail.com",
                Password = "admin",
                Roles = new Role[] { Role.Admin }
            }
        };

        [Route("/login")]
        [HttpPost]
        public IActionResult Login ([FromBody]Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);

            if (user != null)
            {
                // Generate JWT
                var token = GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        private string GenerateJWT(Account user)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
