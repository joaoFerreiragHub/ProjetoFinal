using Microsoft.AspNetCore.Identity;

namespace ProjetoFoodTracker.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
 
        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public DateTime? DateOfBirth { get; set; }


    }
}
