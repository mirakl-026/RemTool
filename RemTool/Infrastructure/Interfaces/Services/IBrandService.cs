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

        public Brand ReadBrand(int id);

        public void UpdateBrand(Brand brand);

        public void DeleteBrand(int id);

        #endregion

        public void DeleteAllBrands();


        // асинхронные методы
        //public Task<IEnumerable<Brand>> GetAllBrandsAsync();

        //#region CRUD

        //public Task CreateBrandAsync(Brand brand);

        //public Task<Brand> ReadBrandAsync(int id);

        //public Task UpdateBrandAsync(Brand brand);

        //public Task DeleteBrandAsync(int id);

        //#endregion

        //public Task DeleteAllBrandsAsync();
    }
}
