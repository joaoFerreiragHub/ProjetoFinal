#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Pages.MyActions
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetoFoodTracker.Data.ApplicationDbContext _context;

        public DeleteModel(ProjetoFoodTracker.Data.ApplicationDbContext context)
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

            Actions = await _context.Actions.FirstOrDefaultAsync(m => m.ActionId == id);

            if (Actions == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actions = await _context.Actions.FindAsync(id);

            if (Actions != null)
            {
                _context.Actions.Remove(Actions);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
