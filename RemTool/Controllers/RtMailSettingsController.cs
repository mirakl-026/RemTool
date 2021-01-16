using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

// контроллер для управления настройками почты
namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RtMailSettingsController : ControllerBase
    {
        private readonly IRtMailSettingsService _db;

        public RtMailSettingsController(IRtMailSettingsService context)
        {
            _db = context;
        }

        #region CRUD
        //[HttpPost]
        //public IActionResult CreateRtMailSettings(RtMailSettings mailSettings)
        //{
        //    _db.CreateRtMailSettings(mailSettings);
        //    return Ok();
        //}

        [HttpGet]
        public RtMailSettings ReadRtMailSettings()
        {
            return _db.ReadRtMailSettings();
        }

        [HttpPut]
        public IActionResult UpdateRtMailSettings(RtMailSettings mailSettings)
        {
            if (mailSettings != null)
            {
                _db.UpdateRtMailSettings(mailSettings);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpDelete]
        //public IActionResult DeleteRtMailSettings()
        //{
        //    _db.DeleteRtMailSettings();
        //    return Ok();
        //}
        #endregion



        [HttpPut("ChangeHQeMail")]
        public IActionResult ChangeHQeMail(string eMail)
        {
            if (eMail != null)
            {
                _db.ChangeHQeMail(eMail);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeFlag_notificationToHQ")]
        public IActionResult ChangeFlag_notificationToHQ(bool? value)
        {
            if (value.HasValue)
            {
                _db.ChangeFlag_notificationToHQ(value.Value);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeFlag_notificationToClient")]
        public IActionResult ChangeFlag_notificationToClient(bool? value)
        {
            if (value.HasValue)
            {
                _db.ChangeFlag_notificationToClient(value.Value);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeDefaultMessageToClient")]
        public IActionResult ChangeDefaultMessageToClient(string message)
        {
            if (message != null)
            {
                _db.ChangeDefaultMessageToClient(message);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeCredentials_Name")]
        public IActionResult ChangeCredentials_Name(string credentialsName)
        {
            if (credentialsName != null)
            {
                _db.ChangeCredentials_Name(credentialsName);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeCredentials_Pass")]
        public IActionResult ChangeCredentials_Pass(string credentialsPass)
        {
            if (credentialsPass != null)
            {
                _db.ChangeCredentials_Pass(credentialsPass);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeSmtpServer_Host")]
        public IActionResult ChangeSmtpServer_Host(string smtp_host)
        {
            if (smtp_host != null)
            {
                _db.ChangeSmtpServer_Host(smtp_host);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeSmtpServer_Port")]
        public IActionResult ChangeSmtpServer_Port(string smtp_port)
        {
            if (smtp_port != null)
            {
                _db.ChangeSmtpServer_Port(smtp_port);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
