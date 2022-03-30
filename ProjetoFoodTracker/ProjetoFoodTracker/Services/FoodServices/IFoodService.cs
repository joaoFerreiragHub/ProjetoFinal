using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Services.FoodServices
{
    public interface IFoodService
    {
        List<Food> GetAllFoods();
        List<FoodAction> GetAllFoodActions();
        List<Category> GetAllCategories();
        List<Actions> GetAllActions();

        List<FoodAction> GetActionsByFood();

    }
}
