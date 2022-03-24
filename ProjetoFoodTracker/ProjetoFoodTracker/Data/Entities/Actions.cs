namespace ProjetoFoodTracker.Data.Entities
{
    public class Actions
    {

        public int ActionId { get; set; }
        public string ActionName { get; set; }

        public List<FoodAction> FoodAction { get; set; }
    }
}
