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
    public class ToolTypeSearchService : IToolTypeSearchService
    {
        private readonly IMongoCollection<ToolTypeSearch> _toolTypesSearch;
        private readonly IMongoCollection<ToolType> _toolTypes;

        public ToolTypeSearchService(IRemToolMongoDBsettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var dataBase = client.GetDatabase(settings.DatabaseName);

            _toolTypes = dataBase.GetCollection<ToolType>("ToolTypes");
            _toolTypesSearch = dataBase.GetCollection<ToolTypeSearch>("ToolTypesSearch");
        }

        public void CreateToolTypeSearch(ToolTypeSearch toolTypeSearch)
        {
            _toolTypesSearch.InsertOne(toolTypeSearch);
        }

        public async Task CreateToolTypeSearchAsync(ToolTypeSearch toolTypeSearch)
        {
            await _toolTypesSearch.InsertOneAsync(toolTypeSearch);
        }

        public void CreateToolTypeSearch(ToolType toolType)
        {
            var tts = new ToolTypeSearch
            {
                Name = toolType.Name    
            };

            string[] mainTypes = new string[]
            {
                "Электроинструмент",
                "Бензоинструмент",
                "Садовая техника",
                "Компрессоры",
                "Генераторы",
                "Сварочная техника",
                "Тепловые пушки",
                "Техника для отдыха"
            };

            List<string> ttsKeyWords = new List<string>();   

            // Add All Key Words
            ttsKeyWords.Add(toolType.Name); // ToolType.Name

            for (int i = 0; i < mainTypes.Length; i++)
            {
                if (toolType.MainType[i] == true)
                {
                    ttsKeyWords.Add(mainTypes[i] + " ");  // ToolType.MainType
                }
            }

            foreach(var brand in toolType.Brands)
            {
                ttsKeyWords.Add(brand + " ");   // ToolType.Brands
            }

            foreach(var serve in toolType.Serves)
            {
                ttsKeyWords.Add(serve + " ");   // ToolTypes.Serves
            }

            tts.KeyWords = ttsKeyWords.ToArray();

            _toolTypesSearch.InsertOne(tts);           
        }

        public async Task CreateToolTypeSearchAsync(ToolType toolType)
        {
            var tts = new ToolTypeSearch
            {
                Name = toolType.Name    
            };

            string[] mainTypes = new string[]
            {
                "Электроинструмент",
                "Бензоинструмент",
                "Садовая техника",
                "Компрессоры",
                "Генераторы",
                "Сварочная техника",
                "Тепловые пушки",
                "Техника для отдыха"
            };

            List<string> ttsKeyWords = new List<string>();   

            // Add All Key Words
            ttsKeyWords.Add(toolType.Name); // ToolType.Name

            for (int i = 0; i < mainTypes.Length; i++)
            {
                if (toolType.MainType[i] == true)
                {
                    ttsKeyWords.Add(mainTypes[i] + " ");  // ToolType.MainType
                }
            }

            foreach(var brand in toolType.Brands)
            {
                ttsKeyWords.Add(brand + " ");   // ToolType.Brands
            }

            foreach(var serve in toolType.Serves)
            {
                ttsKeyWords.Add(serve + " ");   // ToolTypes.Serves
            }

            tts.KeyWords = ttsKeyWords.ToArray();

            await _toolTypesSearch.InsertOneAsync(tts);   
        }

        public void CreateToolTypeSearch(string toolTypeName)
        {
            ToolType tt = _toolTypes.Find(toolType => toolType.Name.Equals(toolTypeName)).FirstOrDefault();

            var tts = new ToolTypeSearch
            {
                Name = tt.Name    
            };

            string[] mainTypes = new string[]
            {
                "Электроинструмент",
                "Бензоинструмент",
                "Садовая техника",
                "Компрессоры",
                "Генераторы",
                "Сварочная техника",
                "Тепловые пушки",
                "Техника для отдыха"
            };

            List<string> ttsKeyWords = new List<string>();   

            // Add All Key Words
            ttsKeyWords.Add(tt.Name); // ToolType.Name

            for (int i = 0; i < mainTypes.Length; i++)
            {
                if (tt.MainType[i] == true)
                {
                    ttsKeyWords.Add(mainTypes[i] + " ");  // ToolType.MainType
                }
            }

            foreach(var brand in tt.Brands)
            {
                ttsKeyWords.Add(brand + " ");   // ToolType.Brands
            }

            foreach(var serve in tt.Serves)
            {
                ttsKeyWords.Add(serve + " ");   // ToolTypes.Serves
            }

            tts.KeyWords = ttsKeyWords.ToArray();

            _toolTypesSearch.InsertOne(tts);     
        }

        public async Task CreateToolTypeSearchAsync(string toolTypeName)
        {
            ToolType tt = await _toolTypes.Find(toolType => toolType.Name.Equals(toolTypeName)).FirstOrDefaultAsync();

            var tts = new ToolTypeSearch
            {
                Name = tt.Name    
            };

            string[] mainTypes = new string[]
            {
                "Электроинструмент",
                "Бензоинструмент",
                "Садовая техника",
                "Компрессоры",
                "Генераторы",
                "Сварочная техника",
                "Тепловые пушки",
                "Техника для отдыха"
            };

            List<string> ttsKeyWords = new List<string>();   

            // Add All Key Words
            ttsKeyWords.Add(tt.Name); // ToolType.Name

            for (int i = 0; i < mainTypes.Length; i++)
            {
                if (tt.MainType[i] == true)
                {
                    ttsKeyWords.Add(mainTypes[i] + " ");  // ToolType.MainType
                }
            }

            foreach(var brand in tt.Brands)
            {
                ttsKeyWords.Add(brand + " ");   // ToolType.Brands
            }

            foreach(var serve in tt.Serves)
            {
                ttsKeyWords.Add(serve + " ");   // ToolTypes.Serves
            }

            tts.KeyWords = ttsKeyWords.ToArray();

            await _toolTypesSearch.InsertOneAsync(tts);   
        }

        public ToolTypeSearch ReadToolTypeSearchByName(string name)
        {
            return _toolTypesSearch.Find(tts => tts.Name.Equals(name)).FirstOrDefault();
        }

        public async Task<ToolTypeSearch> ReadToolTypeSearchByNameAsync(string name)
        {
            return await _toolTypesSearch.Find(tts => tts.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public IEnumerable<ToolTypeSearch> ReadAllToolTypeSearch()
        {
            return _toolTypesSearch.Find(new BsonDocument()).ToList();
        }

        public async Task<IEnumerable<ToolTypeSearch>> ReadAllToolTypeSearchAsync()
        {
            return await _toolTypesSearch.Find(new BsonDocument()).ToListAsync();
        }

        public void UpdateToolTypeSearch(ToolTypeSearch toolTypeSearch)
        {
            _toolTypesSearch.ReplaceOne(tts => tts.Id == toolTypeSearch.Id, toolTypeSearch);
        }

        public async Task UpdateToolTypeSearchAsync(ToolTypeSearch toolTypeSearch)
        {
            await _toolTypesSearch.ReplaceOneAsync(tts => tts.Id == toolTypeSearch.Id, toolTypeSearch);
        }


        public void DeleteToolTypeSearch(string name)
        {
            _toolTypesSearch.DeleteOne(tts => tts.Name.Equals(name));
        }

        public async Task DeleteToolTypeSearchAsync(string name)
        {
            await _toolTypesSearch.DeleteOneAsync(tts => tts.Name.Equals(name));
        }



        public string Search(string userInput)
        {
            var ttss = ReadAllToolTypeSearch();

            List<string> findedToolTypes = new List<string>();

            foreach(var tts in ttss)
            {
                // если пользовательский ввод - часть названия - уже добавить
                if(tts.Name.IndexOf(userInput) > 0 || tts.Name.Equals(userInput))
                {
                    findedToolTypes.Add(tts.Name);
                }
                else
                {
                    foreach(var word in tts.KeyWords)
                    {
                        if (word.IndexOf(userInput) > 0 || word.Equals(userInput))
                        {
                            findedToolTypes.Add(tts.Name);
                        }
                    }
                }
            }

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(findedToolTypes, options);
        }

        public async Task<string> SearchAsync(string userInput)
        {
            var ttss = await ReadAllToolTypeSearchAsync();

            List<string> findedToolTypes = new List<string>();

            foreach(var tts in ttss)
            {
                // если пользовательский ввод - часть названия - уже добавить
                if(tts.Name.IndexOf(userInput) > 0 || tts.Name.Equals(userInput))
                {
                    findedToolTypes.Add(tts.Name);
                }
                else
                {
                    foreach(var word in tts.KeyWords)
                    {
                        if (word.IndexOf(userInput) > 0 || word.Equals(userInput))
                        {
                            findedToolTypes.Add(tts.Name);
                        }
                    }
                }
            }

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //поможет с кодировкой
                WriteIndented = true
            };

            return JsonSerializer.Serialize(findedToolTypes, options);
        }

        public void DeleteAllToolTypeSearch()
        {
            _toolTypesSearch.DeleteMany(new BsonDocument());
        }
    }
}