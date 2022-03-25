using CsvHelper.Configuration;

namespace ProjetoFoodTracker.Data.Entities
{
    public class DataTablesMap : ClassMap<DataTables>
    {
        public DataTablesMap()
        {
            Map(m => m.Foods).Name("Foods");
            Map(a => a.Categories).Name("Categories");
            Map(m => m.Actions).Name("Actions");
        }
    }
}
