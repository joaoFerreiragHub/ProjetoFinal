#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoFoodTracker.Data;
using ProjetoFoodTracker.Data.Entities;

namespace ProjetoFoodTracker.Pages.MyActions
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
        public Actions Actions { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var actionCheck = _context.Actions.FirstOrDefault(c => c.ActionName == Actions.ActionName);
            if (actionCheck == null)
            {
                _context.Actions.Add(Actions);
                await _context.SaveChangesAsync();

                TempData["Success"] = "New Action created";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["Failed"] = "That Action already Exist";
                return RedirectToPage("./Create");
            }

        }
    }
}
