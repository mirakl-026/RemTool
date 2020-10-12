using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.DAL.Context.SQLExpress;

namespace RemTool.Services.SQLExpress
{
    public class BrandService :IBrandService
    {
        private readonly RemToolContext _db;

        public BrandService(RemToolContext db) => _db = db;

        #region Brands implementation

        public IEnumerable<Brand> GetAllBrands()
        {
            return _db.Brands.ToList();
        }

        public void CreateBrand(Brand brand)
        {
            _db.Brands.Add(brand);
            _db.SaveChanges();
        }

        public Brand ReadBrand(int id)
        {
            Brand brand = _db.Brands.FirstOrDefault(x => x.Id == id);
            return brand;
        }

        public void UpdateBrand(Brand brand)
        {
            _db.Brands.Update(brand);
            _db.SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            Brand brand = _db.Brands.FirstOrDefault(x => x.Id == id);
            if (brand != null)
            {
                _db.Brands.Remove(brand);
                _db.SaveChanges();
            }
        }

        public void DeleteAllBrands()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
