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
        private readonly IToolTypeSearchService _dbs;

        public ToolTypeController(IToolTypeService context, IToolTypeSearchService contextS)
        {
            _db = context;
            _dbs = contextS;
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
                // добавил инструмент
                _db.CreateToolTypeAsync(toolType);

                // добавил инфу для поиска
                _dbs.CreateToolTypeSearchAsync(toolType);
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
                // удалили инфу для поиска
                //_dbs.DeleteToolTypeSearch(toolType.Name);
                _dbs.DeleteToolTypeSearchByRefId(toolType.Id);
                
                // обновили инструмент
                _db.UpdateToolType(toolType);

                // добавили инфу для поиска
                _dbs.CreateToolTypeSearch(toolType);

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
                //_dbs.DeleteToolTypeSearch(toolType.Name);
                _dbs.DeleteToolTypeSearchByRefId(toolType.Id);
                _db.DeleteToolType(id);
            }
            return Ok(toolType);
        }


        [HttpDelete("DeleteAllToolTypes")]
        [Authorize]
        public IActionResult DeleteAll()
        {
            // удалили все инструменты и всю инфу для поиска
            _db.DeleteAllToolTypes();
            _dbs.DeleteAllToolTypeSearch();
            return Ok();
        }

        [HttpPatch("RefreshAllToolTypesSearch")]
        [Authorize]
        public IActionResult RefreshAllToolTypesSearch()
        {
            _dbs.DeleteAllToolTypeSearch();

            IEnumerable<ToolType> allToolTypes = _db.GetAllToolTypes();
            foreach(var toolType in allToolTypes)
            {
                _dbs.CreateToolTypeSearch(toolType);
            }
            return Ok();
        }
    }
}
