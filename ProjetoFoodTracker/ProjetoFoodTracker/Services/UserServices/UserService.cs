using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFoodTracker.Data;

namespace ProjetoFoodTracker.Services.UserServices
{
    [Authorize]
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _ctx;
        public UserService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> GetTotalCustomersCount()
        {
            var list = await Task.Run(() =>
            {
                var customerRole = _ctx.UserRoles.ToList();
                var customerCount = _ctx.Roles.Where(u => u.Name == "Customer").Count();
                return customerCount;
            });

            return list;
        }
        public async Task<int> GetTotalMealsCount()
        {
            var list = await Task.Run(() =>
            {
                var meals = _ctx.MealsList.Count(m => m.MealsId >= 0);
                return meals;
            });

            return list;
        }
        public async Task<string> GetTopFoods()
        {
            var list = await Task.Run(() =>
            {
                var topFoods = _ctx.FoodMealsList.GroupBy(f => f.FoodId).OrderByDescending(f => f.Count()).Take(10);
                var foodList = topFoods.Select(m => m.First().Food.FoodName).ToList();
                var newlist = String.Join(" | ", foodList);
                return newlist;
            });

            return list;
        }
        public async Task<List<string>> GetTopUsersMeals()
        {
            var list = await Task.Run(() =>
            {
                var mealsByOrder = _ctx.MealsList.GroupBy(x => x.ApplicationUser.Id).OrderByDescending(x => x.Count()).Take(5);
                var usersName = mealsByOrder.Select(x => x.First().ApplicationUser.UserName);
                return usersName.ToList();
            });

            return list;
        }
    }
}
