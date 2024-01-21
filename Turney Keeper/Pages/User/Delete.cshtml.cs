using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turney_Keeper.Models;

namespace Turney_Keeper.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;

        public DeleteModel(Turney_Keeper.Data.Turney_KeeperContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Users Users { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (users == null)
            {
                return NotFound();
            }
            else
            {
                Users = users;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(id);

            if (users != null)
            {
                Users = users;
                _context.Users.Remove(Users);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
