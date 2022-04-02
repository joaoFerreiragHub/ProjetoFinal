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


        void AddMeal(Meals meal, string userId);
        void RemoveMeal(int ID, string userId);
        Task <IActionResult> AddFoodToMeal(int ID, string userId);
        void RemoveFoodFromMeal(int ID, string userId);
        void MealDetails(int ID, string userId);
    }
}
