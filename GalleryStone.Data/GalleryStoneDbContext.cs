using GalleryStone.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GalleryStone.Data
{
    public class GalleryStoneDbContext : DbContext
    {
        public GalleryStoneDbContext(DbContextOptions options) :base(options)
        {
            
        }

        public DbSet<GalleryImage> Images { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }
    }
}
