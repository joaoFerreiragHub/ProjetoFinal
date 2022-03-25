namespace ProjetoFoodTracker.Data.Entities
{
    public class Food
    {
        public string  FoodName{ get; set; }
        public int FoodId { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public List<FoodAction> FoodAction { get; set; }
        public List<FoodMeals> FoodMeals { get; set; }
    }
}
