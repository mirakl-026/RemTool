using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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
            }

            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
