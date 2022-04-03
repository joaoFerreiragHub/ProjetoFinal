using Microsoft.AspNetCore.Mvc;
using ProjetoFoodTracker.Data;

namespace ProjetoFoodTracker.Services.UserServices
{
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
                var maxValue = 10;
                var topFoods = _ctx.FoodMealsList.GroupBy(f => f.FoodId).OrderByDescending(f => f.Count()).Take(maxValue);
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
                var maxValue = 5;
                var mealsByOrder = _ctx.MealsList.GroupBy(x => x.ApplicationUser.Id).OrderByDescending(x => x.Count()).Take(maxValue);
                var usersName = mealsByOrder.Select(x => x.First().ApplicationUser.UserName);
                return usersName.ToList();
            });

            return list;
        }
    }
}
