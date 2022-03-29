using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;

namespace ProjetoFoodTracker.Services
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IHostEnvironment _environment;
        public LocalFileUploadService(ApplicationDbContext ctx, IHostEnvironment environment)
        {
            _ctx = ctx;
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(IFormFile ufile)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, @"wwwroot\File\", ufile.Name);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await ufile.CopyToAsync(fileStream);
            UploadtoDb();
            return filePath;
        }

       
        public void UploadtoDb(/*IFormFile file*/)
        {

            string path = @"C:\Users\gar_e\Downloads\ProjetoFinal-main (2)\ProjetoFinal-main\ProjetoFoodTracker\ProjetoFoodTracker\wwwroot\File\Alimentus.csv";

            string[] text = File.ReadAllLines(path);

            for (int i = 1; i < text.Length; i++)
            {
                string[] lines = text[i].Split(',');
                var categoryName = lines[1].Trim();
                Category newCategory;

                if (!_ctx.Categories.Any(x => x.CategoryName.Equals(categoryName)))
                {
                    newCategory = new Category() { CategoryName = categoryName };
                }
                else
                {
                    newCategory = _ctx.Categories.FirstOrDefault(x => x.CategoryName.Equals(categoryName));
                }

                var food = new Food()
                { FoodName = lines[0], Category = newCategory };
                _ctx.Foods.Add(food);
                _ctx.SaveChanges();
            };


            _ctx.SaveChanges();




        }
    }
}


//var stream = file.OpenReadStream();
//using (var reader = new StreamReader(stream))
//using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
//var categoryRecords = new List<Category>();
//var ActionsRecords = new List<Actions>();
//var foodRecords = new List<Food>();                
//csvReader.Read().ToString().Split(',');


//var records = csvReader.GetRecords<csvRead>();
//foreach (var r in records)
//{
//    r.Actionsz.Split(',');

//}
//csvReader.ReadHeader();
//csvReader.Configuration.BadDataFound.Equals(true);


//while (csvReader.Read())
//{
//    var catRecords = new Category
//    {
//        CategoryName = csvReader.GetField("Categories")
//    };
//    categoryRecords.Add(catRecords);

//    var fRecords = new Food
//    {
//        FoodName = csvReader.GetField("Foods"),
//    };
//    foodRecords.Add(fRecords);


//    var actionsRecords = new Actions
//    {
//        ActionName = csvReader.GetField("Actions")
//    };
//    ActionsRecords.Add(actionsRecords);
//}

//categoryRecords.Select(x => x.CategoryName).Distinct();
//var uniqueCategories = categoryRecords.GroupBy(p => p.CategoryName)
//           .Select(grp => grp.First())
//           .ToArray();


//// Validar se categorias existem e, se não, inseri-las na BD
//var dbCategories = _ctx.Categories.ToList();

//foreach (var cat in uniqueCategories)
//{
//    if (!dbCategories.Contains(cat))
//        // se não existe
//        _ctx.Categories.Add(new Category() { CategoryName = cat.CategoryName });
//}
//_ctx.SaveChanges();


////foodRecords.Select(x => x.FoodName).Distinct();
////var uniqueFoods = foodRecords.GroupBy(p => p.FoodName)
////           .Select(grp => grp.First())
////           .ToArray();
//foreach (var food in foodRecords)
//{
//    food.CategoryId = _ctx.Categories.FirstOrDefault(x => x.CategoryName == food.Category.CategoryName).Id;
//    _ctx.Foods.Add(food);
//    _ctx.SaveChanges();
//}

//foreach (var action in ActionsRecords)
//    _ctx.Actions.Add(action);
//_ctx.SaveChanges();




//for (int j = 1; j < columns.Length; i++)
//{

//    string[] smicolumn = columns[j].Split(',');



//var stream = file.OpenReadStream();
//using (var reader = new StreamReader(stream))

//using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
//{

