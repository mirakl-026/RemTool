using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Services.SqlSE;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparePartController : ControllerBase
    {
        private ApplicationContext db;

        public SparePartController(ApplicationContext context)
        {
            db = context;
            if (!db.SpareParts.Any())
            {
                db.SpareParts.Add(new SparePart { Name = "Cooler", Description = "Cooler description" });
                db.SpareParts.Add(new SparePart { Name = "PowerSupply", Description = "PowerSupply description" });
                db.SpareParts.Add(new SparePart { Name = "Riser", Description = "Riser description" });
                db.SaveChanges();
            }
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
