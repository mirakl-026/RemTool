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
                            await _spareParts.InsertOneAsync(sp);
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
    }
}
