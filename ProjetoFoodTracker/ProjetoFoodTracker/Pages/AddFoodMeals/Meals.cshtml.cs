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
        public Meals meals { get; set; }

        public List<FoodMeals> AddDetails { get; set; }

        [BindProperty]
        public List<Meals> Meal { get; set; } = new List<Meals>();

        [BindProperty]
        public List<FoodMeals> AddFoodsToMeals { get; set; } = new List<FoodMeals>();

        public async Task OnGet()
        {
            Foods = await _foodService.GetAllFoodsAsync();
            Categories = await _foodService.GetAllCategoriesAsync();
            Actions = await _foodService.GetAllActionsAsync();

            Meal = await _MealService.GetAllMealsAsyn();
            AddDetails = await _MealService.GetAllFoodMealsAsyn();
        }
  
        public void OnPostAddMeal(Meals meals)
        {
            var userId = _userManager.GetUserId(User);
            _MealService.AddMeal(meals, userId);
        }
        public void OnPostRemoveMeals(int ID, int sessionCount)
        {
            var userId = _userManager.GetUserId(User);
            _MealService.RemoveMeal(ID, userId);
        }
        public void OnPostAddFoodToMeal(int ID, int sessionCount)
        {

           var userId = _userManager.GetUserId(User);
           _MealService.RemoveMeal(ID, userId);

        }
    }
}
