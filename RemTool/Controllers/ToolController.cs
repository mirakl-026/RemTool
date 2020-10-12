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
    [Route("api/tools")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private IToolService db;

        public ToolController(IToolService context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Tool> Get()
        {
            return db.GetAllTools();
        }

        [HttpGet("{id}")]
        public Tool Get(int id)
        {
            Tool tool = db.ReadTool(id);
            return tool;
        }

        [HttpPost]
        public IActionResult Post(Tool tool)
        {
            if (ModelState.IsValid)
            {
                db.CreateTool(tool);
                return Ok(tool);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(Tool tool)
        {
            if (ModelState.IsValid)
            {
                db.UpdateTool(tool);
                return Ok(tool);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Tool tool = db.ReadTool(id);
            if (tool != null)
            {
                db.DeleteTool(id);
            }
            return Ok(tool);
        }
    }
}
