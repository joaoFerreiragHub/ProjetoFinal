using CsvHelper.Configuration;

namespace ProjetoFoodTracker.Data.Entities
{
    public class FoodsMap : ClassMap<Food>
    {
        public FoodsMap()
        {
            Map(m => m.FoodName).Name("Foods");
            Map(m => m.Category).Name("Categories");
            Map(m => m.FoodAction).Name("Foods");
        }
      
    }
}
