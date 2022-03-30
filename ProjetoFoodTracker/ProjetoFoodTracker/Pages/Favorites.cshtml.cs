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
        public FavoritesModel(ApplicationDbContext ctx ,IFoodService foodService)
        {
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

        [BindProperty]
        public Food foodie { get; private set; }


        public async Task OnGet(Food food)
        {
            Foods = await _foodService.GetAllFoods();        
            Categories = await _foodService.GetAllCategories();
            FoodActions = await _foodService.GetAllFoodActions(); 
            Actions = await  _foodService.GetAllActions();
            //FoodActions = await _foodService.GetActionsByFood();
            displayCategories = await _ctx.Categories.ToListAsync();
            displayActions = await _ctx.Actions.ToListAsync();
        
        }
        public void OnPost()
        {

        }
        public void OnPostAddFavorite(int ID)
        {
            _foodService.AddToFavorites(ID);
        }

    }
}
