using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Services.FoodServices
{
    public interface IFoodService
    {
        Task <List<Food>> GetAllFoods();
        Task <List<FoodAction>> GetAllFoodActions();
        Task<List<Category>> GetAllCategories();
        Task<List<Actions>> GetAllActions();
        Task AddToFavorites(int ID);


    }
}
