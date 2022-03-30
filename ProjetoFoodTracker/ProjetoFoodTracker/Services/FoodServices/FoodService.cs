using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Services.FoodServices
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _ctx;

        public readonly userManager UserManager

        public FoodService(ApplicationDbContext ctx, userManager userManager)
        {
            _ctx = ctx;
            UserManager = userManager;
        }

        public async Task AddToFavorites(int ID)
        {
           var food = _ctx.Foods.FirstOrDefault(x => x.Id == ID);
            Favorites newFavorite = new Favorites()
            {
                newFavorite.Food = food;
                newFavorite.ApplicationUser = 
            }

            if(!_ctx.FavoritesSet.Contains(newFavorite))
            _ctx.FavoritesSet.Add(newFavorite);
            else
             _ctx.FavoritesSet.Update(newFavorite);
            await _ctx.SaveChangesAsync();

        }

        public async Task<List<Food>> GetAllFoods() => await Task.Run(() => _ctx.Foods.ToList());
        public async  Task<List<FoodAction>> GetAllFoodActions() => await Task.Run(() => _ctx.FoodActions.ToList());
        public async  Task<List<Category>> GetAllCategories() => await Task.Run(() => _ctx.Categories.ToList());
        public async Task<List<Actions>> GetAllActions() => await Task.Run(() => _ctx.Actions.ToList());


        //public async Task<List<FoodAction>> GetActionsByFood()
        //{
        //    var actions = await Task.Run( ()=> (from food in _ctx.Foods
        //                   join action in _ctx.FoodActions on food.Id equals action.Id
        //                   join category in _ctx.Categories on food.Id equals category.Id
        //                   select action).ToList()
        //                   );
        //    return actions;
        //}


    }
}
