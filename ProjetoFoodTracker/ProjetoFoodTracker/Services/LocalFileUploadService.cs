using CsvHelper;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using System.Globalization;

namespace ProjetoFoodTracker.Services
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IWebHostEnvironment _environment;
        public LocalFileUploadService(ApplicationDbContext ctx, IWebHostEnvironment environment)
        {
            _ctx = ctx;
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, @"wwwroot\File", file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return filePath;
        }

        public void UploadtoDb(IFormFile file)
        {
            var stream = file.OpenReadStream();
            using (var reader = new StreamReader(stream))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var foodRecords = new List<Food>();
                var categoryRecords = new List<Category>();
                var ActionsRecords = new List<Actions>();
                csvReader.Read();
                csvReader.ReadHeader();
                while (csvReader.Read())
                {
                    var catRecords = new Category
                    {
                        CategoryName = csvReader.GetField("Categories")
                    };
                    categoryRecords.Add(catRecords);

                    var fRecords = new Food
                    {
                        FoodName = csvReader.GetField("Food")
                    };
                    foodRecords.Add(fRecords);

                    var actionsRecords = new Actions
                    {
                        ActionName = csvReader.GetField("Food")
                    };
                    ActionsRecords.Add(actionsRecords);
                    _ctx.SaveChanges();
                }
            }

         }
    }
}





