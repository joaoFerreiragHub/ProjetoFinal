using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;

namespace ProjetoFoodTracker.Pages
{
    public class FavoritesModel : PageModel
    {
        private readonly IFoodService _foodService;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        public FavoritesModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx ,IFoodService foodService)
        {
            _userManager = userManager;
            _ctx = ctx;
            _foodService = foodService;
        }
        public List<Category> Categories { get; set; }
        public List<Food> Foods { get; set; }
        public List<Actions> Actions { get; set; }


        public IEnumerable<Category> displayCategories { get; set; }
        public IEnumerable<Actions> displayActions { get; set; }


        [BindProperty]
        public List<FoodAction> FoodActions { get; set; }



        public async Task OnGet()
        {
            Foods = await _foodService.GetAllFoodsAsync();        
            Categories = await _foodService.GetAllCategoriesAsync();
            FoodActions = await _foodService.GetAllFoodActionsAsync(); 
            Actions = await  _foodService.GetAllActionsAsync();
            //FoodActions = await _foodService.GetActionsByFood();
            displayCategories = await _ctx.Categories.ToListAsync();
            displayActions = await _ctx.Actions.ToListAsync();        
        }
        public void OnPostAddFavorite(int ID)
        {
            var userId = _userManager.GetUserId(User);
            _foodService.AddToFavorites(ID, userId);
        
        }
        public void OnPostAddBlacklist(int ID, int sessionCount)
        {
            var userId = _userManager.GetUserId(User);
            _foodService.AddToBlacklist(ID, userId);
        }
        public void OnPostRemoveFromFavorites(int ID, int sessionCount)
        {
            var userId = _userManager.GetUserId(User);
            _foodService.RemoveFromFavorites(ID, userId);
        }
        public void OnPostRemoveFromBlacklist(int ID, int sessionCount)
        {
            var userId = _userManager.GetUserId(User);
            _foodService.RemoveFromBlacklist(ID, userId);
        }
    }
}
