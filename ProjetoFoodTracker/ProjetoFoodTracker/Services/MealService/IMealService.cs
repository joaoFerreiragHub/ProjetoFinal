using Microsoft.AspNetCore.Mvc;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Services.MealService
{
    public interface IMealService
    {
        Task<List<Meals>> GetAllMealsAsyn();
        Task<List<FoodMeals>> GetAllFoodMealsAsyn();
        Task<List<Food>> GetAllFoodsAsync();
        Task<List<FoodAction>> GetAllFoodActionsAsync();
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Actions>> GetAllActionsAsync();

        void AddMeal(FoodMeals FoodMealsProp, string userId);
    }
}
