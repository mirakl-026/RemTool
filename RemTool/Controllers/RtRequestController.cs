using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RtRequestController : ControllerBase
    {
        private readonly IRtRequestService db;

        public RtRequestController(IRtRequestService context)
        {
            db = context;
        }

        // Get - get all requests
        [HttpGet]
        public IEnumerable<RtRequest> Get()
        {
            return db.ReadAllRtRequests();
        }

        // Get{id} - get request by id
        [HttpGet("{id}")]
        public RtRequest Get(string id)
        {
            RtRequest rtreq = db.ReadRtRequest(id);
            return rtreq;
        }

        // Post - add request
        [HttpPost]
        public IActionResult Post(RtRequest newRtreq)
        {
            if (ModelState.IsValid)
            {
                db.CreateRtRequest(newRtreq);
                return Ok(newRtreq);
            }
            return BadRequest(ModelState);
        }

        // Delete{id} - delete request
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            RtRequest rtreq = db.ReadRtRequest(id);
            if (rtreq != null)
            {
                db.DeleteRtRequest(id);
            }
            return Ok(rtreq);
        }
    }
}
