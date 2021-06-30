using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Infrastructure.Additional;

using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Unicode;


namespace RemTool.Services.MongoDB
{
    public class MetaDataService : IMetaDataService
    {
        private readonly IMongoCollection<MetaData> _rtMetaData;

        public MetaDataService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _rtMetaData = database.GetCollection<MetaData>("RtMetaData");
        }

        #region CRUD_Sync
        public void CreateMetaData(MetaData md)
        {
            _rtMetaData.InsertOne(md);
        }
        public MetaData ReadMetaData()
        {
            return _rtMetaData.Find(new BsonDocument()).FirstOrDefault();
        }

        public void UpdateMetaData(MetaData newMd)
        {
            DeleteMetaData();
            CreateMetaData(newMd);
        }

        public void DeleteMetaData()
        {
            _rtMetaData.DeleteMany(new BsonDocument());
        }
        #endregion

        public async Task<MetaData> ReadMetaDataAsync()
        {
            return await _rtMetaData.Find(new BsonDocument()).FirstOrDefaultAsync();
        }
    }
}
