using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodCategory
    {
        public int FoodCategoryId { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}
