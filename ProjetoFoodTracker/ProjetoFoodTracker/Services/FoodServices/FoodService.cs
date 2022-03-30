using Microsoft.AspNet.Identity;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using Microsoft.AspNetCore.Identity;



namespace ProjetoFoodTracker.Services.FoodServices
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _ctx;

        public FoodService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<List<Food>> GetAllFoods() => await Task.Run(() => _ctx.Foods.ToList());
        public async Task<List<FoodAction>> GetAllFoodActions() => await Task.Run(() => _ctx.FoodActions.ToList());
        public async Task<List<Category>> GetAllCategories() => await Task.Run(() => _ctx.Categories.ToList());
        public async Task<List<Actions>> GetAllActions() => await Task.Run(() => _ctx.Actions.ToList());

        public void AddToFavorites(int ID, string userId)
        {
            var food = _ctx.Foods.FirstOrDefault(x => x.Id == ID);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            Favorites newFavorite = new Favorites()
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
            if(checkfave != null)
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

            Blacklist newblacklist = new Blacklist()
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

            Favorites newFavorite = new Favorites()
            {
                ApplicationUser = user,
                Food = food,
                date = DateTime.Now,
            };
           
            var checkfave = _ctx.FavoriteList.FirstOrDefault(x => x.Food == food);
            if (checkfave != null)
                _ctx.SaveChanges();
            else
            {
                _ctx.FavoriteList.Remove(newFavorite);
                _ctx.SaveChanges();
            }
        }

        public void RemoveFromBlacklist(int ID, string userId)
        {
            var food = _ctx.Foods.FirstOrDefault(x => x.Id == ID);
            var user = _ctx.Users.FirstOrDefault(x => x.Id == userId);

            Blacklist newblacklist = new Blacklist()
            {
                ApplicationUser = user,
                Food = food,
                date = DateTime.Now,
            };

            var checkBlack = _ctx.BlackLists.FirstOrDefault(x => x.Food == food);
            if (checkBlack != null)
                _ctx.SaveChanges();
            else
            {
                _ctx.BlackLists.Remove(newblacklist);
                _ctx.SaveChanges();
            }
        }
    }
}

