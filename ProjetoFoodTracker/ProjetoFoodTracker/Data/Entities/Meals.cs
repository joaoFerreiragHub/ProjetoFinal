using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Meals
    {
        [Key]
        public int MealsId { get; set; }

        public DateTime MealStart { get; set; }
        public DateTime MealEnded { get; set; }


        //FK
        [Required]
        public Food Food { get; set; }

        [Required]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
