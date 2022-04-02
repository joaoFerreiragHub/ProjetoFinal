using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services;

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
        public DbSet<Favorites> FavoriteList { get; set; }
        public DbSet<Blacklist> BlackLists { get; set; }
        public DbSet<Meals> MealsList{ get; set; }
        public DbSet<FoodAction> FoodActions { get; set; }
        public DbSet<FoodMeals> FoodMealsList { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<TypePortion> portionTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Actions>().HasKey(a => a.Id);
            modelBuilder.Entity<Actions>().Property(a => a.ActionName).IsRequired(true);

            modelBuilder.Entity<ApplicationUser>().HasKey(e => e.Id);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.FirstName).IsRequired(true);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.LastName).IsRequired(true);

            modelBuilder.Entity<Blacklist>().HasKey(b => b.Id);

            modelBuilder.Entity<Favorites>().HasKey(f => f.Id);

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.CategoryName).IsRequired(true);

            modelBuilder.Entity<Food>().HasKey(m => m.Id);
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
                .HasOne(a => a.Meals)
                .WithMany(fa => fa.FoodMeals)
                .HasForeignKey(ai => ai.MealId);
        }
    }
}