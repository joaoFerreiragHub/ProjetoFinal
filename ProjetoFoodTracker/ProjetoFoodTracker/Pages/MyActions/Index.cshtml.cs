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
    public class IndexModel : PageModel
    {
        private readonly ProjetoFoodTracker.Data.ApplicationDbContext _context;

        public IndexModel(ProjetoFoodTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Actions> Actions { get;set; }

        public async Task OnGetAsync()
        {
            Actions = await _context.Actions.ToListAsync();
        }
    }
}
