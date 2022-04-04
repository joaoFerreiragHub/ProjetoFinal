#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Pages.MyFood
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        public CreateModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public List<Actions> actionsList { get; set; } = new List<Actions>();


        public List<FoodAction> foodActions { get; set; } = new List<FoodAction>();
        public List<SelectListItem> CategoryOptions { get; set; }

        [BindProperty]
        public Food Food { get; set; } = new Food();

        public IActionResult OnGet()
        {
            CategoryOptions = _ctx.Categories.Select(a =>
                         new SelectListItem
                         { Text = a.CategoryName }).ToList();

            actionsList = _ctx.Actions.ToList();
            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var foodCheck = _ctx.Foods.FirstOrDefault(c => c.FoodName == Food.FoodName);
            if (foodCheck == null)
            {
                var actionList = Request.Form["actionsList"];

                Actions newAction;
                List<FoodAction> listFoodAction = new List<FoodAction>();
                FoodAction newFoodAction;

                var category = _ctx.Categories.FirstOrDefault(x => x.CategoryName == Food.Category.CategoryName).Id;
                var newFood = new Food()
                {
                    FoodName = Food.FoodName,
                    CategoryId = category,
                };

                foreach (var actionName in actionList)
                {
                    newAction = _ctx.Actions.FirstOrDefault(x => x.ActionName.Equals(actionName));
                    newFoodAction = new FoodAction() { Actions = newAction, ActionId = newAction.Id, Food = newFood, FoodId = Food.Id };
                    _ctx.FoodActions.Add(newFoodAction);
                }

                await _ctx.SaveChangesAsync();
                TempData["Success"] = "New Food Added";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["Failed"] = "That Food already Exist";
                return RedirectToPage("./Create");
            }
        }
    }
}
