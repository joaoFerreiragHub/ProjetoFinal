using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using System.Data;
using System.Globalization;

namespace ProjetoFoodTracker.Pages
{
    public class ImportModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        public ImportModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadFile(IFormFile file)
        {

            if (file?.Length > 0)
            {
                var stream = file.OpenReadStream();


                try
                {
                    using (var reader = new StreamReader(stream))
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        while (csvReader.Read())
                        {
                            List<Food> foods = new List<Food>();

                            //csvReader.Context.RegisterClassMap<DataTablesMap>();
                            var result = csvReader.GetRecord<dynamic>().ToList();
                            result.Foods = foods;
                        }
                        
                    }


                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
            return RedirectToAction("Index");
        }
    }
}
