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
    public class SparePartController : ControllerBase, ISparePartService
    {


        public void CreateSparePart(SparePart sparePart)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllSpareParts()
        {
            throw new NotImplementedException();
        }

        public void DeleteSparePart(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tool> GetAllSpareParts()
        {
            throw new NotImplementedException();
        }

        public SparePart ReadSparePart(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateSparePart(SparePart sparePart)
        {
            throw new NotImplementedException();
        }
    }
}
