using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Food Name")]
        public string FoodName { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<FoodAction> FoodAction { get; set; }
        public List<FoodMeals> FoodMeals { get; set; }
    }
}
