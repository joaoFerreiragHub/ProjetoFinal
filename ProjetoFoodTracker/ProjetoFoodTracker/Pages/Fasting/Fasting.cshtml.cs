using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;
using ProjetoFoodTracker.Services.MealService;

namespace ProjetoFoodTracker.Pages.Fasting
{
    public class FastingModel : PageModel
    {
        private readonly IFoodService _foodService;
        private readonly IMealService _MealService;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        public FastingModel(IFoodService foodService, IMealService MealService, ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _foodService = foodService;
            _MealService = MealService;
            _ctx = ctx;
            _userManager = userManager;    
        }

        [BindProperty]
        public List<DateTime> allStarts{ get; set; } = new List<DateTime>();
        public List<DateTime> allEndingsFasting { get; set; } = new List<DateTime>();
        public void OnGet()
        {
            GetCurrentFast();
        }

        public void GetCurrentFast()
        {
            var x = _ctx.MealsList.OrderBy(x => x.MealStart).Last();
            var z = x.MealStart;
            var y = x.MealEnded;
           
            if (x.MealStart <= x.MealEnded)
            {
              var fast = y - DateTime.Now;

                
                DateTime dt = DateTime.Now + fast;
                allStarts.Add(dt);
            }
            
        }
    }
}
