using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Infrastructure.Additional;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RemTool.Services.FileSystem
{
    public class BackUpService : IBackUpService
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private string ImagesPath = "/images/";
        private string BackUpTemp = "/backup/temp/";
        private string BackUpZip = "/backup/archive/";

        private IMongoCollection<ClickCounter> _counters;
        private IMongoCollection<RtRequest> _rtRequests;
        private IMongoCollection<SparePart> _spareParts;
        private IMongoCollection<ToolType> _toolTypes;
        private IMongoCollection<ToolTypeSearch> _toolTypesSearch;

        private string PathToImages;
        private string PathToBackUpTemp;
        private string PathToBackUpZip;


        public BackUpService(IRemToolMongoDBsettings settings, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

            // настройка путей
            PathToImages = _appEnvironment.WebRootPath + ImagesPath;
            PathToBackUpTemp = _appEnvironment.ContentRootPath + BackUpTemp;
            PathToBackUpZip = _appEnvironment.ContentRootPath + BackUpZip;



            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            // получил коллекции
            _counters = database.GetCollection<ClickCounter>("ClickCounters");
            _rtRequests = database.GetCollection<RtRequest>("RtRequests");
            _spareParts = database.GetCollection<SparePart>("SpareParts");
            _toolTypes = database.GetCollection<ToolType>("ToolTypes");
            _toolTypesSearch = database.GetCollection<ToolTypeSearch>("ToolTypesSearch");
        }

        public async Task SaveServerToZip()
        {
            // удостоверяемся что ключевые папки на месте, если нет - создаём
            CheckNCreateMainFolders();

            // чистка папки temp
            ClearTemp();

            // копируем файлы картинок в temp бэкапа
            // получение списка всех файлов
            foreach (var imgPath in GetAllImagesPaths())
            {
                // копирование файла картинки в папку BackUp сервера
                FileInfo fi = new FileInfo(imgPath);
                if (fi.Exists)
                {
                    fi.CopyTo(PathToBackUpTemp + "/images/" + fi.Name, true);
                }
            }

            // копирование коллекций с сериализацией в temp
            using (FileStream fs = new FileStream(PathToBackUpTemp + "/json/" + "counters.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, GetAllClickCounters());
            }
            using (FileStream fs = new FileStream(PathToBackUpTemp + "/json/" + "rtrequests.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, GetAllRtRequests());
            }
            using (FileStream fs = new FileStream(PathToBackUpTemp + "/json/" + "spareparts.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, GetAllSpareParts());
            }
            using (FileStream fs = new FileStream(PathToBackUpTemp + "/json/" + "toolTypes.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, GetAllToolTypes());
            }
            using (FileStream fs = new FileStream(PathToBackUpTemp + "/json/" + "toolTypesSearch.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, GetAllToolTypeSearches());
            }

            // пакуем в архив
            string sourceFolder = PathToBackUpTemp;

            string zipFile = PathToBackUpZip + "backup.zip";
            FileInfo fiZip = new FileInfo(PathToBackUpZip + "backup.zip");
            if (fiZip.Exists)
            {
                fiZip.Delete();
            }
            ZipFile.CreateFromDirectory(sourceFolder, zipFile);

            // чистка папки temp
            ClearTemp();
        }


        public async Task UnZipToServer()
        {
            //
            int checkResult = CheckNCreateMainFolders();
            if (checkResult == -1)
            {
                // папки с архивом не было, значит распаковывать нечего
                return;
            }

            // чистка папки temp
            ClearTemp();

            // распаковываем из архива
            string zipFile = PathToBackUpZip + "backup.zip";
            FileInfo fiZip = new FileInfo(zipFile);
            if (!fiZip.Exists)
            {
                // если архива нет - стоп
                return;
            }
            ZipFile.ExtractToDirectory(zipFile, PathToBackUpTemp);

            // получаем картинки и .json файлы 
            // перемещаем картинки из temp в wwwroot/images
            foreach (var imgFile in Directory.GetFiles(PathToBackUpTemp + "/images/"))
            {
                // копирование файла картинки в папку BackUp сервера
                FileInfo fi = new FileInfo(imgFile);
                if (fi.Exists)
                {
                    fi.CopyTo(_appEnvironment.WebRootPath + ImagesPath + fi.Name, true);
                }
            }

            // по json записываем данные в коллекции
            string jsonToolType = PathToBackUpTemp + "/json/" + "toolTypes.json";
            FileInfo fiTt = new FileInfo(jsonToolType);
            if (fiTt.Exists)
            {
                using (FileStream fs = new FileStream(jsonToolType, FileMode.OpenOrCreate))
                {
                    ToolType[] toolTypes = await JsonSerializer.DeserializeAsync<ToolType[]>(fs);
                    if (toolTypes != null)
                    {
                        foreach (var tt in toolTypes)
                        {
                            if ( _toolTypes.Find(tool => tool.Id == tt.Id).FirstOrDefault() == null)
                                await _toolTypes.InsertOneAsync(tt);
                        }
                    }
                }
            }

            string jsonCounter = PathToBackUpTemp + "/json/" + "counters.json";
            FileInfo fiCt = new FileInfo(jsonCounter);
            if (fiCt.Exists)
            {
                using (FileStream fs = new FileStream(jsonCounter, FileMode.OpenOrCreate))
                {
                    ClickCounter[] counters = await JsonSerializer.DeserializeAsync<ClickCounter[]>(fs);
                    if (counters != null)
                    {
                        foreach (var ct in counters)
                        {
                            if (_counters.Find(cnt => cnt.Id == ct.Id).FirstOrDefault() == null)
                                await _counters.InsertOneAsync(ct);
                        }
                    }
                }
            }

            string jsonRtReq = PathToBackUpTemp + "/json/" + "rtrequests.json";
            FileInfo fiRt = new FileInfo(jsonRtReq);
            if (fiRt.Exists)
            {
                using (FileStream fs = new FileStream(jsonRtReq, FileMode.OpenOrCreate))
                {
                    RtRequest[] requests = await JsonSerializer.DeserializeAsync<RtRequest[]>(fs);
                    if (requests != null)
                    {
                        foreach (var rt in requests)
                        {
                            if (_rtRequests.Find(rtReq => rtReq.Id == rt.Id).FirstOrDefault() == null)
                                await _rtRequests.InsertOneAsync(rt);
                        }
                    }
                }
            }

            string jsonSpareParts = PathToBackUpTemp + "/json/" + "spareparts.json";
            FileInfo fiSp = new FileInfo(jsonSpareParts);
            if (fiSp.Exists)
            {
                using (FileStream fs = new FileStream(jsonSpareParts, FileMode.OpenOrCreate))
                {
                    SparePart[] spareParts = await JsonSerializer.DeserializeAsync<SparePart[]>(fs);
                    if (spareParts != null)
                    {
                        foreach (var sp in spareParts)
                        {
                            if (_toolTypes.Find(spp => spp.Id == sp.Id).FirstOrDefault() == null)
                                await _spareParts.InsertOneAsync(sp);
                        }
                    }
                }
            }

            string jsonToolTypesSearch = PathToBackUpTemp + "/json/" + "toolTypesSearch.json";
            FileInfo fiTts = new FileInfo(jsonToolTypesSearch);
            if (fiTts.Exists)
            {
                using (FileStream fs = new FileStream(jsonToolTypesSearch, FileMode.OpenOrCreate))
                {
                    ToolTypeSearch[] ttss = await JsonSerializer.DeserializeAsync<ToolTypeSearch[]>(fs);
                    if (ttss != null)
                    {
                        foreach (var tts in ttss)
                        {
                            if (_toolTypesSearch.Find(tttss => tttss.Id == tts.Id).FirstOrDefault() == null)
                                await _toolTypesSearch.InsertOneAsync(tts);
                        }
                    }
                }
            }

            // чистим папку temp
            ClearTemp();
        }

        public async Task UnZipToServerWithHardReload()
        {
            //
            int checkResult = CheckNCreateMainFolders();
            if (checkResult == -1)
            {
                // папки с архивом не было, значит распаковывать нечего
                return;
            }

            // чистка папки temp
            ClearTemp();

            // распаковываем из архива
            string zipFile = PathToBackUpZip + "backup.zip";
            FileInfo fiZip = new FileInfo(zipFile);
            if (!fiZip.Exists)
            {
                // если архива почему-то нет - стоп
                return;
            }
            ZipFile.ExtractToDirectory(zipFile, PathToBackUpTemp);

            // получаем картинки и .json файлы 
            // перемещаем картинки из temp в wwwroot/images
            foreach (var imgFile in Directory.GetFiles(PathToBackUpTemp + "/images/"))
            {
                // копирование файла картинки в папку BackUp сервера
                FileInfo fi = new FileInfo(imgFile);
                if (fi.Exists)
                {
                    fi.CopyTo(_appEnvironment.WebRootPath + ImagesPath + fi.Name, true);
                }
            }

            // по json записываем данные в коллекции (предварительно стирая)
            // стереть всю коллекцию
            _toolTypes.DeleteMany(new BsonDocument());
            string jsonToolType = PathToBackUpTemp + "/json/" + "toolTypes.json";
            FileInfo fiTt = new FileInfo(jsonToolType);
            if (fiTt.Exists)
            {
                using (FileStream fs = new FileStream(jsonToolType, FileMode.OpenOrCreate))
                {
                    ToolType[] toolTypes = await JsonSerializer.DeserializeAsync<ToolType[]>(fs);
                    if (toolTypes != null)
                    {
                        // поочерёдно заполнить коллекцию
                        foreach (var tt in toolTypes)
                        {
                            await _toolTypes.InsertOneAsync(tt);
                        }
                    }
                }
            }

            // Counters
            // стереть всю коллекцию
            _counters.DeleteMany(new BsonDocument());
            string jsonCounter = PathToBackUpTemp + "/json/" + "counters.json";
            FileInfo fiCt = new FileInfo(jsonCounter);
            if (fiCt.Exists)
            {
                using (FileStream fs = new FileStream(jsonCounter, FileMode.OpenOrCreate))
                {
                    ClickCounter[] counters = await JsonSerializer.DeserializeAsync<ClickCounter[]>(fs);
                    if (counters != null)
                    {
                        foreach (var ct in counters)
                        {
                            await _counters.InsertOneAsync(ct);
                        }
                    }
                }
            }

            // rtRequest
            // стереть всю коллекцию
            _rtRequests.DeleteMany(new BsonDocument());
            string jsonRtReq = PathToBackUpTemp + "/json/" + "rtrequests.json";
            FileInfo fiRt = new FileInfo(jsonRtReq);
            if (fiRt.Exists)
            {
                using (FileStream fs = new FileStream(jsonRtReq, FileMode.OpenOrCreate))
                {
                    RtRequest[] requests = await JsonSerializer.DeserializeAsync<RtRequest[]>(fs);
                    if (requests != null)
                    {
                        foreach (var rt in requests)
                        {
                            await _rtRequests.InsertOneAsync(rt);
                        }
                    }
                }
            }

            // spareParts
            _spareParts.DeleteMany(new BsonDocument());
            string jsonSpareParts = PathToBackUpTemp + "/json/" + "spareparts.json";
            FileInfo fiSp = new FileInfo(jsonSpareParts);
            if (fiSp.Exists)
            {
                using (FileStream fs = new FileStream(jsonSpareParts, FileMode.OpenOrCreate))
                {
                    SparePart[] spareParts = await JsonSerializer.DeserializeAsync<SparePart[]>(fs);
                    if (spareParts != null)
                    {
                        foreach (var sp in spareParts)
                        {
                            await _spareParts.InsertOneAsync(sp);
                        }
                    }
                }
            }

            // toolTypesSearch
            _toolTypesSearch.DeleteMany(new BsonDocument());
            string jsonToolTypesSearch = PathToBackUpTemp + "/json/" + "toolTypesSearch.json";
            FileInfo fiTts = new FileInfo(jsonToolTypesSearch);
            if (fiTts.Exists)
            {
                using (FileStream fs = new FileStream(jsonToolTypesSearch, FileMode.OpenOrCreate))
                {
                    ToolTypeSearch[] ttss = await JsonSerializer.DeserializeAsync<ToolTypeSearch[]>(fs);
                    if (ttss != null)
                    {
                        foreach (var tts in ttss)
                        {
                            await _toolTypesSearch.InsertOneAsync(tts);
                        }
                    }
                }
            }

            // чистим папку temp
            ClearTemp();
        }

        public async Task UnZipToServerWithSoftReload()
        {
            //
            int checkResult = CheckNCreateMainFolders();
            if (checkResult == -1)
            {
                // папки с архивом не было, значит распаковывать нечего
                return;
            }

            // чистка папки temp
            ClearTemp();

            // распаковываем из архива
            string zipFile = PathToBackUpZip + "backup.zip";
            FileInfo fiZip = new FileInfo(zipFile);
            if (!fiZip.Exists)
            {
                // если архива почему-то нет - стоп
                return;
            }
            ZipFile.ExtractToDirectory(zipFile, PathToBackUpTemp);

            // получаем картинки и .json файлы 
            // перемещаем картинки из temp в wwwroot/images
            foreach (var imgFile in Directory.GetFiles(PathToBackUpTemp + "/images/"))
            {
                // копирование файла картинки в папку BackUp сервера
                FileInfo fi = new FileInfo(imgFile);
                if (fi.Exists)
                {
                    fi.CopyTo(_appEnvironment.WebRootPath + ImagesPath + fi.Name, true);
                }
            }

            // по json записываем данные в коллекции (предварительно стирая)
            string jsonToolType = PathToBackUpTemp + "/json/" + "toolTypes.json";
            FileInfo fiTt = new FileInfo(jsonToolType);
            if (fiTt.Exists)
            {
                using (FileStream fs = new FileStream(jsonToolType, FileMode.OpenOrCreate))
                {
                    ToolType[] toolTypes = await JsonSerializer.DeserializeAsync<ToolType[]>(fs);
                    if (toolTypes != null)
                    {
                        // стереть всю коллекцию (стирает только если, из бэкапа есть замена)
                        _toolTypes.DeleteMany(new BsonDocument());

                        // поочерёдно заполнить коллекцию
                        foreach (var tt in toolTypes)
                        {
                            await _toolTypes.InsertOneAsync(tt);
                        }
                    }
                }
            }

            // Counters
            // стереть всю коллекцию
            string jsonCounter = PathToBackUpTemp + "/json/" + "counters.json";
            FileInfo fiCt = new FileInfo(jsonCounter);
            if (fiCt.Exists)
            {
                using (FileStream fs = new FileStream(jsonCounter, FileMode.OpenOrCreate))
                {
                    ClickCounter[] counters = await JsonSerializer.DeserializeAsync<ClickCounter[]>(fs);
                    if (counters != null)
                    {
                        // стереть всю коллекцию (стирает только если, из бэкапа есть замена)
                        _counters.DeleteMany(new BsonDocument());

                        foreach (var ct in counters)
                        {
                            await _counters.InsertOneAsync(ct);
                        }
                    }
                }
            }

            // rtRequest
            // стереть всю коллекцию
            string jsonRtReq = PathToBackUpTemp + "/json/" + "rtrequests.json";
            FileInfo fiRt = new FileInfo(jsonRtReq);
            if (fiRt.Exists)
            {
                using (FileStream fs = new FileStream(jsonRtReq, FileMode.OpenOrCreate))
                {
                    RtRequest[] requests = await JsonSerializer.DeserializeAsync<RtRequest[]>(fs);
                    if (requests != null)
                    {
                        // стереть всю коллекцию (стирает только если, из бэкапа есть замена)
                        _rtRequests.DeleteMany(new BsonDocument());

                        foreach (var rt in requests)
                        {
                            await _rtRequests.InsertOneAsync(rt);
                        }
                    }
                }
            }

            // spareParts
            string jsonSpareParts = PathToBackUpTemp + "/json/" + "spareparts.json";
            FileInfo fiSp = new FileInfo(jsonSpareParts);
            if (fiSp.Exists)
            {
                using (FileStream fs = new FileStream(jsonSpareParts, FileMode.OpenOrCreate))
                {
                    SparePart[] spareParts = await JsonSerializer.DeserializeAsync<SparePart[]>(fs);
                    if (spareParts != null)
                    {
                        // стереть всю коллекцию (стирает только если, из бэкапа есть замена)
                        _spareParts.DeleteMany(new BsonDocument());

                        foreach (var sp in spareParts)
                        {
                            await _spareParts.InsertOneAsync(sp);
                        }
                    }
                }
            }

            // toolTypesSearch
            string jsonToolTypesSearch = PathToBackUpTemp + "/json/" + "toolTypesSearch.json";
            FileInfo fiTts = new FileInfo(jsonToolTypesSearch);
            if (fiTts.Exists)
            {
                using (FileStream fs = new FileStream(jsonToolTypesSearch, FileMode.OpenOrCreate))
                {
                    ToolTypeSearch[] ttss = await JsonSerializer.DeserializeAsync<ToolTypeSearch[]>(fs);
                    if (ttss != null)
                    {
                        // стереть всю коллекцию (стирает только если, из бэкапа есть замена)
                        _toolTypesSearch.DeleteMany(new BsonDocument());

                        foreach (var tts in ttss)
                        {
                            await _toolTypesSearch.InsertOneAsync(tts);
                        }
                    }
                }
            }

            // чистим папку temp
            ClearTemp();
        }






        // метод очистки папки temp
        public void ClearTemp()
        {
            string[] tempFilesImages = Directory.GetFiles(PathToBackUpTemp + "/images/");
            foreach (var tempFile in tempFilesImages)
            {
                FileInfo fi = new FileInfo(tempFile);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }

            string[] tempFilesJson = Directory.GetFiles(PathToBackUpTemp + "/json/");
            foreach (var tempFile in tempFilesJson)
            {
                FileInfo fi = new FileInfo(tempFile);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
        }

        public int CheckNCreateMainFolders()
        {
            string pathToApp = _appEnvironment.ContentRootPath;

            // backUpFolder
            DirectoryInfo di_backUp = new DirectoryInfo(pathToApp + "/backup/");
            if (!di_backUp.Exists)
            {
                di_backUp.Create();
            }

            DirectoryInfo di_backUpTemp = new DirectoryInfo(pathToApp + "/backup/" + "/temp/");
            if (!di_backUpTemp.Exists)
            {
                di_backUpTemp.Create();
            }

            DirectoryInfo di_backUpTempImg = new DirectoryInfo(pathToApp + "/backup/" + "/temp/" + "/images/");
            if (!di_backUpTempImg.Exists)
            {
                di_backUpTempImg.Create();
            }

            DirectoryInfo di_backUpTempJson = new DirectoryInfo(pathToApp + "/backup/" + "/temp/" + "/json/");
            if (!di_backUpTempJson.Exists)
            {
                di_backUpTempJson.Create();
            }

            DirectoryInfo di_backUpArch = new DirectoryInfo(pathToApp + "/backup/" + "/archive/");
            if (!di_backUpArch.Exists)
            {
                di_backUpArch.Create();
                return -1;
            }
            return 0;
        }

        public IEnumerable<string> GetAllImagesPaths()
        {
            // получение списка всех файлов
            string[] filesPaths = Directory.GetFiles(PathToImages);
            return new List<string>(filesPaths);
        }

        public IEnumerable<ToolType> GetAllToolTypes()
        {
            return _toolTypes.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<ClickCounter> GetAllClickCounters()
        {
            return _counters.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<RtRequest> GetAllRtRequests()
        {
            return _rtRequests.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<SparePart> GetAllSpareParts()
        {
            return _spareParts.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<ToolTypeSearch> GetAllToolTypeSearches()
        {
            return _toolTypesSearch.Find(new BsonDocument()).ToList();
        }

        public async Task ReplaceBackupToNew(IFormFile newBackup)
        {
            FileInfo fiCurrentBackup = new FileInfo(PathToBackUpZip + "backup.zip");
            if (fiCurrentBackup.Exists)
            {
                fiCurrentBackup.Delete();
            }

            // Сохранение файла на сервере
            using (var fileStream = new FileStream(PathToBackUpZip + "backup.zip", FileMode.Create))
            {
                await newBackup.CopyToAsync(fileStream);
            }
        }

        public byte[] ReadBackupFromSystem()
        {
            byte[] backupFile;
            // чтение из файла
            using (FileStream fstream = File.OpenRead(PathToBackUpZip + "backup.zip"))
            {
                // преобразуем строку в байты
                backupFile = new byte[fstream.Length];
                // считываем данные
                fstream.Read(backupFile, 0, backupFile.Length);
            }

            return backupFile;
        }
    }
}
