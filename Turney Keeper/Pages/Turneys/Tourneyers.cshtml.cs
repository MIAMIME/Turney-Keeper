using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turneys.Models;

namespace Turney_Keeper.Pages.Turneys
{
    public class TourneyersModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;

        public TourneyersModel(Turney_Keeper.Data.Turney_KeeperContext context)
        {
            _context = context;
        }
        public List<string> UserNames { get; set; }
        public int AdminId { get; set; }

        public Turney Turneys { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Turneys = await _context.Turneys
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
            if (Turneys.UserIds == null)
            {
                return (Page());
            }

            if (Turneys != null)
            {
                var tourneyers = await _context.Users
                    .Where(u => Turneys.UserIds.Contains(u.Id))
                    .Select(u => u.Username)
                    .ToListAsync();

                UserNames = tourneyers;
            }
            if (id == null)
            {
                return NotFound();
            }

            Turneys = await _context.Turneys.FirstOrDefaultAsync(m => m.Id == id);

            if (Turneys == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostRemoveTourneyerAsync(string username)
        {
            if (User.Identity.IsAuthenticated && User.FindFirst(ClaimTypes.Name)?.Value == Turneys.Admin_id.ToString())
            {
                var userToRemove = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

                if (userToRemove != null)
                {
                    Turneys.UserIds = Turneys.UserIds?.Where(id => id != userToRemove.Id).ToArray();

                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}
