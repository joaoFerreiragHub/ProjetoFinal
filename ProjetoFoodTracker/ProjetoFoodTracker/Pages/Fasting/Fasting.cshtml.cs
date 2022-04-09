using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Pages.Fasting
{
    [Authorize(Roles = "Customer")]
    public class FastingModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _ctx;

        public FastingModel(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        [BindProperty]
        public List<TimeSpan> allStarts { get; set; } = new List<TimeSpan>();

        [BindProperty]
        public List<TimeSpan> allmeals { get; set; } = new List<TimeSpan>();
        public async Task OnGet()
        {
            await GetCurrentFast();
            await GetAnyFast();
        }

        public async Task<IActionResult> GetCurrentFast()
        {
            var task = await Task.Run(() =>
            {
                var mealCheck = _ctx.MealsList.Any(x => x.MealsId >= 1);

                if (mealCheck == true)
                {
                    var lastMeal = _ctx.MealsList.OrderBy(x => x.MealStart).Last();
                    var endOfMeal = lastMeal.MealEnded;

                    if (lastMeal.MealStart <= lastMeal.MealEnded)
                    {
                        var fasting = DateTime.Now - endOfMeal;
                        allStarts.Add(fasting);
                    }
                    return RedirectToPage("./Fasting");
                }
                else
                {
                    TempData["Failed"] = "No Meals registered";
                    return RedirectToPage("./Meals");
                }
            });

            return task;
        }

        public async Task<IActionResult> GetAnyFast()
        {
            var user = await _userManager.GetUserAsync(User);
            var meals = _ctx.MealsList.ToList();

            for (int i = 0; i < meals.Count(); i++)
            {
                if (i == meals.Count() - 1)
                    continue;
                else
                {
                    var fasts = meals[i + 1].MealStart - meals[i].MealEnded;
                    allmeals.Add(fasts);
                }            
            }
            return Page();
        }
    }
}

