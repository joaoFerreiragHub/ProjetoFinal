namespace ProjetoFoodTracker.Data.Entities
{
    public class Food
    {
        public string  FoodName{ get; set; }
        public int Id { get; set; }

        //Fk
        public string ActionId{ get; set; }
        public Action Action { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; } 
    }
}
