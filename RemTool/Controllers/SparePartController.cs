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
    [Route("api/spareparts")]
    [ApiController]
    public class SparePartController : ControllerBase
    {
        private ISparePartService db;

        public SparePartController(ISparePartService context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<SparePart> Get()
        {
            return db.GetAllSpareParts();
        }

        [HttpGet("{id}")]
        public SparePart Get(int id)
        {
            SparePart sparePart = db.ReadSparePart(id);
            return sparePart;
        }

        [HttpPost]
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
        public IActionResult Delete(int id)
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
