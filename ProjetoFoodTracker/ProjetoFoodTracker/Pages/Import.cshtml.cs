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
        public async void OnPostUploadtoDb(IFormFile file)
        {
           _fileUploadService.UploadtoDb(file);
        }
        public async void OnPostLocalSave(IFormFile ufile, int sessionCount)
        {
            if (ufile != null)
            {
                Filepath = await _fileUploadService.UploadFileAsync(ufile);
            }

        }

    }
}
