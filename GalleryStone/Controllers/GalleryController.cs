using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalleryStone.Data;
using GalleryStone.Data.Models;
using GalleryStone.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalleryStone.Controllers
{
    public class GalleryController : Controller
    {
        private IImage _imageService;
        public GalleryController(IImage imageService)
        {
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            var imageList = _imageService.GetAll();
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuerry=""
            };
            return View(model);
        }
    }
}