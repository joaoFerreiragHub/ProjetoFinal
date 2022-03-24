namespace ProjetoFoodTracker.Data.Entities
{
    public class Food
    {
        public string  FoodName{ get; set; }
        public int FoodId { get; set; }

        //Fk
        public string ActionId{ get; set; }
        public Actions Action { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; } 
    }
}
