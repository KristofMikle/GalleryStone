using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalleryStone.Data.Models;

namespace GalleryStone.Models
{
    public class GalleryIndexModel
    {
        public IEnumerable<GalleryImage> Images { get; set; }
        public string SearchQuerry { get; set; }
    }
}
