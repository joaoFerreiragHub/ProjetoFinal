using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodAction
    {
        public int FoodActionId { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int ActionId { get; set; }
        public Actions Actions { get; set; }
        
    }
}
