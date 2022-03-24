namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodAction
    {
        public int FoodId { get; set; }
        public int ActionId { get; set; }
        public Food Food { get; set; }
        public Actions Actions { get; set; }
    }
}
