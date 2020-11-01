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
    [Route("api/counters")]
    [ApiController]
    public class ClickCounterController : ControllerBase
    {
        private readonly IClickCounterService db;

        public ClickCounterController(IClickCounterService context)
        {
            db = context;
        }

        // Get{id} - get counter value by counter id
        [HttpGet("{id}")]
        public int Get(string id)
        {
            ClickCounter clickCounter = db.ReadClickCounter(id);
            if (clickCounter != null)
            {
                return clickCounter.Count;
            }
            return 0;
        }

        // Put{id} - increase counter
        [HttpPut("{counterid}/{tooltypeId}")]
        public IActionResult Put(string counterId, string tooltypeId)
        {
            if (ModelState.IsValid)
            {
                ClickCounter clickCounter = db.ReadClickCounter(counterId);
                if (clickCounter != null)
                {
                    db.IncreaseCounter(clickCounter.Id, tooltypeId);
                }                
                return Ok();
            }
            return BadRequest(ModelState);
        }


        // Delete{id} - reset counter by id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            db.ResetCounter(id);
            return Ok();
        }
    }
}
