using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces;
using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolTypeController : ControllerBase
    {
        private readonly IToolTypeService _db;

        public ToolTypeController(IToolTypeService context)
        {
            _db = context;
        }


        [HttpGet("GetElectroTools")]
        public string GetElectroTools()
        {
            return _db.GetElectroToolsList();
        }

        [HttpGet("GetFuelTools")]
        public string GetFuelTools()
        {
            return _db.GetFuelToolsList();
        }

        [HttpGet("GetWeldingTools")]
        public string GetWeldingTools()
        {
            return _db.GetWeldingToolsList();
        }

        [HttpGet("GetGenerators")]
        public string GetGenerators()
        {
            return _db.GetGeneratorsList();
        }

        [HttpGet("GetCompressors")]
        public string GetCompressors()
        {
            return _db.GetCompressorsList();
        }

        [HttpGet("GetRestTools")]
        public string GetRestTools()
        {
            return _db.GetRestToolsList();
        }

        [HttpGet("GetGardenTools")]
        public string GetGardenTools()
        {
            return _db.GetGardenToolsList();
        }

        [HttpGet("GetHeatGuns")]
        public string GetHeatGuns()
        {
            return _db.GetHeatGunsList();
        }


        [HttpGet]
        public IEnumerable<ToolType> Get()
        {
            return _db.GetAllToolTypes();
        }

        [HttpGet("{id}")]
        public ToolType Get(string id)
        {
            ToolType toolType = _db.ReadToolType(id);
            return toolType;
        }

        [HttpPost]
        public IActionResult Post(ToolType toolType)
        {
            if (ModelState.IsValid)
            {
                _db.CreateToolType(toolType);
                return Ok(toolType);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(ToolType toolType)
        {
            if (ModelState.IsValid)
            {
                _db.UpdateToolType(toolType);
                return Ok(toolType);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            ToolType toolType = _db.ReadToolType(id);
            if (toolType != null)
            {
                _db.DeleteToolType(id);
            }
            return Ok(toolType);
        }

        [HttpDelete("DeleteAllToolTypes")]
        public IActionResult DeleteAll()
        {
            _db.DeleteAllToolTypes();
            return Ok();
        }

    }
}
