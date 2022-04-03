using Microsoft.AspNetCore.Mvc;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;

namespace ProjetoFoodTracker.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext _ctx;

        public MealService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddMeal(FoodMeals FoodMealsProp, string userId)
        {
            

                _ctx.FoodMealsList.Add(FoodMealsProp);
                _ctx.SaveChanges();            
        }

        public async Task<List<Food>> GetAllFoodsAsync() => await Task.Run(() => _ctx.Foods.ToList());
        public async Task<List<FoodAction>> GetAllFoodActionsAsync() => await Task.Run(() => _ctx.FoodActions.ToList());
        public async Task<List<Category>> GetAllCategoriesAsync() => await Task.Run(() => _ctx.Categories.ToList());
        public async Task<List<Actions>> GetAllActionsAsync() => await Task.Run(() => _ctx.Actions.ToList());
        public async Task<List<FoodMeals>> GetAllFoodMealsAsyn() => await Task.Run(() => _ctx.FoodMealsList.ToList());
        public async Task<List<Meals>> GetAllMealsAsyn() => await Task.Run(() => _ctx.MealsList.ToList());


        public void RemoveMeal(int ID, string userId)
        {
            throw new NotImplementedException();
        }
        public void EditMeal(int ID, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> AddFoodToMeal(int ID, string userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFoodFromMeal(int ID, string userId)
        {
            throw new NotImplementedException();
        }

        public void MealDetails(int ID, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
