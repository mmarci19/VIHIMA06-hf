using CaffStore.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace CaffStore.Dal
{
    public class StoreDbContext : DbContext
    {
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UploadedFile>()
                .HasMany(caff => caff.Comments)
                .WithOne();
        }
    }
}
