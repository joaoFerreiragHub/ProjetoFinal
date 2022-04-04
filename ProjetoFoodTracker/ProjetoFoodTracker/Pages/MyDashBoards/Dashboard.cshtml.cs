using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Services.UserServices;


namespace ProjetoFoodTracker.Pages.MyDashBoards
{
    [Authorize(Roles = "ADMIN")]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IUserService _userService;
        public DashboardModel(ApplicationDbContext ctx, IUserService userService)
        {
            _ctx = ctx;
            _userService = userService;
        }

        public int AppUser { get; set; }
        public int MealsCount { get; set; }
        public string TopFoods { get; set; }  
        public List<string> TopUsersMeals { get; set; }
      

        public async Task<IActionResult> OnGet()
        {
            AppUser = await _userService.GetTotalCustomersCount();
            MealsCount = await _userService.GetTotalMealsCount();
            TopFoods = await _userService.GetTopFoods();
            TopUsersMeals = await _userService.GetTopUsersMeals();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}


