using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoFoodTracker.Services.FoodServices
{
    [Authorize]
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _ctx;

        public FoodService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public List<Actions> GetAllActions() => _ctx.Actions.ToList();
        public async Task<List<Food>> GetAllFoodsAsync() => await Task.Run(() => _ctx.Foods.ToList());
        public async Task<List<FoodAction>> GetAllFoodActionsAsync() => await Task.Run(() => _ctx.FoodActions.ToList());
        public async Task<List<Category>> GetAllCategoriesAsync() => await Task.Run(() => _ctx.Categories.ToList());
        public async Task<List<Actions>> GetAllActionsAsync() => await Task.Run(() => _ctx.Actions.ToList());
        public async Task<List<Favorites>> GetAllFavoritesAsync() => await Task.Run(() => _ctx.FavoriteList.ToList());
        public async Task<List<Blacklist>> GetAllBlacklistAsync() => await Task.Run(() => _ctx.BlackLists.ToList());
        public async Task<List<TrackSuccess>> GetAllSuccessesAsync() => await Task.Run(() => _ctx.TrackSuccesses.ToList());

        public void AddToFavorites(int ID, string userId)
        {
            var food = _ctx.Foods.FirstOrDefault(x => x.Id == ID);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            Favorites newFavorite = new()
            {
                ApplicationUser = user,
                Food = food,
                date = DateTime.Now,
            };

            var check = _ctx.BlackLists.FirstOrDefault(x => x.Food == food);

            if (_ctx.BlackLists.Contains(check))
            {
                _ctx.BlackLists.Remove(check);
                _ctx.SaveChanges();
            }
            var checkfave = _ctx.FavoriteList.FirstOrDefault(x => x.Food == food);
            if (checkfave != null)
                _ctx.SaveChanges();
            else
            {
                _ctx.FavoriteList.Add(newFavorite);
                _ctx.SaveChanges();
            }
        }

        public void AddToBlacklist(int ID, string userId)
        {
            var food = _ctx.Foods.FirstOrDefault(x => x.Id == ID);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            Blacklist newblacklist = new()
            {
                ApplicationUser = user,
                Food = food,
                date = DateTime.Now,
            };

            var check = _ctx.FavoriteList.FirstOrDefault(x => x.Food == food);

            if (_ctx.FavoriteList.Contains(check))
            {
                _ctx.FavoriteList.Remove(check);
                _ctx.SaveChanges();
            }
            var checkBlack = _ctx.BlackLists.FirstOrDefault(x => x.Food == food);
            if (checkBlack != null)
                _ctx.SaveChanges();
            else
            {
                _ctx.BlackLists.Add(newblacklist);
                _ctx.SaveChanges();
            }
        }

        public void RemoveFromFavorites(int ID, string userId)
        {
            var food = _ctx.Foods.FirstOrDefault(x => x.Id == ID);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            var checkfave = _ctx.FavoriteList.FirstOrDefault(x => x.Food == food);
            if (checkfave != null)
            {
                _ctx.FavoriteList.Remove(checkfave);
                _ctx.SaveChanges();
            }
            else
            _ctx.SaveChanges();
        }

        public void RemoveFromBlacklist(int ID, string userId)
        {
            var food = _ctx.Foods.FirstOrDefault(x => x.Id == ID);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            var checkBlack = _ctx.BlackLists.FirstOrDefault(x => x.Food == food);
            if (checkBlack != null)
            {
                _ctx.BlackLists.Remove(checkBlack);
                _ctx.SaveChanges();
            }
            else
                _ctx.SaveChanges();
        }


    }
}

