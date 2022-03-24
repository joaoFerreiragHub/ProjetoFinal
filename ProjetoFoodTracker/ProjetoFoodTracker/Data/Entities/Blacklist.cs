using Microsoft.AspNetCore.Identity;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Blacklist
    {
        public int Id { get; set; }


        //FK
        public string FoodId { get; set; }
        public Food Food { get; set; }

        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
