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

        public Food Food { get; set; }


        public ApplicationUser ApplicationUser { get; set; }
    }
}
