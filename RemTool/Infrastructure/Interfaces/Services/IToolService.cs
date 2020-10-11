using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;


// интерфейс для сервиса - управление инструментами
namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IToolService
    {
        // синхронные методы
        public IEnumerable<Tool> GetAllTools();

        #region CRUD

        public void CreateTool(Tool tool);

        public Tool ReadTool(int id);

        public void UpdateTool(Tool tool);

        public void DeleteTool(int id);

        #endregion

        public void DeleteAllTools();


        // асинхронные методы
        //public Task<IEnumerable<Tool>> GetAllToolsAsync();

        //#region CRUD

        //public Task CreateToolAsync(Tool tool);

        //public Task<Tool> ReadToolAsync(int id);

        //public Task UpdateToolAsync(Tool tool);

        //public Task DeleteToolAsync(int id);

        //#endregion

        //public Task DeleteAllToolsAsync();
    }
}
