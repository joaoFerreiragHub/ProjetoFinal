#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Pages.MyActions
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actions Actions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actions = await _context.Actions.FirstOrDefaultAsync(m => m.Id == id);

            if (Actions == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var categoryCheck = _context.Actions.FirstOrDefault(c => c.ActionName == Actions.ActionName);
            if (categoryCheck == null)
            {
                _context.Attach(Actions).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionsExists(Actions.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "That Action has been updated";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["Failed"] = "That Action already exists";
                return RedirectToPage("./Index");
            }
        }
        private bool ActionsExists(int id)
        {
            return _context.Actions.Any(e => e.Id == id);
        }
    }
}
