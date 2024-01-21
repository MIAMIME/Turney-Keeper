using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turneys.Models;

namespace Turney_Keeper.Pages.Turneys
{
    public class DeleteModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;

        public DeleteModel(Turney_Keeper.Data.Turney_KeeperContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Turney Turneys { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Turneys == null)
            {
                return NotFound();
            }

            var turneys = await _context.Turneys.FirstOrDefaultAsync(m => m.Id == id);

            if (turneys == null)
            {
                return NotFound();
            }
            else
            {
                Turneys = turneys;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Turneys == null)
            {
                return NotFound();
            }
            var turneys = await _context.Turneys.FindAsync(id);

            if (turneys != null)
            {
                Turneys = turneys;
                _context.Turneys.Remove(Turneys);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
