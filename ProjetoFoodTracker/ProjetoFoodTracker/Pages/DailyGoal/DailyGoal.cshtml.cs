using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;
using ProjetoFoodTracker.Services.MealService;

namespace ProjetoFoodTracker.Pages.DailyGoal
{
    [Authorize(Roles = "Customer")]
    public class DailyGoalModel : PageModel
    {
        
        private readonly IFoodService _foodService;
        private readonly IMealService _mealService;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public DailyGoalModel(IFoodService foodService, IMealService MealService,
            ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _foodService = foodService;
            _mealService = MealService;
            _ctx = ctx;
        }

        public List<Category> Categories { get; set; }

        public List<Actions> Actions { get; set; }

        public List<TypePortion> TypePorts { get; set; }

        [BindProperty]
        public TrackSuccess Track { get; set; }

        [BindProperty]
        public List<TrackSuccess> ShowSuccess { get; set; } = new List<TrackSuccess>();

        [BindProperty]
        public List<TrackSuccess> TrackSuccess { get; set; } = new List<TrackSuccess>();

        [BindProperty]
        public List<Food> Foods { get; set; }
        [BindProperty]
        public List<FoodAction> FoodActions { get; set; }

        [BindProperty]
        public List<FoodMeals> AddDetails { get; set; } = new List<FoodMeals>();
        [BindProperty]
        public List<Food> FoodForDailyGoal { get; set; } = new List<Food>();

        [BindProperty]
        public bool SuccesOrFail { get; set; }

        [BindProperty]
        public List<Meals> Meal { get; set; } = new List<Meals>();


        public async Task OnGet()
        {
            Categories = await _foodService.GetAllCategoriesAsync();
            Actions = await _foodService.GetAllActionsAsync();
            FoodActions = _ctx.FoodActions.ToList();
            TypePorts = _ctx.PortionTypes.ToList();
            Meal = await _mealService.GetAllMealsAsyn();
            AddDetails = await _mealService.GetAllFoodMealsAsyn();
            Foods =  GetFoodByAction();
            SuccesOrFail = SuccessORFail(GetFoodByAction());
            FoodForDailyGoal = SuggestFoods(Foods);
            TrackSuccess = await _foodService.GetAllSuccessesAsync();

            SaveProgress(SuccesOrFail);
        }

        public List<Food> GetFoodByAction()
        {
            DateTime tomorrow = DateTime.Now.AddDays(1);

            var foods =  (from fm in _ctx.FoodMealsList
                         where (fm.Meals.MealEnded >= DateTime.Today && fm.Meals.MealEnded <= tomorrow)
                         join food in _ctx.FoodActions on fm.FoodId equals food.Id
                         join actions in _ctx.FoodActions on food.Id equals actions.FoodId
                         select actions.Food).Distinct().ToList();

            return foods;
        }

        public bool SuccessORFail(List<Food> foods)
        {

            var sumAngiogenesis = 0;
            var sumMicrobiome = 0;
            var sumDnaProtection = 0;
            var sumImmunity = 0;
            var sumRegeneration = 0;

            foreach (var item in foods)
            {
                var angiogenesis = item.FoodAction.Count(x => x.Actions.ActionName == "Angiogenesis");
                var microbiome = item.FoodAction.Count(x => x.Actions.ActionName == "Microbiome");
                var dnaProtection = item.FoodAction.Count(x => x.Actions.ActionName == "DNA Protection");
                var immunity = item.FoodAction.Count(x => x.Actions.ActionName == "Immunity");
                var regeneration = item.FoodAction.Count(x => x.Actions.ActionName == "Regeneration");

                sumAngiogenesis += angiogenesis;
                sumMicrobiome += microbiome;
                sumDnaProtection += dnaProtection;
                sumImmunity += immunity;
                sumRegeneration += regeneration;
            }

            if (sumAngiogenesis >= 5 && sumMicrobiome >= 5 && sumDnaProtection >= 5 && sumImmunity >= 5 && sumRegeneration >= 5)
            {
                return true;
            }
            else
                return false;
        }

