using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IToolTypeService
    {
        // синхронные методы

        #region Base_Sync
        // получить все типы инструментов
        public IEnumerable<ToolType> GetAllToolTypes();

        // получить список электро-инструментов
        public string GetElectroToolsList();

        // получить список бензо-инструментов
        public string GetFuelToolsList();

        // получить список сварочных аппаратов
        public string GetWeldingToolsList();

        // получить список генераторов
        public string GetGeneratorsList();

        // получить список компрессоров
        public string GetCompressorsList();

        // получить список техники-для-отдыха
        public string GetRestToolsList();

        // получить список садовой-техники
        public string GetGardenToolsList();

        // получить список тепловых-пушек
        public string GetHeatGunsList();

        // получить прайслист
        public string GetPriceListOfToolType(string ToolTypeId);

        public string GetPriceListOfToolTypeByName(string Name);

        public string GetPriceListOfToolTypeByFilter(string filter);
        #endregion


        #region CRUD_Sync

        public void CreateToolType(ToolType toolType);

        public ToolType ReadToolType(string id);

        public void UpdateToolType(ToolType toolType);

        public void DeleteToolType(string id);

        public void DeleteAllToolTypes();
        #endregion




        // асинхронные методы

        #region Base_Async
        // получить все типы инструментов
        public Task<IEnumerable<ToolType>> GetAllToolTypesAsync();

        // получить список электро-инструментов
        public Task<string> GetElectroToolsListAsync();

        // получить список бензо-инструментов
        public Task<string> GetFuelToolsListAsync();

        // получить список сварочных аппаратов
        public Task<string> GetWeldingToolsListAsync();

        // получить список генераторов
        public Task<string> GetGeneratorsListAsync();

        // получить список компрессоров
        public Task<string> GetCompressorsListAsync();

        // получить список техники-для-отдыха
        public Task<string> GetRestToolsListAsync();

        // получить список садовой-техники
        public Task<string> GetGardenToolsListAsync();

        // получить список тепловых-пушек
        public Task<string> GetHeatGunsListAsync();

        // получить прайслист
        public Task<string> GetPriceListOfToolTypeAsync(string ToolTypeId);

        public Task<string> GetPriceListOfToolTypeByNameAsync(string Name);

        public Task<string> GetPriceListOfToolTypeByFilterAsync(string filter);
        #endregion

        #region CRUD_Async
        public Task CreateToolTypeAsync(ToolType toolType);

        public Task<ToolType> ReadToolTypeAsync(string id);

        public Task UpdateToolTypeAsync(ToolType toolType);

        public Task DeleteToolTypeAsync(string id);

        public Task DeleteAllToolTypesAsync();
        #endregion
    }
}
