using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly IBackUpService _context;

        public BackupController(IBackUpService context)
        {
            _context = context;
        }


        [HttpGet("PackToZip")]
        [Authorize]
        public async Task<IActionResult> PackToZip()
        {
            await _context.SaveServerToZip();
            return Ok();
        }

        [HttpGet("UnpackFromZip")]
        [Authorize]
        public async Task<IActionResult> UnpackFromZip()
        {
            await _context.UnZipToServer();
            return Ok();
        }
    }
}
