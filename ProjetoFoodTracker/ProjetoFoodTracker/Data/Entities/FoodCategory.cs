using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodCategory
    {
        [Column(Order = 0)]
        public int FoodId { get; set; }
        [Column(Order = 1)]
        public int CategoryId { get; set; }
        public Food Food { get; set; }
        public Category Category { get; set; }
    }
}
