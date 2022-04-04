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

        private readonly ApplicationDbContext _ctx;

        public FastingModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;

        }

        [BindProperty]
        public List<TimeSpan> allStarts { get; set; } = new List<TimeSpan>();
        [BindProperty]
        public List<TimeSpan> allStartz { get; set; } = new List<TimeSpan>();
        public List<DateTime> allEndingsFasting { get; set; } = new List<DateTime>();


        public void OnGet()
        {
            GetCurrentFast();
        }

        public IActionResult GetCurrentFast()
        {
           var  mealCheck = _ctx.MealsList.Any(x => x.MealsId >= 1);

            if (mealCheck == true)
            {
                var x = _ctx.MealsList.OrderBy(x => x.MealStart).Last();
                var z = x.MealStart;
                var y = x.MealEnded;

                if (x.MealStart <= x.MealEnded)
                {
                    var fast = DateTime.Now - y;
                    allStartz.Add(fast);
                }
     
                return RedirectToPage("./Fasting");
            }
            else
            {
                TempData["Failed"] = "No Meals registered";
                return RedirectToPage("./Meals");
            }

        }
    }
}

