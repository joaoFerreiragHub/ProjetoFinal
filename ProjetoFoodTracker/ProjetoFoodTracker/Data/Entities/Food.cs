namespace ProjetoFoodTracker.Data.Entities
{
    public class Food
    {
        public string  FoodName{ get; set; }
        public int FoodId { get; set; }

        public List<FoodAction> FoodAction { get; set; }
        public List<FoodCategory> FoodCategory { get; set; }
    }
}
