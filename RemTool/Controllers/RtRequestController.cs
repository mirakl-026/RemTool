using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public IActionResult Post(RtRequest newRtreq)
        {
            if (ModelState.IsValid)
            {
                db.CreateRtRequest(newRtreq);
                return Ok(newRtreq);
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
    }
}
