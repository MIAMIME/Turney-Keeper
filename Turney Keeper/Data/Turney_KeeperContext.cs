using Microsoft.EntityFrameworkCore;
using Turneys.Models;

namespace Turney_Keeper.Data
{
    public class Turney_KeeperContext : DbContext
    {
        public Turney_KeeperContext(DbContextOptions<Turney_KeeperContext> options)
            : base(options)
        {
        }

        public DbSet<Turneys.Models.Turney> Turneys { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Turney>()
                .Property(t => t.UserIds)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                );
        }
        public DbSet<Turney_Keeper.Models.Users>? Users { get; set; }
    }
}
