using Microsoft.AspNetCore.Mvc;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Services.FoodServices
{
    public interface IFoodService
    {
        Task<List<Food>> GetAllFoodsAsync();
        Task<List<FoodAction>> GetAllFoodActionsAsync();
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Actions>> GetAllActionsAsync();
        void AddToFavorites(int ID, string userId);
        void AddToBlacklist(int ID, string userId);
        void RemoveFromFavorites(int ID, string userId);
        void RemoveFromBlacklist(int ID, string userId);
    }
}
