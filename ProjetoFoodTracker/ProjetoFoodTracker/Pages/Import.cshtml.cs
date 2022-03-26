using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services;
using System.Data;
using System.Globalization;

namespace ProjetoFoodTracker.Pages
{
    public class ImportModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IFileUploadService _fileUploadService;

     
        public ImportModel(ApplicationDbContext ctx, IFileUploadService fileUploadService)
        {
            _ctx = ctx;
            _fileUploadService = fileUploadService;
        }

        public IFileUploadService FileUploadService { get; set; }
        public string Filepath;
        public void OnGet()
        {

        }
        public async void OnPost(IFormFile file)
        {
            //if (file != null)
            //{
            //    Filepath = await _fileUploadService.UploadFileAsync(file); 
            //}

           _fileUploadService.UploadtoDb(file);
        }


        //public void OnPost(IFormFile file)
        //{
        //    RedirectToAction("Index");

        //    if (file?.Length > 0)
        //    {
        //        var stream = file.OpenReadStream();

        //        try
        //        {
        //            using (var reader = new StreamReader(stream))
        //            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        //            {
        //                var datesInCsv = new List<Food>();
        //                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
        //                config.Delimiter = ";";
        //                var parsedCsv = new CsvParser(reader, config);
        //            }

        //            //while (csvReader.Read())
        //            //{
        //            //    csvReader.Context.RegisterClassMap<FoodsMap>();
        //            //    List<Food> foods = new List<Food>();
        //            //    var foodRecords = csvReader.GetRecord<Food>().ToString().Split(',');
        //            //    foreach (var foodRecord in foodRecords)
        //            //        _ctx.Foods.Add(foodRecord, typeof(Food));
        //            //}

        //        }
        //        catch (Exception ex)
        //        {

        //            Console.WriteLine(ex.Message);
        //        }

        //        RedirectToAction("Index");
        //    }
        //}
    }
}
