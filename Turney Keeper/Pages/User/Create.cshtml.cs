using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turney_Keeper.Data;
using Turney_Keeper.Models;

namespace Turney_Keeper.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly Turney_Keeper.Data.Turney_KeeperContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(Turney_KeeperContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Users Users { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_context.Users.Any(u => u.Username == Users.Username))
            {
                ModelState.AddModelError(nameof(Users.Username), "Username already exists. Please choose a different one.");
                return Page();
            }
            if (!IsValidEmail(Users.Email))
            {
                ModelState.AddModelError(nameof(Users.Email), "Invalid email address format.");
                return Page();
            }
            _context.Users.Add(Users);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
