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
        public IEnumerable<SparePart> GetAllSpareParts();

        #region CRUD

        public void CreateSparePart(SparePart sparePart);

        public SparePart ReadSparePart(string id);

        public void UpdateSparePart(SparePart sparePart);

        public void DeleteSparePart(string id);

        #endregion

        public void DeleteAllSpareParts();


        // асинхронные методы
        //public Task<IEnumerable<SparePart>> GetAllSparePartsAsync();

        //#region CRUD

        //public Task CreateSparePartAsync(SparePart sparePart);

        //public Task<SparePart> ReadSparePartAsync(string id);

        //public Task UpdateSparePartAsync(SparePart sparePart);

        //public Task DeleteSparePartAsync(string id);

        //#endregion

        //public Task DeleteAllSparePartsAsync();
    }
}
