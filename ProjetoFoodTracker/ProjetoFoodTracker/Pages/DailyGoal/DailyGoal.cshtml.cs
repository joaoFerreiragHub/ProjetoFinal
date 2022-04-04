using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;
using ProjetoFoodTracker.Services.MealService;

namespace ProjetoFoodTracker.Pages.DailyGoal
{
    public class DailyGoalModel : PageModel
    {
        private readonly IFoodService _foodService;
        private readonly IMealService _mealService;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public DailyGoalModel(IFoodService foodService, IMealService MealService,
            ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _foodService = foodService;
            _mealService = MealService;
            _ctx = ctx;
            _userManager = userManager;
        }

        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<Food> Foods { get; set; }
        public List<Actions> Actions { get; set; }
        public TypePortion portionsType { get; set; } = new TypePortion();
        public IList<FoodMeals> FoodMeals { get; set; }
        [BindProperty]
        public List<FoodMeals> AddDetails { get; set; } = new List<FoodMeals>();
        public List<TypePortion> TypePorts { get; set; }
        [BindProperty]
        public List<FoodAction> foodActions { get; set; }


        [BindProperty]
        public Meals meals { get; set; } = new Meals();

        [BindProperty]
        public FoodMeals FoodMealsProp { get; set; } = new FoodMeals();

        [BindProperty]
        public List<Meals> Meal { get; set; } = new List<Meals>();

        [BindProperty]
        public List<FoodMeals> AddFoodsToMeals { get; set; } = new List<FoodMeals>();

        [BindProperty]
        public List<FoodAction> actionOfFoods { get; set; } = new List<FoodAction>();
        public async Task OnGet()
        {
           // Foods = await _foodService.GetAllFoodsAsync();
            Categories = await _foodService.GetAllCategoriesAsync();
            Actions = await _foodService.GetAllActionsAsync();
            foodActions = _ctx.FoodActions.ToList();
            TypePorts = _ctx.portionTypes.ToList();
            Meal = await _mealService.GetAllMealsAsyn();
            AddDetails = await _mealService.GetAllFoodMealsAsyn();
            Foods = GetFoodByAction();
        }

        public List<Food> GetFoodByAction()
        {
            var x = (from fm in _ctx.FoodMealsList
                     join food in _ctx.FoodActions on fm.FoodId equals food.Id
                     join actions in _ctx.FoodActions on food.Id equals actions.FoodId
                     select actions.Food).Distinct().ToList();
            
            

            return x;

        }

    }
}
