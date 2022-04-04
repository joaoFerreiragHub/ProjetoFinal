using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;
using ProjetoFoodTracker.Services.MealService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoFoodTracker.Pages
{
    [Authorize]
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
        public TypePortion portionsType { get; set; } = new TypePortion();
        public IList<FoodMeals> FoodMeals { get; set; }
        public List<FoodMeals> AddDetails { get; set; } = new List<FoodMeals>();
        public List<TypePortion> TypePorts { get; set; }
        public List<FoodAction> foodActions { get; set; }


        [BindProperty]
        public Meals meals { get; set; } = new Meals();

        [BindProperty]
        public FoodMeals FoodMealsProp { get; set; } = new FoodMeals();

        [BindProperty]
        public List<Meals> Meal { get; set; } = new List<Meals>();

        [BindProperty]
        public List<FoodMeals> AddFoodsToMeals { get; set; } = new List<FoodMeals>();

        public async Task OnGet()
        {
            Foods = await _foodService.GetAllFoodsAsync();
            Categories = await _foodService.GetAllCategoriesAsync();
            Actions = await _foodService.GetAllActionsAsync();
            foodActions = _ctx.FoodActions.ToList();
            TypePorts = _ctx.portionTypes.ToList();
            Meal = await _MealService.GetAllMealsAsyn();
            AddDetails = await _MealService.GetAllFoodMealsAsyn();

            ViewData["FoodId"] = new SelectList(_ctx.Foods, "Id", "FoodName");
            ViewData["MealName"] = new SelectList(_ctx.MealsList, "MealsId", "Name");
            ViewData["Id"] = new SelectList(_ctx.portionTypes, "Id", "Type");

            FoodMeals = await _ctx.FoodMealsList
            .Include(f => f.Food)
            .Include(f => f.Meals).ToListAsync();

        }

        public IActionResult OnPostAddMeal(Meals meals, FoodMeals FoodMealsProp, TypePortion portionsType)
        {
            var userId = _userManager.GetUserId(User);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            if (meals.Name != null)
            {
                Meals newMeal = new Meals()
                {
                    ApplicationUser = user,
                    Name = meals.Name,
                    MealStart = meals.MealStart,
                    MealEnded = meals.MealEnded,
                };
                _ctx.MealsList.Add(newMeal);
                _ctx.SaveChanges();

                var checkMealsID = _ctx.MealsList.FirstOrDefault(x => x.Name == meals.Name).MealsId;
                var checkportion = _ctx.portionTypes.FirstOrDefault(x => x.Id == portionsType.Id);
                FoodMealsProp.MealId = checkMealsID;
                FoodMealsProp.TypePortions = checkportion;


                _MealService.AddMeal(FoodMealsProp, userId);
                TempData["Success"] = "Meal Created, Now add some food to it!";
                return RedirectToPage("./AddFoodToMeal");
            }
            else
            {
                TempData["Failed"] = "No data Inserted";
                return RedirectToPage("./Meals");
            }
        }

        public IActionResult OnPostAddFoodToMeal(FoodMeals FoodMealsProp, TypePortion portionsType, Meals meals)
        {

            var checkMeal = _ctx.MealsList.FirstOrDefault(x => x.MealsId == meals.MealsId);
            var checkportion = _ctx.portionTypes.FirstOrDefault(x => x.Id == portionsType.Id);

            if (checkMeal != null)
            {
                FoodMeals newAddFoodToMeal = new FoodMeals()
                {
                    MealId = checkMeal.MealsId,
                    Food = FoodMealsProp.Food,
                    FoodId = FoodMealsProp.FoodId,
                    Quantity = FoodMealsProp.Quantity,
                    TypePortions = checkportion,
                };
                _ctx.FoodMealsList.Add(newAddFoodToMeal);
                _ctx.SaveChanges();
                TempData["Success"] = "Food Added, you can add some more if you want!";
                return RedirectToPage("./AddFoodToMeal");
            }
            else
            {
                TempData["Failed"] = "No data Inserted";
                return RedirectToPage("./AddFoodToMeal");
            }
        }
        //public IActionResult OnPostFoodFromMeal(int ID, int sessionCount)
        //{

        //    var checkMeal = _ctx.MealsList.FirstOrDefault(x => x.MealsId == meals.MealsId);
        //    var checkportion = _ctx.portionTypes.FirstOrDefault(x => x.Id == portionsType.Id);
        //    var checkID = _ctx.FoodMealsList.FirstOrDefault(x => x.FoodId == ID);

        //    if (checkID.Id != null)
        //    {
        //        _ctx.FoodMealsList.Remove(checkID);
        //        _ctx.SaveChanges();

        //        TempData["Success"] = "Meal Removed!";
        //        return RedirectToPage("./AddFoodToMeal");
        //    }
        //    else
        //    {
        //        TempData["Failed"] = "Something Happened";
        //        return RedirectToPage("./Meals");
        //    }
        //}
        public IActionResult OnPostRemoveMeal(int ID,int sessionCount)
        {
            var checkID = _ctx.FoodMealsList.FirstOrDefault(x => x.Id == ID);

            if (checkID.Id != null)
            {
                _ctx.FoodMealsList.Remove(checkID);
                _ctx.SaveChanges();

                TempData["Success"] = "Meal Removed!";
                return RedirectToPage("./AddFoodToMeal");
            }
            else
            {
                TempData["Failed"] = "Something Happened";
                return RedirectToPage("./Meals");
            }
        }
    }
}
