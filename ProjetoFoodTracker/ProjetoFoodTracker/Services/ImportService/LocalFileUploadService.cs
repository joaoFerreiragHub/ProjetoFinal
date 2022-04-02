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
            UploadtoDb(ufile);
            return filePath;
        }

        public void UploadtoDb(IFormFile file)
        {
            string path = @"C:\Users\gar_e\OneDrive\Imagens\Documentos\ProjetoFinal\ProjetoFoodTracker\ProjetoFoodTracker\wwwroot\File\Alimentus.csv";
   
            string[] text = File.ReadAllLines(path);

            for (int i = 1; i < text.Length; i++)
            {
                string[] columns = text[i].Split(',');
                var categoryName = columns[1];
                var actionsNames = columns[2].Trim().Split(';');

                Category newCategory;
                Actions newAction;

                if (!_ctx.Categories.Any(x => x.CategoryName.Equals(categoryName)))
                    newCategory = new Category() { CategoryName = categoryName };
                else
                    newCategory = _ctx.Categories.FirstOrDefault(x => x.CategoryName.Equals(categoryName));

                var food = new Food() { FoodName = columns[0], Category = newCategory };

                _ctx.Foods.Add(food);
                _ctx.SaveChanges();
   
                foreach (var actionName in actionsNames)
                {
                    if (!_ctx.Actions.Any(x => x.ActionName.Equals(actionName)))
                    {
                        newAction = new Actions() { ActionName = actionName };
                        _ctx.Actions.Add(newAction);
                        
                    }
                    else
                        newAction = _ctx.Actions.FirstOrDefault(x => x.ActionName.Equals(actionName));

                    var newFoodAction= new FoodAction() { Actions = newAction, ActionId = newAction.Id, Food = food, FoodId = food.Id};
                    _ctx.FoodActions.Add(newFoodAction);
                    
                }
                _ctx.SaveChanges();
            }

            string units = "Units";
            string grams = "Grams";
            TypePortion newTypePortion = new TypePortion() { Type = units };
            TypePortion secondTypePortion = new TypePortion() { Type = grams };
            _ctx.portionTypes.Add(newTypePortion);
            _ctx.portionTypes.Add(secondTypePortion);
            _ctx.SaveChanges();
        }
    }
}


//           var stream = file.OpenReadStream();
//using (var reader = new StreamReader(stream))
//using (CsvReader texto = new CsvReader(reader, CultureInfo.InvariantCulture))
//{
//    var text = texto.Read().ToString().Split(',');

//    for (int i = 1; i < text.Length; i++)
//    {
//        string[] lines = text, column = lines[2].Split('|'); ;
//        var categoryName = lines[1].Trim();
//        var actionsNames = column[0].Trim();

//        Category newCategory;
//        Actions newAction;
//        FoodAction foodActions;
//        Food GetFood;

//        if (!_ctx.Categories.Any(x => x.CategoryName.Equals(categoryName)))
//            newCategory = new Category() { CategoryName = categoryName };
//        else
//            newCategory = _ctx.Categories.FirstOrDefault(x => x.CategoryName.Equals(categoryName));

//        var food = new Food() { FoodName = lines[0], Category = newCategory };

//        for (int k = 0; k < column.Length; k++)
//        {
//            if (!_ctx.Actions.Any(x => x.ActionName.Equals(actionsNames)))
//                newAction = new Actions() { ActionName = actionsNames };
//            else
//                newAction = _ctx.Actions.FirstOrDefault(x => x.ActionName.Equals(actionsNames));

//            if (!_ctx.Foods.Any(x => x.FoodName.Equals(food.FoodName)))
//                foodActions = new FoodAction() { Food = food, Actions = newAction };
//            else
//            {
//                GetFood = _ctx.Foods.Where(x => x.FoodName == food.FoodName).FirstOrDefault();
//                //_ctx.Foods.FirstOrDefault(x => x.FoodName.Equals(food)).FoodName;                       
//                foodActions = new FoodAction() { FoodId = GetFood.Id, Actions = newAction };
//            }
//            _ctx.FoodActions.Add(foodActions);
//            _ctx.SaveChanges();
//        }
//    }
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













