using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IToolTypeSearchService _context;

        public SearchController(IToolTypeSearchService context)
        {
            _context = context;
        }

        [HttpGet("Find")]
        public async Task<string> Find(string userInput)
        {
            return await _context.SearchAsync(userInput);
        }
    }
}