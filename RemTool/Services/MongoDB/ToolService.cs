using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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



        public void CreateTool(Tool tool)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllTools()
        {
            throw new NotImplementedException();
        }

        public void DeleteTool(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tool> GetAllTools()
        {
            throw new NotImplementedException();
        }

        public Tool ReadTool(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTool(Tool tool)
        {
            throw new NotImplementedException();
        }
    }
}
