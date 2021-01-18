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
                Name = toolType.Name,
                RefId = toolType.Id,
                Categories = toolType.MainType,
                Services = toolType.Serves
            };

            List<string> keyWords = new List<string>();   

            // Add All Key Words
            // add name
            //keyWords.Add(toolType.Name.ToUpper()); // ToolType.Name

            // add serves
            foreach(var serve in toolType.Serves)
            {
                keyWords.Add(serve.ToUpper() + " ");   // ToolTypes.Serves
            }

            tts.KeyWords = keyWords.ToArray();

            _toolTypesSearch.InsertOne(tts);           
        }

        public async Task CreateToolTypeSearchAsync(ToolType toolType)
        {
            var tts = new ToolTypeSearch
            {
                Name = toolType.Name,
                RefId = toolType.Id,
                Categories = toolType.MainType,
                Services = toolType.Serves                
            };


            List<string> keyWords = new List<string>();

            // Add All Key Words
            // add name
            //keyWords.Add(toolType.Name.ToUpper()); // ToolType.Name

            // add serves
            foreach(var serve in toolType.Serves)
            {
                keyWords.Add(serve.ToUpper() + " ");   // ToolTypes.Serves
            }

            tts.KeyWords = keyWords.ToArray();

            await _toolTypesSearch.InsertOneAsync(tts);   
        }

        public void CreateToolTypeSearch(string toolTypeName)
        {
            ToolType toolType = _toolTypes.Find(toolType => toolType.Name.Equals(toolTypeName)).FirstOrDefault();

            var tts = new ToolTypeSearch
            {
                Name = toolType.Name,
                RefId = toolType.Id,
                Categories = toolType.MainType,
                Services = toolType.Serves
            };

            List<string> keyWords = new List<string>();

            // Add All Key Words
            // add name
            //keyWords.Add(toolType.Name.ToUpper()); // ToolType.Name

            // add serves
            foreach(var serve in toolType.Serves)
            {
                keyWords.Add(serve.ToUpper());   // ToolTypes.Serves
            }

            tts.KeyWords = keyWords.ToArray();

            _toolTypesSearch.InsertOne(tts);     
        }

        public async Task CreateToolTypeSearchAsync(string toolTypeName)
        {
            ToolType toolType = await _toolTypes.Find(toolType => toolType.Name.Equals(toolTypeName)).FirstOrDefaultAsync();

            var tts = new ToolTypeSearch
            {
                Name = toolType.Name,
                RefId = toolType.Id,
                Categories = toolType.MainType,
                Services = toolType.Serves
            };

            List<string> keyWords = new List<string>();   

            // Add All Key Words
            // add name
            //keyWords.Add(toolType.Name.ToUpper()); // ToolType.Name

            // add serves
            foreach(var serve in toolType.Serves)
            {
                keyWords.Add(serve.ToUpper());   // ToolTypes.Serves
            }

            tts.KeyWords = keyWords.ToArray();

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
            var upperUserInput = userInput.ToUpper();
            var allToolTypesSearch = ReadAllToolTypeSearch();

            SearchResults searchResults = new SearchResults();

            foreach(var oneToolTypeSearch in allToolTypesSearch)
            {
                // Если пользовательский ввод совпадает с именем типа инструмента
                var upperName = oneToolTypeSearch.Name.ToUpper();
                if (upperName.IndexOf(upperUserInput) >= 0 || upperName.Equals(upperUserInput))
                {
                    if (!searchResults.IncludedTypes.Contains(oneToolTypeSearch.Name))
                    {
                        searchResults.IncludedTypes.Add(oneToolTypeSearch.Name);
                        searchResults.IncludedIds.Add(oneToolTypeSearch.RefId);
                        searchResults.IncludedCategories.Add(oneToolTypeSearch.Categories);
                        searchResults.IncludedServices.Add("_");
                    }
                }
                // если пользовательский ввод совпадает со услугой вутри инструмента
                else
                {
                    foreach (var service in oneToolTypeSearch.Services)
                    {
                        var serviceUpper = service.ToUpper();
                        if (serviceUpper.IndexOf(upperUserInput) >= 0 || serviceUpper.Equals(upperUserInput))
                        {
                            if (!searchResults.IncludedTypes.Contains(oneToolTypeSearch.Name))
                            {
                                searchResults.IncludedTypes.Add(oneToolTypeSearch.Name);
                                searchResults.IncludedIds.Add(oneToolTypeSearch.RefId);
                                searchResults.IncludedCategories.Add(oneToolTypeSearch.Categories);
                                searchResults.IncludedServices.Add(service);
                            }
                            else
                            {
                                var last = searchResults.IncludedServices.Last();
                                var added = last + "," + service;
                                searchResults.IncludedServices.Remove(last);
                                searchResults.IncludedServices.Add(added);
                            }
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

            return JsonSerializer.Serialize(searchResults, options);
        }

        public async Task<string> SearchAsync(string userInput)
        {
            var upperUserInput = userInput.ToUpper();
            var allToolTypesSearch = await ReadAllToolTypeSearchAsync();

            SearchResults searchResults = new SearchResults();

            foreach(var oneToolTypeSearch in allToolTypesSearch)
            {
                // Если пользовательский ввод совпадает с именем типа инструмента
                var upperName = oneToolTypeSearch.Name.ToUpper();
                if (upperName.IndexOf(upperUserInput) >= 0 || upperName.Equals(upperUserInput))
                {
                    if (!searchResults.IncludedTypes.Contains(oneToolTypeSearch.Name))
                    {
                        searchResults.IncludedTypes.Add(oneToolTypeSearch.Name);
                        searchResults.IncludedIds.Add(oneToolTypeSearch.RefId);
                        searchResults.IncludedCategories.Add(oneToolTypeSearch.Categories);
                        searchResults.IncludedServices.Add("_");
                    }
                }
                // если пользовательский ввод совпадает со услугой вутри инструмента
                else
                {
                    foreach (var service in oneToolTypeSearch.Services)
                    {
                        var serviceUpper = service.ToUpper();
                        if (serviceUpper.IndexOf(upperUserInput) >= 0 || serviceUpper.Equals(upperUserInput))
                        {
                            if (!searchResults.IncludedTypes.Contains(oneToolTypeSearch.Name))
                            {
                                searchResults.IncludedTypes.Add(oneToolTypeSearch.Name);
                                searchResults.IncludedIds.Add(oneToolTypeSearch.RefId);
                                searchResults.IncludedCategories.Add(oneToolTypeSearch.Categories);
                                searchResults.IncludedServices.Add(service);
                            }
                            else
                            {
                                var last = searchResults.IncludedServices.Last();
                                var added = last + "," + service;
                                searchResults.IncludedServices.Remove(last);
                                searchResults.IncludedServices.Add(added);
                            }
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

            return JsonSerializer.Serialize(searchResults, options);
        }

        public void DeleteAllToolTypeSearch()
        {
            _toolTypesSearch.DeleteMany(new BsonDocument());
        }
    }

    public class SearchResults
    {
        public SearchResults()
        {
            IncludedTypes = new List<string>();
            IncludedIds = new List<string>();
            IncludedCategories = new List<bool[]>();
            IncludedServices = new List<string>();
        }
        public List<string> IncludedTypes { get; set; }
        public List<string> IncludedIds { get; set; }
        public List<bool[]> IncludedCategories { get; set; }
        public List<string> IncludedServices { get; set; }
        
    }
}