        public void SaveProgress(bool isSuccess)
        {
            var checkIfExists = _ctx.TrackSuccesses.FirstOrDefault(x => x.Date == DateTime.Today);

            if(checkIfExists == null)
            {
                if (isSuccess == true)
                {
                    TrackSuccess today = new()
                    {
                        Date = DateTime.Today,
                        IsSuccess = true
                    };
                    _ctx.TrackSuccesses.Add(today);
                    _ctx.SaveChanges();

                }
                else if (isSuccess == false)
                {
                    TrackSuccess today = new()
                    {
                        Date = DateTime.Today,
                        IsSuccess = false
                    };
                    _ctx.TrackSuccesses.Add(today);
                    _ctx.SaveChanges();
                }
            }
            else
            {
                if(isSuccess == true)
                {
                    checkIfExists.IsSuccess = true;
                    _ctx.SaveChanges();

                }
                else if (isSuccess == false)
                {
                    var cgetTrack = _ctx.TrackSuccesses.FirstOrDefault(x => x.Id == checkIfExists.Id);
                    if(cgetTrack.Date == DateTime.Today)
                    {
                         _ctx.SaveChanges();
                    }
                    else
                    {
                        cgetTrack.IsSuccess = false;
                        _ctx.SaveChanges();
                    }
                }
            }
        }

        public List<Food> SuggestFoods(List<Food> foods)
        {
            var sumAngiogenesis = 0;
            var sumMicrobiome = 0;
            var sumDnaProtection = 0;
            var sumImmunity = 0;
            var sumRegeneration = 0;

            foreach (var item in foods)
            {
                var angiogenesis = item.FoodAction.Count(x => x.Actions.ActionName == "Angiogenesis");
                var microbiome = item.FoodAction.Count(x => x.Actions.ActionName == "Microbiome");
                var dnaProtection = item.FoodAction.Count(x => x.Actions.ActionName == "DNA Protection");
                var immunity = item.FoodAction.Count(x => x.Actions.ActionName == "Immunity");
                var regeneration = item.FoodAction.Count(x => x.Actions.ActionName == "Regeneration");

                sumAngiogenesis += angiogenesis;
                sumMicrobiome += microbiome;
                sumDnaProtection += dnaProtection;
                sumImmunity += immunity;
                sumRegeneration += regeneration;

            }
            if (sumAngiogenesis < 5 || sumMicrobiome < 5 || sumDnaProtection < 5 || sumImmunity < 5 || sumRegeneration < 5)
            {

                List<Food> foodSugestions = new();

                var getFoods = (from actions in _ctx.FoodActions
                                where (actions.Actions.ActionName == "Angiogenesis" || actions.Actions.ActionName == "Microbiome"
                                || actions.Actions.ActionName == "DNA Protection" || actions.Actions.ActionName == "Immunity"
                                || actions.Actions.ActionName == "Regeneration")
                                join food in _ctx.Foods on actions.Id equals food.Id
                                select actions.Food).Take(15).ToList();

                foreach (var food in getFoods)
                {
                    var checkBlacklist = _ctx.BlackLists.Any(x => x.Food.FoodName == food.FoodName);
                    var checkMeals = _ctx.FoodMealsList.Any(x => x.Food.FoodName == food.FoodName);
                    if (checkBlacklist == false && checkMeals == false)
                    {
                        foodSugestions.Add(food);
                    }
                }
                return foodSugestions;
            }

            return FoodForDailyGoal.ToList();
        }

