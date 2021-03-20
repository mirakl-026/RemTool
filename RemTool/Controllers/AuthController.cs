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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private string credPath;
        private string admEmail;
        private string admPass;

        public AuthController(IWebHostEnvironment env)
        {
            credPath = env.ContentRootPath + "/_credentials/";

            string logPath = credPath + "adm_log.txt";
            FileInfo fileLog = new FileInfo(logPath);
            if (fileLog.Exists)
            {
                using (StreamReader sr = new StreamReader(logPath, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        admEmail = line;
                    }
                }
            }

            string pasPath = credPath + "adm_pas.txt";
            FileInfo filePas = new FileInfo(pasPath);
            if (filePas.Exists)
            {
                using (StreamReader sr = new StreamReader(pasPath, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        admPass = line;
                    }
                }
            }
        }

        [Route("CheckAuth")]
        [HttpGet]
        [Authorize]
        public IActionResult CheckAuth()
        {
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login ([FromBody]Login request)
        {
            if (request == null)
                return BadRequest("Invalid user request");

            if (admEmail == null || admPass == null)
                return BadRequest("Cant");

            //if (request.Email == "admin@gmail.com" && request.Password == "iddqd1idkfa2")
            if (request.Email == admEmail && request.Password == admPass)
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
