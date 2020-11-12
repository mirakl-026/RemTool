using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

using RemTool.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace RemTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IFileImageService _fsContext;

        public ImagesController(IFileImageService fsContext)
        {
            _fsContext = fsContext;
        }

        // загрузить картинку
        [HttpPost("AddImage")]
        [RequestSizeLimit(52428800)]
        [Authorize]
        public async Task<IActionResult> AddImage(IFormFile newImage)
        {
            if (newImage != null)
            {
                // Определение пути 
                string path = _fsContext.GetImagesRoot() + newImage.FileName;

                // удаление файла картинки с сервера
                _fsContext.DeleteImage(newImage.FileName);

                // Сохранение файла на сервере
                await _fsContext.AddImage(newImage);
                return new JsonResult(path);
            }
            return new JsonResult("");
        }

        // получить ссылки всех картинок
        [HttpGet("GetImages")]
        public IActionResult GetImages()
        {
            string[] images = _fsContext.GetImages();
            return new JsonResult(images);
        }

        // получить ссылку по имени картинки
        [HttpGet("GetImage/{fileName}")]
        public string GetImage(string fileName)
        {
            return _fsContext.GetImage(fileName);
        }


        // удалить картинку
        // получить ссылки всех картинок
        [HttpDelete("DeleteImage/{fileName}")]
        [Authorize]
        public IActionResult DeleteImage(string fileName)
        {
            _fsContext.DeleteImage(fileName);
            return Ok();
        }

        [HttpDelete("DeleteAllImages")]
        [Authorize]
        public IActionResult DeleteAllImages()
        {
            _fsContext.DeleteAllImages();
            return Ok();
        }
    }
}
