using Microsoft.AspNetCore.Identity;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Favorites
    {
        public int FavoritesId { get; set; }

        //FK
        public string FoodId { get; set; }
        public Food Food { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
