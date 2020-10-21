using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IFileImageService
    {
        // получить путь к папке с картинками
        public string GetFSImagesPath();

        // добавить файл картинки на сервер
        public Task AddImage(IFormFile file);

        // получить список картинок на сервере
        public string[] GetImagesFilesList();

        // получить список картинок на сервере (пути)
        //public string[] GetImagesFilesPathsList();

        // удалить картинку по имени файла
        public void DeleteImage(string fileName);

        // удалить все картинки
        public void DeleteAllImages();
    }
}
