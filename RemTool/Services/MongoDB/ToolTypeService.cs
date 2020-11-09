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
            ToolTypesList ttl = new ToolTypesList();
            foreach (var tt in GetAllToolTypes().ToList())
            {
                if (tt.MainType[mainType] == true)
                {
                    ttl.IncludedTypes.Add(tt.Name);
                }
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

        public string GetElectroToolsList() => GetToolTypeList(0);

        public string GetFuelToolsList() => GetToolTypeList(1);

        public string GetWeldingToolsList() => GetToolTypeList(2);

        public string GetGeneratorsList() => GetToolTypeList(3);

        public string GetCompressorsList() => GetToolTypeList(4);

        public string GetRestToolsList() => GetToolTypeList(5);

        public string GetGardenToolsList() => GetToolTypeList(6);

        public string GetHeatGunsList() => GetToolTypeList(7);



        // получит прайс-лист по Id (Id - это и основной тип и второстепенный)
        public string GetPriceListOfToolType(string ToolTypeId)
        {
            var toolType = ReadToolType(ToolTypeId);

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

        public string GetPriceListOfToolTypeByName(string Name)
        {
            var fb = Builders<ToolType>.Filter;

            FilterDefinition<ToolType> f =
                fb.Eq("Name", Name);

            var toolType = _toolTypes.Find(f).FirstOrDefault();


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
            ToolTypesList ttl = new ToolTypesList();
            var ttFiltered = await GetAllToolTypesAsync();
            foreach (var tt in ttFiltered)
            {
                if (tt.MainType[mainType] == true)
                {
                    ttl.IncludedTypes.Add(tt.Name);
                }
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

        public Task<string> GetElectroToolsListAsync() => GetToolTypeListAsync(0);

        public Task<string> GetFuelToolsListAsync() => GetToolTypeListAsync(1);

        public Task<string> GetWeldingToolsListAsync() => GetToolTypeListAsync(2);

        public Task<string> GetGeneratorsListAsync() => GetToolTypeListAsync(3);

        public Task<string> GetCompressorsListAsync() => GetToolTypeListAsync(4);

        public Task<string> GetRestToolsListAsync() => GetToolTypeListAsync(5);

        public Task<string> GetGardenToolsListAsync() => GetToolTypeListAsync(6);

        public Task<string> GetHeatGunsListAsync() => GetToolTypeListAsync(7);



        public async Task<string> GetPriceListOfToolTypeAsync(string ToolTypeId)
        {
            var toolType = await ReadToolTypeAsync(ToolTypeId);

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

        public async Task<string> GetPriceListOfToolTypeByNameAsync(string Name)
        {
            var fb = Builders<ToolType>.Filter;

            FilterDefinition<ToolType> f =
                fb.Eq("Name", Name);

            var tt = await _toolTypes.Find(f).FirstOrDefaultAsync();

             Dictionary<string, string> toolTypePriceList = new Dictionary<string, string>();
            for (int i = 0; i < tt.Serves.Length; i++)
            {
                toolTypePriceList.Add(tt.Serves[i], tt.Costs[i]);
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
