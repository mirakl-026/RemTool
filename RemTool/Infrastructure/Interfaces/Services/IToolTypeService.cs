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

        public string GetPriceListOfToolType(int mainType, int secondType);

        public string GetPriceListOfToolTypeByFilter(string filter);




        #region CRUD

        public void CreateToolType(ToolType toolType);

        public ToolType ReadToolType(string id);

        public void UpdateToolType(ToolType toolType);

        public void DeleteToolType(string id);

        #endregion

        public void DeleteAllToolTypes();


        // асинхронные методы
        //public Task<IEnumerable<ToolType>> GetAllToolTypesAsync();

        //#region CRUD

        //public Task CreateToolTypeAsync(ToolType toolType);

        //public Task<ToolType> ReadToolTypeAsync(string id);

        //public Task UpdateToolTypeAsync(ToolType toolType);

        //public Task DeleteToolTypeAsync(string id);

        //#endregion

        //public Task DeleteAllToolTypesAsync();
    }
}
