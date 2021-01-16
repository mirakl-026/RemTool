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
            _db.UpdateRtMailSettings(mailSettings);
            return Ok();

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
            _db.ChangeHQeMail(eMail);
            return Ok();
        }

        [HttpPut("ChangeFlag_notificationToHQ")]
        public IActionResult ChangeFlag_notificationToHQ(bool value)
        {
            _db.ChangeFlag_notificationToHQ(value);
            return Ok();
        }

        [HttpPut("ChangeFlag_notificationToClient")]
        public IActionResult ChangeFlag_notificationToClient(bool value)
        {
            _db.ChangeFlag_notificationToClient(value);
            return Ok();
        }

        [HttpPut("ChangeDefaultMessageToClient")]
        public IActionResult ChangeDefaultMessageToClient(string message)
        {
            _db.ChangeDefaultMessageToClient(message);
            return Ok();
        }

        [HttpPut("ChangeCredentials_Name")]
        public IActionResult ChangeCredentials_Name(string credentialsName)
        {
            _db.ChangeCredentials_Name(credentialsName);
            return Ok();
        }

        [HttpPut("ChangeCredentials_Pass")]
        public IActionResult ChangeCredentials_Pass(string credentialsPass)
        {
            _db.ChangeCredentials_Pass(credentialsPass);
            return Ok();
        }

        [HttpPut("ChangeSmtpServer_Host")]
        public IActionResult ChangeSmtpServer_Host(string smtp_host)
        {
            _db.ChangeSmtpServer_Host(smtp_host);
            return Ok();
        }

        [HttpPut("ChangeSmtpServer_Port")]
        public IActionResult ChangeSmtpServer_Port(string smtp_port)
        {
            _db.ChangeSmtpServer_Port(smtp_port);
            return Ok();
        }
    }
}
