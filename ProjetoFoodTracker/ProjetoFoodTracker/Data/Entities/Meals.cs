using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Meals
    {
        [Key]
        public int MealsId { get; set; }
        public string Name { get; set; }

                                               
        public DateTime MealStart { get; set; }
        public DateTime MealEnded { get; set; }


        //FK
        [Required]
        public ApplicationUser ApplicationUser { get; set; }
        public List<FoodMeals> FoodMeals { get; set; }
    }
}
