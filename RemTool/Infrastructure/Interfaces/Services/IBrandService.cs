using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

// интерфейс для сервиса - управление брендами
namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IBrandService
    {
        // синхронные методы
        public IEnumerable<Brand> GetAllBrands();

        #region CRUD

        public void CreateBrand(Brand brand);

        public Brand ReadBrand(string id);

        public void UpdateBrand(Brand brand);

        public void DeleteBrand(string id);

        #endregion

        public void DeleteAllBrands();


        // асинхронные методы
        //public Task<IEnumerable<Brand>> GetAllBrandsAsync();

        //#region CRUD

        //public Task CreateBrandAsync(Brand brand);

        //public Task<Brand> ReadBrandAsync(string id);

        //public Task UpdateBrandAsync(Brand brand);

        //public Task DeleteBrandAsync(string id);

        //#endregion

        //public Task DeleteAllBrandsAsync();
    }
}
