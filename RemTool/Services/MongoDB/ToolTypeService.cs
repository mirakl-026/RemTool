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
    public class ToolTypeService : IToolTypeService
    {
        private readonly IMongoCollection<ToolType> _toolTypes;

        public ToolTypeService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _toolTypes = database.GetCollection<ToolType>("ToolTypes");
        }


        #region CRUD+D
        public void CreateToolType(ToolType toolType)
        {
            _toolTypes.InsertOne(toolType);
        }

        public ToolType ReadToolType(string id)
        {
            return _toolTypes.Find(toolType => toolType.Id == id).FirstOrDefault();
        }

        public void UpdateToolType(ToolType toolType)
        {
            _toolTypes.ReplaceOne(t => t.Id == toolType.Id, toolType);
        }
        public void DeleteToolType(string id)
        {
            _toolTypes.DeleteOne(tool => tool.Id == id);
        }

        public void DeleteAllToolTypes()
        {
            _toolTypes.DeleteMany(new BsonDocument());
        }
        #endregion

        public IEnumerable<ToolType> GetAllToolTypes()
        {
            return _toolTypes.Find(new BsonDocument()).ToList();
        }

        // возвращает JSON список подтипов в основном типе
        // например 1 - ЭлектроИнструмент, вернёт - шуруповёрт, перфоратор и т.д.
        private string GetToolTypeList (int mainType)
        {
            var fb = Builders<ToolType>.Filter;

            FilterDefinition<ToolType> f =
                fb.Eq("MainType", mainType);

            ToolTypesList ttl = new ToolTypesList();
            foreach (var toolType in _toolTypes.Find(f).ToList())
            {
                ttl.IncludedTypes.Add(toolType.Name);
            }

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(ttl, options);
        }

        public string GetElectroToolsList() => GetToolTypeList(1);

        public string GetFuelToolsList() => GetToolTypeList(2);

        public string GetWeldingToolsList() => GetToolTypeList(3);

        public string GetGeneratorsList() => GetToolTypeList(4);

        public string GetCompressorsList() => GetToolTypeList(5);

        public string GetRestToolsList() => GetToolTypeList(6);

        public string GetGardenToolsList() => GetToolTypeList(7);

        public string GetHeatGunsList() => GetToolTypeList(8);



        // получит прайс-лист по Id (Id - это и основной тип и второстепенный)
        public string GetPriceListOfToolType(string ToolTypeId)
        {
            var toolType = ReadToolType(ToolTypeId);

            Dictionary<string, string> toolTypePriceList = toolType.ServeCost;

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(toolTypePriceList, options);
        }

        public string GetPriceListOfToolType(int mainType, int secondType)
        {
            var fb = Builders<ToolType>.Filter;

            FilterDefinition<ToolType> f =
                fb.Eq("MainType", mainType) &
                fb.Eq("SecondaryType", secondType);

            Dictionary<string, string> toolTypePriceList = _toolTypes.Find(f).FirstOrDefault().ServeCost;

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(toolTypePriceList, options);
        }

        public string GetPriceListOfToolTypeByFilter(string filter)
        {
            throw new NotImplementedException();
        }
    }




    public class ToolTypesList
    {
        public ToolTypesList() => IncludedTypes = new List<string>();
        public List<string> IncludedTypes { get; set; }
    }
}
