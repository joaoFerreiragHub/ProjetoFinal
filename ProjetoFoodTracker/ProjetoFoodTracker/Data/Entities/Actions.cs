using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Actions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ActionName { get; set; }

        public List<FoodAction> FoodAction { get; set; }
    }
}