//    var categoryRecords = new List<Category>();
//    var ActionsRecords = new List<Actions>();
//    var foodRecords = new List<Food>();
//    csvReader.Read().ToString().Split(',');
//    csvReader.ReadHeader();
//    csvReader.Configuration.BadDataFound.Equals(true);
//    while (csvReader.Read())
//    {
//        var catRecords = new Category
//        {
//            CategoryName = csvReader.GetField("Categories")
//        };
//        categoryRecords.Add(catRecords);

//        var fRecords = new Food
//        {
//            FoodName = csvReader.GetField("Foods"),
//            Category = catRecords
//        };

//        foodRecords.Add(fRecords);
//        foodRecords.Select(x => x.FoodName).Distinct();

//        var actionsRecords = new Actions
//        {
//            ActionName = csvReader.GetField("Actions")
//        };
//        ActionsRecords.Add(actionsRecords);
//        ActionsRecords.Select(x => x.ActionName).Distinct();


//    }
//    categoryRecords.Select(x => x.CategoryName).Distinct();
//    var uniqueCategories = categoryRecords.GroupBy(p => p.CategoryName)
//               .Select(grp => grp.First())
//               .ToArray();

//    foreach (var category in uniqueCategories)
//    {
//        _ctx.Categories.Add(category);
//        _ctx.SaveChanges();

//    }
//    foodRecords.Select(x => x.FoodName).Distinct();
//    var uniqueFoods = foodRecords.GroupBy(p => p.FoodName)
//               .Select(grp => grp.First())
//               .ToArray();
//    foreach (var food in foodRecords)
//    {

//        _ctx.Foods.Add(food);
//        _ctx.SaveChanges();
//    }

//    foreach (var action in ActionsRecords)
//        _ctx.Actions.Add(action);
//    _ctx.SaveChanges();



//csvReader.Context.RegisterClassMap<MapCategory>();
//csvReader.Context.RegisterClassMap<MapActions>();
//var foodRecords = new List<Food>();
//var categoryRecords = new List<Category>();
//var actionsRecords = new List<Actions>();
//var isHeader = false;



//while (csvReader.Read())
//{
//    if (isHeader)
//    {
//        csvReader.ReadHeader();
//        isHeader = false;
//        continue;
//    }

//    switch (csvReader.GetField(0))
//    {
//        case "Foods":
//            foodRecords.Add(csvReader.GetRecord<Food>());
//            break;
//        case "Categories":
//            categoryRecords.Add(csvReader.GetRecord<Category>());
//            break;
//        case "Actions":
//            actionsRecords.Add(csvReader.GetRecord<Actions>());
//            break;
//        default:
//            throw new InvalidOperationException("Unknown record type.");
//    }
//}






//using (var dr = new CsvDataReader(csvReader))
//    {
//        var dt = new DataTable();
//        dt.Columns.Add("Id", typeof(int));
//        dt.Columns.Add("Categories", typeof(string));
//        dt.Columns.Add("Foods", typeof(string));
//        dt.Columns.Add("Actions",typeof(string));
//        dt.Load(dr);


//        var categoryList = (from DataColumn d in dt.Columns
//                       select new Category()
//                       {
//                          Id = Convert.ToInt32(dr["Id"]),
//                          CategoryName = dr["Categories"].ToString(),                                                
//                       }).ToList();

//        List<Food> foodyList = new List<Food>();
//        foodyList = (from DataColumn d in dt.Columns
//                        select new Food()
//                        {
//                            Id = Convert.ToInt32(dr["Id"]),
//                            CategoryId = Convert.ToInt32(dr["Id"]),
//                            FoodName = dr["Foods"].ToString(),

//                        }).ToList();

//        List<Actions> ActionsList = new List<Actions>();
//        ActionsList = (from DataColumn d in dt.Columns
//                     select new Actions()
//                     {
//                         Id = Convert.ToInt32(dr["Id"]),
//                         ActionName = dr["Action"].ToString(),

//                     }).ToList();













