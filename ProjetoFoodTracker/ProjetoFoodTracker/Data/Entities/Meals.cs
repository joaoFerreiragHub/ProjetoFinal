namespace ProjetoFoodTracker.Data.Entities
{
    public class Meals
    {
        public int Id { get; set; }


        //FK
        public string FoodId { get; set; }
        public Food Food { get; set; }
    }
}
