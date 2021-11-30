using Microsoft.EntityFrameworkCore;

namespace CaffStore.Dal
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
