using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces;
using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<string> GetElectroTools()
        {
            return await _db.GetElectroToolsListAsync();
        }

        [HttpGet("GetFuelTools")]
        public async Task<string> GetFuelTools()
        {
            return await _db.GetFuelToolsListAsync();
        }

        [HttpGet("GetWeldingTools")]
        public async Task<string> GetWeldingTools()
        {
            return await _db.GetWeldingToolsListAsync();
        }

        [HttpGet("GetGenerators")]
        public async Task<string> GetGenerators()
        {
            return await _db.GetGeneratorsListAsync();
        }

        [HttpGet("GetCompressors")]
        public async Task<string> GetCompressors()
        {
            return await _db.GetCompressorsListAsync();
        }

        [HttpGet("GetRestTools")]
        public async Task<string> GetRestTools()
        {
            return await _db.GetRestToolsListAsync();
        }

        [HttpGet("GetGardenTools")]
        public async Task<string> GetGardenTools()
        {
            return await _db.GetGardenToolsListAsync();
        }

        [HttpGet("GetHeatGuns")]
        public async Task<string> GetHeatGuns()
        {
            return await _db.GetHeatGunsListAsync();
        }

        [HttpGet("GetPriceList")]
        public async Task<string> GetPriceList(string id)
        {
            return await _db.GetPriceListOfToolTypeAsync(id);
        }

        [HttpGet("GetPriceListByName")]
        public async Task<string> GetPriceListByName(string name)
        {
            return await _db.GetPriceListOfToolTypeByNameAsync(name);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<ToolType>> Get()
        {
            var tools = await _db.GetAllToolTypesAsync();
            return tools;
        }

        [HttpGet("{id}")]
        public async Task<ToolType> Get(string id)
        {
            ToolType toolType = await _db.ReadToolTypeAsync(id);
            return toolType;
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public IActionResult DeleteAll()
        {
            _db.DeleteAllToolTypes();
            return Ok();
        }



    }
}
