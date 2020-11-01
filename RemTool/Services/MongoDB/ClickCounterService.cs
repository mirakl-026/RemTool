using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

using RemTool.Models;
using RemTool.Infrastructure.Additional;
using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Services.MongoDB
{
    public class ClickCounterService : IClickCounterService
    {
        private readonly IMongoCollection<ClickCounter> _counters;

        public ClickCounterService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _counters = database.GetCollection<ClickCounter>("ClickCounters");
        }



        #region CRUD

        public void CreateClickCounter(ClickCounter counter)
        {
            _counters.InsertOne(counter);
        }

        public ClickCounter ReadClickCounter(string id)
        {
            return _counters.Find(counter => counter.Id == id).FirstOrDefault();
        }

        public void UpdateClickCounter(ClickCounter counter)
        {
            _counters.ReplaceOne(ct => ct.Id == counter.Id, counter);
        }

        public void DeleteClickCounter(string id)
        {
            _counters.DeleteOne(ct => ct.Id == id);
        }

        #endregion


        public void IncreaseCounter(string counterId, string toolTypeId)
        {
            ClickCounter counter = ReadClickCounter(counterId);
            if (counter == null)
            {
                CreateClickCounter(new ClickCounter
                {
                    Id = counterId,
                    ToolTypeId = toolTypeId,
                    Count = 1
                });
            }
            else
            {
                counter.Count++;
                UpdateClickCounter(counter);
            }
        }

        public void ResetCounter(string counterId)
        {
            ClickCounter counter = ReadClickCounter(counterId);
            if (counter != null)
            {
                counter.Count = 0;
                UpdateClickCounter(counter);
            }
        }

        public void ResetAllCounters()
        {
            List<ClickCounter> allCounters = _counters.Find(new BsonDocument()).ToList();
            foreach (var counter in allCounters)
            {
                ResetCounter(counter.Id);
            }
        }

        public void DeleteAllCounters()
        {
            _counters.DeleteMany(new BsonDocument());
        }
    }
}
