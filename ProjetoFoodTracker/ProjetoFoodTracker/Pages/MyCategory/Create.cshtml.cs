#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Pages.MyCategory
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var categoryCheck = _context.Categories.FirstOrDefault(c => c.CategoryName == Category.CategoryName);
            if(categoryCheck == null)
            {
                _context.Categories.Add(Category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "New Category created";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["Failed"] = "That Category already Exist";
                return RedirectToPage("./Create");
            }
        }
    }
}
