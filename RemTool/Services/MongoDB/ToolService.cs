using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Driver;
using RemTool.Infrastructure.Additional;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace RemTool.Services.MongoDB
{
    public class ElectroToolsList
    {
        public ElectroToolsList()
        {
            ElectroTools = new List<string>();
        }
        public List<string> ElectroTools { get; set; }
    }

    public class FuelToolsList
    {
        public FuelToolsList()
        {
            FuelTools = new List<string>();
        }

        public List<string> FuelTools { get; set; }
    }

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


        // получить список Электро инструментов
        public string GetElectroList()
        {
            var fb = Builders<Tool>.Filter;

            FilterDefinition<Tool> f =
                fb.Eq("MainType", 1);

            ElectroToolsList ptl = new ElectroToolsList();
            foreach (var tool in _tools.Find(f).ToList())
            {
                ptl.ElectroTools.Add(tool.Name);
            }

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(ptl,options);
        }

        // получить список Бензо инструментов
        public string GetFuelList()
        {
            var fb = Builders<Tool>.Filter;

            FilterDefinition<Tool> f =
                fb.Eq("MainType", 2);

            FuelToolsList ftl = new FuelToolsList();
            foreach (var tool in _tools.Find(f).ToList())
            {
                ftl.FuelTools.Add(tool.Name);
            }

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(ftl, options);
        }
    }
}
