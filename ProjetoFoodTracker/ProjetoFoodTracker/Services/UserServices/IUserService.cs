namespace ProjetoFoodTracker.Services.UserServices
{
    public interface IUserService
    {
        public void AddFavorite();
        public void RemoveFavorite();
        public void AddBlacklisted();
        public void RemoveBlacklisted();

    }
}
