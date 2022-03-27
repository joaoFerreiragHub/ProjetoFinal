using CsvHelper.Configuration;
using ProjetoFoodTracker.Data.Entities;
using System.Collections.Generic;

namespace ProjetoFoodTracker.Services
{
    public class MapActions : ClassMap<Actions>
    {
        public MapActions()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.ActionName).Name("Actions");
        }
    }
}
