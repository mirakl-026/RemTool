using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Services.SqlSE;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase, IBrandService
    {


        public void CreateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllBrands()
        {
            throw new NotImplementedException();
        }

        public void DeleteBrand(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            throw new NotImplementedException();
        }

        public Brand ReadBrand(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}
