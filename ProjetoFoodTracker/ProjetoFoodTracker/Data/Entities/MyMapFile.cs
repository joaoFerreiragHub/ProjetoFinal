namespace ProjetoFoodTracker.Data.Entities
{
    public class MyMapFile
    {
        public List<Category> category { get; set; }
        public List<Food> Food { get; set; }
        public List<Actions> Actions { get; set; }

        public MyMapFile(List<Category> category, List<Food> Food, List<Actions> Actions)
        {
            this.category = category;
            this.Food = Food;
            this.Actions = Actions;
        }
    }
}
