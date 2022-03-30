using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Services.FoodServices
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _ctx;

        public FoodService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Category> GetAllCategories() => _ctx.Categories.ToList();

        public List<Food> GetAllFoods() => _ctx.Foods.ToList();

        public List<FoodAction> GetAllFoodActions() => _ctx.FoodActions.ToList();

        public List<Actions> GetAllActions() => _ctx.Actions.ToList();

        public List<FoodAction> GetActionsByFood()
        {
            var actions = (from food in _ctx.Foods
                           join action in _ctx.FoodActions on food.Id equals action.Id
                           join category in _ctx.Categories on food.Id equals category.Id
                           select action).ToList(); 
            return actions;
        }
    }
}
