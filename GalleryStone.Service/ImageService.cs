using GalleryStone.Data;
using GalleryStone.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryStone.Service
{
    public class ImageService : IImage
    {
        private GalleryStoneDbContext _context;
        public ImageService(GalleryStoneDbContext context)
        {
            _context = context;
        }
        public IEnumerable<GalleryImage> GetAll()
        {
            return _context.Images.Include(img => img.Tags);
        }

        public CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }

        public GalleryImage GetById(int id)
        {
            return _context.Images.Find(id);
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll().Where(img => img.Tags
            .Any(t => t.Description == tag));
        }

        public async Task SetImage(string title, string tags, Uri uri)
        {
            var image = new GalleryImage
            {
                Title = title,
                Tags = new List<ImageTag>(),
                Url = uri.AbsoluteUri,
                Created = DateTime.Now
            };
            _context.Add(image);
            await _context.SaveChangesAsync();
        }
    }
}
