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

            //int toolTypePriceListLength = toolType.Serves.Length;
            //string toolTypePriceList = "{";
            //for (int i = 0; i < toolTypePriceListLength; i++)
            //{
            //    toolTypePriceList += "\"" + toolType.Serves[i] + "\":";
            //    toolTypePriceList += "\"" + toolType.Costs[i] + "\",";
            //}
            //toolTypePriceList += "}";

            Dictionary<string, string> toolTypePriceList = new Dictionary<string, string>();
            for (int i = 0; i < toolType.Serves.Length; i++)
            {
                toolTypePriceList.Add(toolType.Serves[i], toolType.Costs[i]);
            }

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

            var toolType = _toolTypes.Find(f).FirstOrDefault();

            //int toolTypePriceListLength = toolType.Serves.Length;
            //string toolTypePriceList = "{";
            //for (int i = 0; i < toolTypePriceListLength; i++)
            //{
            //    toolTypePriceList += "\"" + toolType.Serves[i] + "\":";
            //    toolTypePriceList += "\"" + toolType.Costs[i] + "\",";
            //}
            //toolTypePriceList += "}";

            Dictionary<string, string> toolTypePriceList = new Dictionary<string, string>();
            for (int i = 0; i < toolType.Serves.Length; i++)
            {
                toolTypePriceList.Add(toolType.Serves[i], toolType.Costs[i]);
            }

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







        #region CRUD+D Async
        public async Task CreateToolTypeAsync(ToolType toolType)
        {
            await _toolTypes.InsertOneAsync(toolType);
        }

        public async Task<ToolType> ReadToolTypeAsync(string id)
        {
            return await _toolTypes.Find(toolType => toolType.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateToolTypeAsync(ToolType toolType)
        {
            await _toolTypes.ReplaceOneAsync(tt => tt.Id == toolType.Id, toolType);
        }

        public async Task DeleteToolTypeAsync(string id)
        {
            await _toolTypes.DeleteOneAsync(toolType => toolType.Id == id);
        }

        public async Task DeleteAllToolTypesAsync()
        {
            await _toolTypes.DeleteManyAsync(new BsonDocument());
        }
        #endregion


        public async Task<IEnumerable<ToolType>> GetAllToolTypesAsync()
        {
            return await _toolTypes.Find(new BsonDocument()).ToListAsync();
        }

        private async Task<string> GetToolTypeListAsync(int mainType)
        {
            var fb = Builders<ToolType>.Filter;

            FilterDefinition<ToolType> f =
                fb.Eq("MainType", mainType);

            ToolTypesList ttl = new ToolTypesList();
            var ttcol = await _toolTypes.Find(f).ToListAsync();
            foreach (var toolType in ttcol)
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

        public Task<string> GetElectroToolsListAsync() => GetToolTypeListAsync(1);

        public Task<string> GetFuelToolsListAsync() => GetToolTypeListAsync(2);

        public Task<string> GetWeldingToolsListAsync() => GetToolTypeListAsync(3);

        public Task<string> GetGeneratorsListAsync() => GetToolTypeListAsync(4);

        public Task<string> GetCompressorsListAsync() => GetToolTypeListAsync(5);

        public Task<string> GetRestToolsListAsync() => GetToolTypeListAsync(6);

        public Task<string> GetGardenToolsListAsync() => GetToolTypeListAsync(7);

        public Task<string> GetHeatGunsListAsync() => GetToolTypeListAsync(8);



        public async Task<string> GetPriceListOfToolTypeAsync(string ToolTypeId)
        {
            var toolType = await ReadToolTypeAsync(ToolTypeId);

            //int toolTypePriceListLength = toolType.Serves.Length;
            //string toolTypePriceList = "{";
            //for (int i = 0; i < toolTypePriceListLength; i++)
            //{
            //    toolTypePriceList += "\"" + toolType.Serves[i] + "\":";
            //    toolTypePriceList += "\"" + toolType.Costs[i] + "\",";
            //}
            //toolTypePriceList += "}";

            Dictionary<string, string> toolTypePriceList = new Dictionary<string, string>();
            for (int i = 0; i < toolType.Serves.Length; i++)
            {
                toolTypePriceList.Add(toolType.Serves[i], toolType.Costs[i]);
            }

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(toolTypePriceList, options);
        }

        public async Task<string> GetPriceListOfToolTypeAsync(int mainType, int secondType)
        {
            var fb = Builders<ToolType>.Filter;

            FilterDefinition<ToolType> f =
                fb.Eq("MainType", mainType) &
                fb.Eq("SecondaryType", secondType);

            var tt = await _toolTypes.Find(f).FirstOrDefaultAsync();

            //int toolTypePriceListLength = tt.Serves.Length;
            //string toolTypePriceList = "{";
            //for (int i = 0; i < toolTypePriceListLength; i++)
            //{
            //    toolTypePriceList += "\"" + tt.Serves[i] + "\":";
            //    toolTypePriceList += "\"" + tt.Costs[i] + "\",";
            //}
            //toolTypePriceList += "}";
            Dictionary<string, string> toolTypePriceList = new Dictionary<string, string>();
            for (int i = 0; i < toolType.Serves.Length; i++)
            {
                toolTypePriceList.Add(toolType.Serves[i], toolType.Costs[i]);
            }

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(toolTypePriceList, options);
        }

        public Task<string> GetPriceListOfToolTypeByFilterAsync(string filter)
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
