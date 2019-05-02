using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GalleryStone.Data;
using GalleryStone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GalleryStone.Controllers
{
    public class ImageController : Controller
    {
        private IImage _imageService;
        private IConfiguration _config;
        private string AzureConnectionString { get; }

        public ImageController(IConfiguration config, IImage imageService)
        {
            _imageService = imageService;
            _config = config;
            AzureConnectionString = _config["AzureStorageConnectionString"];
        }
        public IActionResult Upload()
        {
            var model = new UploadImageModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewImage(IFormFile file, string tags, string title)
        {
            var container = _imageService.GetBlobContainer(AzureConnectionString, "images");
            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = content.FileName.Trim('"');
            var blockBlob = container.GetBlockBlobReference(fileName);
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _imageService.SetImage(title, tags, blockBlob.Uri);
            return RedirectToAction("index", "Gallery");
        }
    }
}