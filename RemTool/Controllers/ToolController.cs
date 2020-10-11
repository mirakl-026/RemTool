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
    public class ToolController : ControllerBase, IToolService
    {



        public void CreateTool(Tool tool)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllTools()
        {
            throw new NotImplementedException();
        }

        public void DeleteTool(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tool> GetAllTools()
        {
            throw new NotImplementedException();
        }

        public Tool ReadTool(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTool(Tool tool)
        {
            throw new NotImplementedException();
        }
    }
}
