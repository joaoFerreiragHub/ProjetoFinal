using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;


namespace ProjetoFoodTracker.Pages.MyUsers
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;

        public UsersModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<ApplicationUser> AppUser { get; set; }
        

        public void OnGet()
        {
            AppUser = _ctx.Users.ToList();
        }

        public IActionResult OnPostDeleteUser(string id)
        {     
            var userCheck = _ctx.Users.FirstOrDefault(x=>x.Id == id);
            if (userCheck.UserName != "Admin@admin.com")
            {
                _ctx.Users.Remove(userCheck);
                _ctx.SaveChanges();
                TempData["Success"] = "Removed from Users List";
                return RedirectToPage("./Users");
            }
            else
            {
                TempData["Failed"] = "Can't Delete Admin";
                return RedirectToPage("./Users");
            }

        }
    }
}
