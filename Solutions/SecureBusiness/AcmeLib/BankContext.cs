using Microsoft.EntityFrameworkCore;
namespace AcmeLib
{
    public class BankContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public BankContext(DbContextOptions<BankContext> op) : base(op) { }
        protected override void OnConfiguring(DbContextOptionsBuilder bldr)
            => bldr.UseLazyLoadingProxies();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         //   modelBuilder.Entity<User>().ToTable("t_users");
            base.OnModelCreating(modelBuilder);
        }
    }
}
