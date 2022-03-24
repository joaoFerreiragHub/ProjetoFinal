using Microsoft.AspNetCore.Identity;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Meals
    {
        public int MealsId { get; set; }


        //FK
        public string FoodId { get; set; }
        public Food Food { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
