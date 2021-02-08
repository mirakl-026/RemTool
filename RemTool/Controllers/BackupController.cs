using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mime;

using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.IO;

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

        [HttpGet("UnpackFromZipHard")]
        [Authorize]
        public async Task<IActionResult> UnpackFromZipHard()
        {
            await _context.UnZipToServerWithHardReload();
            return Ok();
        }

        [HttpGet("UnpackFromZipSoft")]
        [Authorize]
        public async Task<IActionResult> UnpackFromZipSoft()
        {
            await _context.UnZipToServerWithSoftReload();
            return Ok();
        }



        // загрузить бэкап
        [HttpPost("LoadBackup")]
        [RequestSizeLimit(100428800)]
        [Authorize]
        public async Task<IActionResult> LoadBackup(IFormFile newBackup)
        {
            if (newBackup != null)
            {
                await _context.ReplaceBackupToNew(newBackup);
                return Ok();
            }
            return NoContent();
        }

        [HttpGet("DownloadBackup")]
        [Route("Stream")]
        [Authorize]
        public IActionResult DownloadBackup()
        {
            string mimeType = "application/zip";
            return new FileContentResult(_context.ReadBackupFromSystem(), mimeType)
            {
                FileDownloadName = "backup.zip"
            };
        }
    }
}
