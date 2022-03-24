using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodAction
    {
        [Column(Order = 0)]
        public int FoodId { get; set; }
        [Column(Order = 1)]
        public int ActionId { get; set; }
        public Food Food { get; set; }
        public Actions Actions { get; set; }
    }
}
