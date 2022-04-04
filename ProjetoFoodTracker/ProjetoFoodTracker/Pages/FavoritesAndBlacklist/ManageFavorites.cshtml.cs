using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;
using ProjetoFoodTracker.Services.FoodServices;

namespace ProjetoFoodTracker.Pages
{
    [Authorize]
    public class FavoritesModel : PageModel
    {

        private readonly IFoodService _foodService;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        public FavoritesModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx, IFoodService foodService)
        {
            _userManager = userManager;
            _ctx = ctx;
            _foodService = foodService;
        }
        public List<Category> Categories { get; set; }
        public List<Food> Foods { get; set; }
        public List<Actions> Actions { get; set; }
        public List<Favorites> Favorites { get; set; } = new List<Favorites>();
        public List<Blacklist> Blacklisted { get; set; } = new List<Blacklist> { };
        public List<SelectListItem> CategoryOptions { get; set; }
        public List<SelectListItem> ActionsOptions { get; set; }

        //public IEnumerable<Category> displayCategories { get; set; }
        //public IEnumerable<Actions> displayActions { get; set; }

        [BindProperty]
        public List<FoodAction> FoodActions { get; set; }


        public async Task OnGet()
        {
            Foods = await _foodService.GetAllFoodsAsync();
            Categories = await _foodService.GetAllCategoriesAsync();
            FoodActions = await _foodService.GetAllFoodActionsAsync();
            Actions = await _foodService.GetAllActionsAsync();
            Favorites = await _foodService.GetAllFavoritesAsync();
            Blacklisted = await _foodService.GetAllBlacklistAsync();

            CategoryOptions = _ctx.Categories.Select(a =>
                                  new SelectListItem
                                  { Text = a.CategoryName }).ToList();

            ActionsOptions = _ctx.Actions.Select(a =>
                                  new SelectListItem
                                  { Text = a.ActionName }).ToList();

        }

        public async Task<IActionResult> OnPostAddFavorite(int ID)
        {
            var task = await Task.Run(() =>
            {
                var userId = _userManager.GetUserId(User);
                _foodService.AddToFavorites(ID, userId);
                TempData["Success"] = "Added a new Favorite!";
                return RedirectToPage("./Favorites");
            });

            return task;
        }

        public async Task<IActionResult> OnPostAddBlacklist(int ID, int sessionCount)
        {
            var task = await Task.Run(() =>
            {
                var userId = _userManager.GetUserId(User);
                _foodService.AddToBlacklist(ID, userId);
                TempData["Success"] = "Added a new Blacklisted Item";
                return RedirectToPage("./Blacklist");
            });

            return task;
        }
        public async Task<IActionResult> OnPostRemoveFromFavorites(int ID, int sessionCount)
        {
            var task = await Task.Run(() =>
            {
                var userId = _userManager.GetUserId(User);
                _foodService.RemoveFromFavorites(ID, userId);
                TempData["Success"] = "Removed from the favorites!";
                return RedirectToPage("./Favorites");
            });

            return task;
        }
        public async Task<IActionResult> OnPostRemoveFromBlacklist(int ID, int sessionCount)
        {
            var task = await Task.Run(() =>
            {
                var userId = _userManager.GetUserId(User);
                _foodService.RemoveFromBlacklist(ID, userId);
                TempData["Success"] = "Removed from Blaclist";
                return RedirectToPage("./Blacklist");
            });

            return task;
        }

        public async Task<IActionResult> OnPostManageList(int ID, int sessionCount)
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            return RedirectToPage("./ManageFavorites");
        }

    }
}
