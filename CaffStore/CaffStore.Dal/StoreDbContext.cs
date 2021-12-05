using CaffStore.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace CaffStore.Dal
{
    public class StoreDbContext : DbContext
    {
        public DbSet<CaffFile> CaffFiles { get; set; }
        public DbSet<CiffFile> CiffFile { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public StoreDbContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CaffFile>()
                .HasMany(caff => caff.Comments)
                .WithOne();

            modelBuilder.Entity<CaffFile>()
                .HasMany(caff => caff.CiffFiles)
                .WithOne(ciff => ciff.CaffFile)
                .HasForeignKey(ciff => ciff.CaffFileId);

            modelBuilder.Entity<CiffFile>()
                .OwnsMany(ciff => ciff.Tags);
        }
    }
}
