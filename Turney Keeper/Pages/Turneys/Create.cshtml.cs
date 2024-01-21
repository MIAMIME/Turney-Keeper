using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Turneys.Models;

namespace Turney_Keeper.Pages.Turneys
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;

        public CreateModel(Turney_Keeper.Data.Turney_KeeperContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Turney Turneys { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            int adminId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            Turneys.Admin_id = adminId;
            if (string.IsNullOrEmpty(Turneys.Title) || string.IsNullOrEmpty(Turneys.Description) || Turneys.Starting == null || Turneys.Ending == null)
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return Page();
            }
            _context.Turneys.Add(Turneys);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
