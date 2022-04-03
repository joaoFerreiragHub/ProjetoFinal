using Microsoft.AspNetCore.Mvc;

namespace ProjetoFoodTracker.Services.UserServices
{
    public interface IUserService
    {
        Task<int> GetTotalCustomersCount();
        Task<int> GetTotalMealsCount();
        Task<string> GetTopFoods();
        Task<List<string>> GetTopUsersMeals();
    }
}
