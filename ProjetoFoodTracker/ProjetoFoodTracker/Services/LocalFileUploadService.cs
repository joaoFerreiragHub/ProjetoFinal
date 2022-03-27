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

                //var categoryRecords = new List<Category>();
                //var ActionsRecords = new List<Actions>();
                //var foodRecords = new List<Food>();
                //var csv = csvReader.Read().ToString().Split(',');
                //csvReader.ReadHeader();
                //csvReader.Configuration.BadDataFound.Equals(true);

            using (var dr = new CsvDataReader(csvReader))
                {
                    var dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("Categories", typeof(Category));
                    dt.Columns.Add("Foods", typeof(Food));
                    dt.Columns.Add("Actions",typeof(Action));
                    dt.Load(dr);

                    List<Category> categoryList = new List<Category>();
                    categoryList = (from DataRow d in dt.Columns
                                   select new Category()
                                   {
                                      CategoryName = dr["Categories"].ToString(),                            
                                   }).ToList();

                    List<Food> foodyList = new List<Food>();
                    foodyList = (from DataRow d in dt.Columns
                                    select new Food()
                                    {
                                        FoodName = dr["Foods"].ToString(),
                                        
                                    }).ToList();


                }





                //    while (csvReader.Read())
                //    {
                //        var catRecords = new Category
                //        {
                //            CategoryName = csvReader.GetField("Categories")
                //        };
                //        categoryRecords.Add(catRecords);

                //        var fRecords = new Food
                //        {
                //            FoodName = csvReader.GetField("Foods")
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
                //    foreach (var food in foodRecords)
                //    {
                //        _ctx.Foods.Add(food);
                //        _ctx.SaveChanges();
                //    }

                //    foreach (var action in ActionsRecords)
                //        _ctx.Actions.Add(action);
                //    _ctx.SaveChanges();
                //}

            }
        }

    }
}









