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
    public class DetailsModel : PageModel
    {
        private readonly ProjetoFoodTracker.Data.ApplicationDbContext _context;

        public DetailsModel(ProjetoFoodTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
