using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodMeals
    {
        [Key]
        public int Id { get; set; }

        
        public int FoodId { get; set; }
        public Food Food { get; set; }


        public int MealId { get; set; }
        public Meals Meals { get; set; }


        [Required]
        public TypePortion TypePortions { get; set; }

        [Required]
        public decimal Portion { get; set; }

    }
}
