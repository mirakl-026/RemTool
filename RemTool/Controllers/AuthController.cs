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
using System.Text;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    { 

        [Route("login")]
        [HttpPost]
        public IActionResult Login ([FromBody]Login request)
        {
            if (request == null)
                return BadRequest("Invalid user request");

            if (request.Email == "admin@gmail.com" && request.Password == "adminadmin")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("whoWillSaveYouNow?123456789+-"));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "RemToolBack",
                    audience: "RemToolFront",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    idToken = tokenString,
                    expiresIn = 3600
                });
            }
            return Unauthorized();
        }
    }
}
