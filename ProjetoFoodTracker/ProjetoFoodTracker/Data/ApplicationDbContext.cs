using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoFoodTracker.Data.Entities;


namespace ProjetoFoodTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actions> Actions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Primary Keys & Required fields
            modelBuilder.Entity<Actions>().HasKey(a => a.ActionId);
            modelBuilder.Entity<Actions>().Property(a => a.ActionName).IsRequired(true);


            modelBuilder.Entity<ApplicationUser>().HasKey(e => e.Id);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.FirstName).IsRequired(true);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.LastName).IsRequired(true);

            modelBuilder.Entity<Blacklist>().HasKey(b => b.BlacklistId);

            modelBuilder.Entity<Favorites>().HasKey(f => f.FavoritesId);

            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.CategoryName).IsRequired(true);

            modelBuilder.Entity<Food>().HasKey(o => o.FoodId);
            modelBuilder.Entity<Food>().Property(o => o.FoodName).IsRequired(true);


            modelBuilder.Entity<Meals>().HasKey(m => m.MealsId);

            //Composite Keys

            modelBuilder.Entity<FoodAction>()
                .HasOne(f => f.Food)
                .WithMany(fa => fa.FoodAction)
                .HasForeignKey(fi => fi.FoodId);

            modelBuilder.Entity<FoodAction>()
                .HasOne(a => a.Actions)
                .WithMany(fa => fa.FoodAction)
                .HasForeignKey(ai => ai.ActionId);

            modelBuilder.Entity<FoodMeals>()
                .HasOne(f => f.Food)
                .WithMany(fM => fM.FoodMeals)
                .HasForeignKey(fi => fi.FoodId);

            modelBuilder.Entity<FoodMeals>()
                .HasOne(a => a.ApplicationUser)
                .WithMany(fa => fa.FoodMeals)
                .HasForeignKey(ai => ai.UserId);


        }
    }
}