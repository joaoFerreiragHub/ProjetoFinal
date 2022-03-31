using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;
using ProjetoFoodTracker.Services.MealService;

namespace ProjetoFoodTracker.Pages
{
    public class MealsModel : PageModel
    {
        private readonly IFoodService _foodService;
        private readonly IMealService _MealService;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public MealsModel(IFoodService foodService, IMealService MealService, 
            ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _foodService = foodService;
            _MealService = MealService;
            _ctx = ctx;
            _userManager = userManager;
        }


        public List<Category> Categories { get; set; }
        public List<Food> Foods { get; set; }
        public List<Actions> Actions { get; set; }
        public List<Meals> MealList { get; set; }

        [BindProperty]
        public Meals Meal { get; set; }

        [BindProperty]
        public List<FoodMeals> AddFoodsToMeals { get; set; }

        public async Task OnGet()
        {
            Foods = await _foodService.GetAllFoodsAsync();
            Categories = await _foodService.GetAllCategoriesAsync();
            Actions = await _foodService.GetAllActionsAsync();

            MealList = await _MealService.GetAllMealsAsyn();
            AddFoodsToMeals = await _MealService.GetAllFoodMealsAsyn();
        }
  
        public void OnPostAddMeal()
        {
            var userId = _userManager.GetUserId(User);
            _MealService.AddMeal(Meal, userId);
        }
        public void OnPostRemoveMeals(int ID, int sessionCount)
        {
            var userId = _userManager.GetUserId(User);
            _MealService.RemoveMeal(ID, userId);
        }
        public async Task<IActionResult> OnPostAddFoodToMeal(int ID, int sessionCount)
        {

           RedirectToAction("FoodToMeals");
           var userId = _userManager.GetUserId(User);
           _MealService.RemoveMeal(ID, userId);

            return  RedirectToPage("FoodToMeals");
        }
    }
}
