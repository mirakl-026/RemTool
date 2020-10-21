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
        public string GetImagesRoot();

        // добавить файл картинки на сервер
        public Task AddImage(IFormFile newImage);

        // получить список картинок на сервере
        public string[] GetImages();

        // получить ссылку на картинку по имени
        public string GetImage(string fileName);

        // удалить картинку по имени файла
        public void DeleteImage(string fileName);

        // удалить все картинки
        public void DeleteAllImages();
    }
}
