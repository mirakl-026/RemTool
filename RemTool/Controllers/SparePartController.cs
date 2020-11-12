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
    public class SparePartController : ControllerBase
    {
        private ISparePartService db;

        public SparePartController(ISparePartService context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<SparePart>> Get()
        {
            return await db.GetAllSparePartsAsync();
        }

        [HttpGet("{id}")]
        public async Task<SparePart> Get(string id)
        {
            SparePart sparePart = await db.ReadSparePartAsync(id);
            return sparePart;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                db.CreateSparePart(sparePart);
                return Ok(sparePart);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put(SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                db.UpdateSparePart(sparePart);
                return Ok(sparePart);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            SparePart sparePart = db.ReadSparePart(id);
            if (sparePart != null)
            {
                db.DeleteSparePart(id);
            }
            return Ok(sparePart);
        }
    }
}