        public IActionResult OnPostSearchSuccess()
        {
            var userId = _userManager.GetUserId(User);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            var checkSuccess = _ctx.TrackSuccesses.FirstOrDefault(x => x.Date == Track.Date);
            if (checkSuccess != null)
            {
                ShowSuccess.Add(checkSuccess);
            }
            else
            {
                TempData["Failed"] = "No Results for that day";
                return RedirectToPage("./Progress");
            }

            return Page();
        }
    }
}










        //else if (sumamicrobiome < 5)
        //{
        //    var checkActions = (from actions in _ctx.FoodActions
        //                        where (actions.Actions.ActionName == "Microbiome")
        //                        join food in _ctx.Foods on actions.Id equals food.Id
        //                        select actions.Food).ToList();

        //    var check = checkActions.Distinct().Take(5).ToList();
        //    return check;
        //}
        //else if (sumaregeneration < 5)
        //{
        //    var checkActions = (from actions in _ctx.Actions
        //                        where (actions.ActionName == "DNA Protection")
        //                        join food in _ctx.FoodActions on actions.Id equals food.Id
        //                        select food).ToList();

        //    var listed = foodForDailyGoal.Distinct().ToList();
        //    return listed;
        //}
        //else if (sumadnaProtection < 5)
        //{
        //    var checkActions = (from actions in _ctx.Actions
        //                        where (actions.ActionName == "Immunity")
        //                        join food in _ctx.FoodActions on actions.Id equals food.Id
        //                        select food).ToList();

        //    var listed = foodForDailyGoal.Distinct().ToList();
        //    return listed;
        //}
        //else if (sumaimmunity < 5)
        //{
        //    var checkActions = (from actions in _ctx.Actions
        //                        where (actions.ActionName == "Regeneration")
        //                        join food in _ctx.FoodActions on actions.Id equals food.Id
        //                        select food).ToList();

        //    var listed = foodForDailyGoal.Distinct().ToList();
        //    return listed;
        //}
        //    if (sumangiogenesis < 5 && sumamicrobiome < 5 && sumadnaProtection < 5 && sumaimmunity < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" || foodies.Actions.ActionName == "Microbiome"
        //                            && foodies.Actions.ActionName == "DNA Protection" || foodies.Actions.ActionName == "Immunity" || foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumamicrobiome < 5 && sumadnaProtection < 5 && sumaimmunity < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Microbiome"
        //                            && foodies.Actions.ActionName == "DNA Protection" && foodies.Actions.ActionName == "Immunity")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumamicrobiome < 5 && sumadnaProtection < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Microbiome"
        //                            && foodies.Actions.ActionName == "DNA Protection")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }
        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumamicrobiome < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Microbiome")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }
        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }
        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumamicrobiome < 5 && sumadnaProtection < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Microbiome"
        //                            && foodies.Actions.ActionName == "DNA Protection" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumamicrobiome < 5 && sumaimmunity < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Microbiome"
        //                            && foodies.Actions.ActionName == "Immunity" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumadnaProtection < 5 && sumaimmunity < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "DNA Protection" && foodies.Actions.ActionName == "Immunity" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumamicrobiome < 5 && sumadnaProtection < 5 && sumaimmunity < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Microbiome" && foodies.Actions.ActionName == "DNA Protection" && foodies.Actions.ActionName == "Immunity" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    if (sumangiogenesis < 5 && sumamicrobiome < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Microbiome" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumaimmunity < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Immunity" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumadnaProtection < 5 && sumaimmunity < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "DNA Protection" && foodies.Actions.ActionName == "Immunity" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }

        //    else if (sumangiogenesis < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumaimmunity < 5 && sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Immunity" && foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumaimmunity < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "Immunity")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumangiogenesis < 5 && sumadnaProtection < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Angiogenesis" && foodies.Actions.ActionName == "DNA Protection")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    if (sumamicrobiome < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Microbiome")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().Take(5).ToList();
        //    }
        //    else if (sumadnaProtection < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "DNA Protection")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumaimmunity < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Immunity")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }
        //    else if (sumaregeneration < 5)
        //    {
        //        var checkActions = (from foodies in _ctx.FoodActions
        //                            where (foodies.Actions.ActionName == "Regeneration")
        //                            join food in _ctx.Foods on foodies.FoodId equals food.Id
        //                            join actions in _ctx.Actions on food.Id equals actions.Id
        //                            select foodies).Distinct().ToList();

        //        foreach (var item in checkActions)
        //        {
        //            foodForDailyGoal.Add(item);
        //        }

        //        return foodForDailyGoal.Distinct().ToList();
        //    }


        //    return foodForDailyGoal.Distinct().ToList();
        //}

