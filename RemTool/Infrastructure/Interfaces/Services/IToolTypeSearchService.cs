using System.Collections.Generic;
using System.Threading.Tasks;

using RemTool.Models;

namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IToolTypeSearchService
    {
        public void CreateToolTypeSearch(ToolTypeSearch toolTypeSearch);
        public Task CreateToolTypeSearchAsync(ToolTypeSearch toolTypeSearch);

        public void CreateToolTypeSearch(ToolType toolType);
        public Task CreateToolTypeSearchAsync(ToolType toolType);

        public void CreateToolTypeSearch(string toolTypeName);
        public Task CreateToolTypeSearchAsync(string toolTypeName);


        public ToolTypeSearch ReadToolTypeSearchByName(string name);
        public Task<ToolTypeSearch> ReadToolTypeSearchByNameAsync(string name);

        public IEnumerable<ToolTypeSearch> ReadAllToolTypeSearch();

        public Task<IEnumerable<ToolTypeSearch>> ReadAllToolTypeSearchAsync();

        public void UpdateToolTypeSearch(ToolTypeSearch toolTypeSearch);

        public Task UpdateToolTypeSearchAsync(ToolTypeSearch toolTypeSearch);

        public void DeleteToolTypeSearch(string name);

        public Task DeleteToolTypeSearchAsync(string name);

        public void DeleteAllToolTypeSearch();

        public void DeleteToolTypeSearchByRefId(string refId);

        public Task DeleteToolTypeSearchByRefIdAsync(string refId);



        public string Search(string userInput);

        public Task<string> SearchAsync(string userInput);
    }   
}