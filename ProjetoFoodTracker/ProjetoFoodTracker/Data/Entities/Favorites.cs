using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Favorites
    {
        [Key]
        public int Id { get; set; }

        //FK
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
