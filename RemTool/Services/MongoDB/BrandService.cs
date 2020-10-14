using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using RemTool.Infrastructure.Additional;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Models;

namespace RemTool.Services.MongoDB
{
    public class BrandService : IBrandService
    {
        private readonly IMongoCollection<Brand> _brands;

        public BrandService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _brands = database.GetCollection<Brand>("Brands");
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _brands.Find(new BsonDocument()).ToList();
        }

        public void CreateBrand(Brand brand)
        {
            _brands.InsertOne(brand);
        }
        public Brand ReadBrand(string id)
        {
            return _brands.Find(brand => brand.Id == id).FirstOrDefault();
        }

        public void UpdateBrand(Brand brand)
        {
            _brands.ReplaceOne(b => b.Id == brand.Id, brand);
        }

        public void DeleteBrand(string id)
        {
            _brands.DeleteOne(brand => brand.Id == id);
        }

        public void DeleteAllBrands()
        {
            throw new NotImplementedException();
        }
    }
}
