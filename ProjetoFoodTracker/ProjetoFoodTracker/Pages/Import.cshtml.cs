using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services;

namespace ProjetoFoodTracker.Pages
{
    [Authorize]
    public class ImportModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IFileUploadService _fileUploadService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ImportModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx, IFileUploadService fileUploadService)
        {
            _ctx = ctx;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
        }
      
        public string Filepath;
        public void OnGet()
        {

        }
        public void OnPostUploadtoDb(IFormFile file)
        {
           _fileUploadService.UploadtoDb(file);
        }

    }
}
