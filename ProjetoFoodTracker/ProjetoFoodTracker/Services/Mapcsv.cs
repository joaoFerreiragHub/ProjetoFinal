using CsvHelper.Configuration;
using ProjetoFoodTracker.Data.Entities;
using System.Collections.Generic;

namespace ProjetoFoodTracker.Services
{
    public class Mapcsv : ClassMap<Food>
    {
        public Mapcsv()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.FoodName).Name("Foods");
            Map(m => m.CategoryId).Name("Id");

        }
    }
}
