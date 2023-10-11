using Microsoft.EntityFrameworkCore;

namespace Shoporium.Data._EntityFramework
{
    public partial class ShoporiumContext : DbContext
    {
        public ShoporiumContext()
        {
        }

        public ShoporiumContext(DbContextOptions<ShoporiumContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
