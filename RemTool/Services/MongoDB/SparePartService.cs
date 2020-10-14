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
    public class SparePartService : ISparePartService
    {
        private readonly IMongoCollection<SparePart> _spareParts;

        public SparePartService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _spareParts = database.GetCollection<SparePart>("SpareParts");
        }

        // синхроннные методы
        public IEnumerable<SparePart> GetAllSpareParts()
        {
            return _spareParts.Find(new BsonDocument()).ToList();
        }

        public void CreateSparePart(SparePart sparePart)
        {
            _spareParts.InsertOne(sparePart);
        }
        public SparePart ReadSparePart(string id)
        {
            return _spareParts.Find(sparePart => sparePart.Id == id).FirstOrDefault();
        }
        public void UpdateSparePart(SparePart sparePart)
        {
            _spareParts.ReplaceOne(sp => sp.Id == sparePart.Id, sparePart);
        }

        public void DeleteSparePart(string id)
        {
            _spareParts.DeleteOne(sp => sp.Id == id);
        }

        public void DeleteAllSpareParts()
        {
            throw new NotImplementedException();
        }
    }
}
