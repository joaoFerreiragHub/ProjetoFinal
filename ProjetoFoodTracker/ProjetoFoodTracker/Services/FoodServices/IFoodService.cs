using Microsoft.AspNetCore.Mvc;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Services.FoodServices
{
    public interface IFoodService
    {
        Task<List<Food>> GetAllFoods();
        Task<List<FoodAction>> GetAllFoodActions();
        Task<List<Category>> GetAllCategories();
        Task<List<Actions>> GetAllActions();
        void AddToFavorites(int ID, string userId);
        void AddToBlacklist(int ID, string userId);
        void RemoveFromFavorites(int ID, string userId);
        void RemoveFromBlacklist(int ID, string userId);
    }
}
