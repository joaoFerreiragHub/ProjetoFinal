using CsvHelper.Configuration;
using ProjetoFoodTracker.Data.Entities;
using System.Collections.Generic;

namespace ProjetoFoodTracker.Services
{
    public class MapCategory : ClassMap<Category>
    {
        public MapCategory()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.CategoryName).Name("Categories");
        }
    }
    
}
