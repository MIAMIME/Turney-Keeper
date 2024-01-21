using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turneys.Models;

namespace Turney_Keeper.Pages.Turneys
{
    public class EditModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;

        public EditModel(Turney_Keeper.Data.Turney_KeeperContext context)
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
            Turneys = turneys;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Turneys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurneysExists(Turneys.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TurneysExists(int id)
        {
            return (_context.Turneys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
