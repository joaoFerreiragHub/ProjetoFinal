using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Favorites
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set ; } 


        //FK
        [Required]
        public Food Food { get; set; }

        [Required]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
