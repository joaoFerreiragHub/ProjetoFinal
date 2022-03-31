using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;
using ProjetoFoodTracker.Services.MealService;

namespace ProjetoFoodTracker.Pages
{
    public class FoodToMealsModel : PageModel
    {
        private readonly IFoodService _foodService;
        private readonly IMealService _MealService;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public FoodToMealsModel(IFoodService foodService, IMealService MealService,
            ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _foodService = foodService;
            _MealService = MealService;
            _userManager = userManager;
        }

        public List<Category> Categories { get; set; }
        public List<FoodAction> Foods { get; set; }
        public List<Actions> Actions { get; set; }
        public List<Meals> MealList { get; set; }

        [BindProperty]
        public Food Foodies { get; set; }





        public async Task OnGet()
        {
            Foods = await _foodService.GetAllFoodActionsAsync();
            Categories = await _foodService.GetAllCategoriesAsync();
            Actions = await _foodService.GetAllActionsAsync();

            MealList = await _MealService.GetAllMealsAsyn();

        }



    }
}
