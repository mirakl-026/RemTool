﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

// контроллер для управления счетчиками кликов

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClickCounterController : ControllerBase
    {
        private readonly IClickCounterService db;

        public ClickCounterController(IClickCounterService context)
        {
            db = context;
        }

        // получить значение счетчика по id
        // Get{id} - get counter value by counter id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<int> Get(string counterId)
        {
            ClickCounter clickCounter = await db.ReadClickCounterAsync(counterId);
            if (clickCounter != null)
            {
                return clickCounter.Count;
            }
            return 0;
        }

        // увеличить значение счётчика по id (ладе если счётчика нет - он создастся)
        // Put{id} - increase counter
        [HttpPut("{counterId}/{tooltypeId}")]
        [Authorize]
        public IActionResult Put(string counterId, string tooltypeId)
        {
            if (ModelState.IsValid)
            {
                db.IncreaseCounter(counterId, tooltypeId);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // сбросить значение счётчика по id
        // Delete{id} - reset counter by id
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string counterId)
        {
            db.ResetCounter(counterId);
            return Ok();
        }
    }
}
