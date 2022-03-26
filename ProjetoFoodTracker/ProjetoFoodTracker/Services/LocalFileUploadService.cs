using CsvHelper;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using System.Configuration;
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
                    _ctx.Categories.Add(catRecords);
                    _ctx.SaveChanges();

                    var fRecords = new Food
                    {
                        FoodName = csvReader.GetField("Foods")
                    };
                    _ctx.Foods.Add(fRecords);
                    _ctx.SaveChanges();

                    var actionsRecords = new Actions
                    {
                        ActionName = csvReader.GetField("Actions")
                    };
                    _ctx.Actions.Add(actionsRecords);
                    _ctx.SaveChanges();
                }
           
            }

            //using (var reader = new StreamReader(stream))
            //using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    var foodRecords = new List<Food>();
            //    var categoryRecords = new List<Category>();
            //    var ActionsRecords = new List<Actions>();
            //    csvReader.Read();
            //    csvReader.ReadHeader();
            //    while (csvReader.Read())
            //    {
            //        var catRecords = new Category
            //        {
            //            CategoryName = csvReader.GetField("Categories")
            //        };
            //        categoryRecords.Add(catRecords);

            //        var fRecords = new Food
            //        {
            //            FoodName = csvReader.GetField("Food")
            //        };
            //        foodRecords.Add(fRecords);

            //        var actionsRecords = new Actions
            //        {
            //            ActionName = csvReader.GetField("Food")
            //        };
            //        ActionsRecords.Add(actionsRecords);
            //        _ctx.SaveChanges();
            //    }
            //}

        }


    }
}








