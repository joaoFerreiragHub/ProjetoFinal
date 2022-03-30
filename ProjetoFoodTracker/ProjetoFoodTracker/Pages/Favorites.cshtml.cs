using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;

namespace ProjetoFoodTracker.Pages
{
    public class FavoritesModel : PageModel
    {
        private readonly IFoodService _foodService;

        public FavoritesModel(IFoodService foodService)
        {
            _foodService = foodService;
        }
        public List<Category> Categories { get; set; }
        public List<Food> Foods { get; set; }

        [BindProperty]
        public List<FoodAction> FoodActions { get; set; }
        public List<Actions> Actions { get; set; }

        public void OnGet()
        {
            //Foods = _foodService.GetAllFoods();        
            //Categories = _foodService.GetAllCategories();
            FoodActions = _foodService.GetAllFoodActions(); 
            //Actions = _foodService.GetAllActions();
            //FoodActions = _foodService.GetActionsByFood();
        }

       

    }
}
