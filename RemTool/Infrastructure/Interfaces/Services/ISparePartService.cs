using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

// интерфейс для сервиса - управление запчастями
namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface ISparePartService
    {
        // синхронные методы
        public List<Tool> GetAllSpareParts();

        #region CRUD

        public void CreateSparePart(SparePart sparePart);

        public SparePart ReadSparePart(int id);

        public void UpdateSparePart(SparePart sparePart);

        public void DeleteSparePart(int id);

        #endregion

        public void DeleteAllSpareParts();


        // асинхронные методы
        //public Task<List<SparePart>> GetAllSparePartsAsync();

        //#region CRUD

        //public Task CreateSparePartAsync(SparePart sparePart);

        //public Task<SparePart> ReadSparePartAsync(int id);

        //public Task UpdateSparePartAsync(SparePart sparePart);

        //public Task DeleteSparePartAsync(int id);

        //#endregion

        //public Task DeleteAllSparePartsAsync();
    }
}
