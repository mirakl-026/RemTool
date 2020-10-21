using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Services.FileSystem
{
    public class FileImageService : IFileImageService
    {
        private string ImagesPath = "/images/";
        private string FullServerImagesPath;
        private IWebHostEnvironment _appEnvironment;

        public FileImageService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            FullServerImagesPath = _appEnvironment.WebRootPath + ImagesPath;
        }

        public async Task AddImage(IFormFile file)
        {
            // Определение пути 
            string path = FullServerImagesPath + file.FileName;

            // Сохранение файла на сервере
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public void DeleteImage(string fileName)
        {
            // Определение пути
            string path = FullServerImagesPath + fileName;

            // удаление файла картинки с сервера
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.Delete();
            }
        }


        public void DeleteAllImages()
        {
            // получение списка всех файлов
            string[] filesPaths = Directory.GetFiles(FullServerImagesPath);
            foreach (var filePath in filesPaths)
            {
                // удаление файла картинки с сервера
                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
        }


        public string GetFSImagesPath()
        {
            return ImagesPath;
        }

        public string[] GetImagesFilesList()
        {
            string[] images = Directory.GetFiles(FullServerImagesPath);
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = images[i].Substring(_appEnvironment.WebRootPath.Length);
            }
            return images;
        }

        //public string[] GetImagesFilesPathsList()
        //{
        //    string[] images = Directory.GetFiles(FullServerImagesPath);
        //    return images;
        //}
    }
}
