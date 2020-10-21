using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IFileImageService _fsContext;

        public ImagesController(IFileImageService fsContext)
        {
            _fsContext = fsContext;
        }

        #region Image load routing

        // загрузить картинку
        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> AddImage(IFormFile uploadedImage)
        {
            if (uploadedImage != null)
            {
                // Определение пути 
                string path = _fsContext.GetFSImagesPath() + uploadedImage.FileName;

                // удаление файла картинки с сервера
                _fsContext.DeleteImage(uploadedImage.FileName);

                // Сохранение файла на сервере
                await _fsContext.AddImage(uploadedImage);
                return new JsonResult(path);
            }
            return new JsonResult("");
        }

        // получить ссылки всех картинок
        [HttpGet]
        public IActionResult GetImages()
        {
            string[] images = _fsContext.GetImagesFilesList();
            return new JsonResult(images);
        }


        // удалить картинку
        // получить ссылки всех картинок
        [HttpDelete]
        public IActionResult DeleteImage(string fileName)
        {
            _fsContext.DeleteImage(fileName);
            return Ok();
        }

        [HttpDelete("DeleteAllImages")]
        public IActionResult DeleteAllImages()
        {
            _fsContext.DeleteAllImages();
            return Ok();
        }

        #endregion

    }
}
