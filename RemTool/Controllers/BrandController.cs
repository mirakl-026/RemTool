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
    public class BrandController : ControllerBase
    {
        private ApplicationContext db;

        public BrandController(ApplicationContext context)
        {
            db = context;
            if (!db.Brands.Any())
            {
                db.Brands.Add(new Brand { Name = "Caterpillar", Description = "Caterpillar description" });
                db.Brands.Add(new Brand { Name = "StroyBat", Description = "StroyBat description" });
                db.Brands.Add(new Brand { Name = "FastBuilder", Description = "FastBuilder description" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return db.GetAllBrands();
        }

        [HttpGet("{id}")]
        public Brand Get(int id)
        {
            Brand brand = db.ReadBrand(id);
            return brand;
        }

        [HttpPost]
        public IActionResult Post(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.CreateBrand(brand);
                return Ok(brand);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.UpdateBrand(brand);
                return Ok(brand);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Brand brand = db.ReadBrand(id);
            if (brand != null)
            {
                db.DeleteBrand(id);
            }
            return Ok(brand);
        }
    }
}
