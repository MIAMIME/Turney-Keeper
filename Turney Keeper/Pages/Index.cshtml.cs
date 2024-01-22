using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Turney_Keeper.Models;
using Turneys.Models;

namespace Turney_Keeper.Pages.Turneys
{
    public class IndexModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;

        public IndexModel(Turney_Keeper.Data.Turney_KeeperContext context)
        {
            _context = context;
        }
        public Users GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }
        public IList<Turney> Turneys { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Turneys != null)
            {
                Turneys = await _context.Turneys.ToListAsync();
            }
            IQueryable<Turney> turneysQuery = _context.Turneys.AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                turneysQuery = turneysQuery.Where(t => t.Title.Contains(SearchString));
            }

            Turneys = await turneysQuery.ToListAsync();
        }
    }
}
