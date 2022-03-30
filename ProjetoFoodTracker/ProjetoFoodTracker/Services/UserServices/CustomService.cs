using ProjetoFoodTracker.Data;

namespace ProjetoFoodTracker.Services.UserServices
{
    public class CustomService //: IUserService
    {
        private readonly ApplicationDbContext _ctx;
        public CustomService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        //public void AddBlacklisted()
        //{
        //    _ctx.BlackLists.Add();
        //    _ctx.SaveChanges();
        //}

        //public void AddFavorite()
        //{
        //    _ctx.FavoritesSet.Add();
        //    _ctx.SaveChanges();
        //}

        //public void RemoveBlacklisted()
        //{
        //    _ctx.BlackLists.Remove();
        //    _ctx.SaveChanges();
        //}

        //public void RemoveFavorite()
        //{
        //    _ctx.FavoritesSet.Add();
        //    _ctx.SaveChanges();
        //}
    }
}
