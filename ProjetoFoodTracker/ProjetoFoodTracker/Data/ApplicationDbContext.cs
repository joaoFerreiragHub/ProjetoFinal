using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjetoFoodTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().Property(p => p.CountryCode).HasMaxLength(5);
            modelBuilder.Entity<Country>().Property(p => p.Code).HasMaxLength(5);
            modelBuilder.Entity<Book>().HasIndex(p => p.ISBN);
        }
    }
}