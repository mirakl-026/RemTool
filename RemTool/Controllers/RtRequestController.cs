using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Infrastructure.Additional;
using Microsoft.AspNetCore.Authorization;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RtRequestController : ControllerBase
    {
        private readonly IRtRequestService db;
        private readonly IMailSendSettings mailSendSettings;

        public RtRequestController(IRtRequestService context, IMailSendSettings mailSendSettings)
        {
            db = context;
            this.mailSendSettings = mailSendSettings;
        }

        // Get - get all requests
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<RtRequest>> Get()
        {
            return await db.ReadAllRtRequestsAsync();
        }

        // Get{id} - get request by id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<RtRequest> Get(string id)
        {
            RtRequest rtreq = await db.ReadRtRequestAsync(id);
            return rtreq;
        }

        // Post - add request
        [HttpPost]
        public async Task<IActionResult> Post(RtRequest newRtreq)
        {
            if (ModelState.IsValid)
            {
                var rtReq = await db.ReadRtRequestByPhoneAsync(newRtreq.Phone);

                if(rtReq != null)
                {
                    // < script >
                    //      var nowDate = new Date();
                    //      var day = nowDate.getDate();
                    //      var mounth = nowDate.getMonth();
                    //      var year = nowDate.getFullYear();
                    //      var hours = nowDate.getHours();
                    //      var minutes = nowDate.getMinutes();
                    //      var time = day + " " + mounth + " " + year + " " + hours + " " + minutes;
                    //      document.write(time);
                    // </ script >


                    if (newRtreq.SendedTime != null && rtReq.SendedTime != null)
                    {
                        var newTimeValues = newRtreq.SendedTime.Split(' ');
                        var oldTimeValues = rtReq.SendedTime.Split(' ');


                        if (newTimeValues.Length == 5 && oldTimeValues.Length == 5)
                        {
                            var newTime = 24 * int.Parse(newTimeValues[3]) + 60 * int.Parse(newTimeValues[4]);
                            var oldTime = 24 * int.Parse(oldTimeValues[3]) + 60 * int.Parse(oldTimeValues[4]);

                            if (newTime > oldTime + 60)
                            {
                                await db.UpdateRtRequestAsync(new RtRequest
                                {
                                    Id = rtReq.Id,
                                    Name = newRtreq.Name,
                                    Email = newRtreq.Email,
                                    Phone = newRtreq.Phone,
                                    ReqInfo = newRtreq.ReqInfo,
                                    SendedTime = newRtreq.SendedTime
                                });

                                sendMail(newRtreq.Email);
                            }
                            else
                            {
                                BadRequest("wait");
                            }
                        }
                    }
                    else
                    {
                        BadRequest("bad time");
                    }
                }
                else
                {
                    sendMail(newRtreq.Email);
                    db.CreateRtRequest(newRtreq);
                    return Ok(newRtreq);
                }
            }
            return BadRequest(ModelState);
        }

        // Put - update rtrequest (mark)
        [HttpPut]
        [Authorize]
        public IActionResult Put(RtRequest updatedRtreq)
        {
            if (ModelState.IsValid)
            {
                db.UpdateRtRequest(updatedRtreq);
                return Ok();
            }
            return BadRequest(ModelState);
        }


        // Delete{id} - delete request
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            RtRequest rtreq = db.ReadRtRequest(id);
            if (rtreq != null)
            {
                db.DeleteRtRequest(id);
            }
            return Ok(rtreq);
        }


        public void sendMail(string clientEmail)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(mailSendSettings.Credentials_Name, "RemTool");

            // кому отправляем
            MailAddress to = new MailAddress(clientEmail);

            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);

            // тема письма
            m.Subject = "Запрос";

            // текст письма
            m.Body = "<h3>Ваш запрос передан, с Вами свяжутся...</h3>";

            // письмо представляет код html
            m.IsBodyHtml = true;

            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient(mailSendSettings.Host, 25);

            // логин и пароль
            smtp.Credentials = new NetworkCredential(mailSendSettings.Credentials_Name, mailSendSettings.Credentials_Pass);

            smtp.EnableSsl = true;
            //smtp.EnableSsl = false;
            
            smtp.Send(m);
        }
    }
}
