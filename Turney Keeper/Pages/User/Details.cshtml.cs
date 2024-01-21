using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Turney_Keeper.Models;

namespace Turney_Keeper.Pages.User
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;

        public DetailsModel(Turney_Keeper.Data.Turney_KeeperContext context)
        {
            _context = context;
        }

        public Users Users { get; set; } = default!;

        public IActionResult OnGet()
        {
            int adminId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);

            if (adminId == null)
            {
                return NotFound();
            }
            Users = _context.Users.FirstOrDefault(m => m.Id == adminId);

            if (Users == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
