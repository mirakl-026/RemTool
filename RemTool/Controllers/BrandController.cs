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
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandService db;

        public BrandController(IBrandService context)
        {
            db = context;
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
