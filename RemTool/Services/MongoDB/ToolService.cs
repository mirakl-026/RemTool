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
    public class ToolService : IToolService
    {
        private readonly IMongoCollection<Tool> _tools;

        public ToolService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _tools = database.GetCollection<Tool>("Tools");
        }


        public IEnumerable<Tool> GetAllTools()
        {
            return _tools.Find(new BsonDocument()).ToList();
        }

        public void CreateTool(Tool tool)
        {
            _tools.InsertOne(tool);
        }

        public Tool ReadTool(string id)
        {
            return _tools.Find(tool => tool.Id == id).FirstOrDefault();
        }

        public void UpdateTool(Tool tool)
        {
            _tools.ReplaceOne(t => t.Id == tool.Id, tool);
        }

        public void DeleteTool(string id)
        {
            _tools.DeleteOne(tool => tool.Id == id);
        }

        public void DeleteAllTools()
        {
            throw new NotImplementedException();
        }
    }
}
