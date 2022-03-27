using CsvHelper;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using System.Configuration;
using System.Data;
using System.Globalization;

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

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, @"wwwroot\File\", file.Name);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath;
        }

        public void UploadtoDb(IFormFile file)
        {
            var stream = file.OpenReadStream();
            using (var reader = new StreamReader(stream))

            using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                var categoryRecords = new List<Category>();
                var ActionsRecords = new List<Actions>();
                var foodRecords = new List<Food>();
                csvReader.Read().ToString().Split(',');
                csvReader.ReadHeader();
                csvReader.Configuration.BadDataFound.Equals(true);
                while (csvReader.Read())
                {
                    var catRecords = new Category
                    {
                        CategoryName = csvReader.GetField("Categories")
                    };
                    categoryRecords.Add(catRecords);

                    var fRecords = new Food
                    {
                        FoodName = csvReader.GetField("Foods"),
                        Category = catRecords
                    };

                    foodRecords.Add(fRecords);
                    foodRecords.Select(x => x.FoodName).Distinct();

                    var actionsRecords = new Actions
                    {
                        ActionName = csvReader.GetField("Actions")
                    };
                    ActionsRecords.Add(actionsRecords);
                    ActionsRecords.Select(x => x.ActionName).Distinct();


                }
                categoryRecords.Select(x => x.CategoryName).Distinct();
                var uniqueCategories = categoryRecords.GroupBy(p => p.CategoryName)
                           .Select(grp => grp.First())
                           .ToArray();

                foreach (var category in uniqueCategories)
                {
                    _ctx.Categories.Add(category);
                    _ctx.SaveChanges();

                }
                foodRecords.Select(x => x.FoodName).Distinct();
                var uniqueFoods = foodRecords.GroupBy(p => p.FoodName)
                           .Select(grp => grp.First())
                           .ToArray();
                foreach (var food in foodRecords)
                {
                    
                    _ctx.Foods.Add(food);
                    _ctx.SaveChanges();
                }

                foreach (var action in ActionsRecords)
                    _ctx.Actions.Add(action);
                _ctx.SaveChanges();
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
            }
        }






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

    }


}












