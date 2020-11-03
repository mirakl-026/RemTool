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
                Id = Guid.Parse("fb30fd52-de02-4c22-a240-922653eadd57"),
                Email = "user1@gmail.com",
                Password = "user1",
                Roles = new Role[] { Role.User }
            },
            new Account()
            {
                Id = Guid.Parse("6bd540c7-5cfe-4876-b471-eaa37c44b1e0"),
                Email = "user2@gmail.com",
                Password = "user2",
                Roles = new Role[] { Role.User }
            },
            new Account()
            {
                Id = Guid.Parse("69c05b34-f227-4c9a-b126-53659ac76f90"),
                Email = "admin@gmail.com",
                Password = "adminadmin",
                Roles = new Role[] { Role.Admin }
            }
        };

        [Route("login")]
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
