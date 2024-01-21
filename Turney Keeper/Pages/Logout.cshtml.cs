using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turney_Keeper.Data;

namespace Turney_Keeper.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly Turney_KeeperContext _context;
        private readonly IConfiguration _configuration;

        public LogoutModel(Turney_KeeperContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Page();
        }
    }

}
