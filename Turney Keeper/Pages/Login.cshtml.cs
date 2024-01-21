using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turney_Keeper.Data;
using Turney_Keeper.Models;

namespace Turney_Keeper.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly Turney_KeeperContext _context;
        private readonly IConfiguration _configuration;

        public LoginModel(Turney_KeeperContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Users Users { get; set; } = default!;


        public async Task<IActionResult> OnPost()
        {
            string username = Request.Form["Username"];
            string password = Request.Form["Password"];

            using (var dbContext = new Turney_KeeperContext(new DbContextOptionsBuilder<Turney_KeeperContext>().UseSqlServer(_configuration.GetConnectionString("Turney_KeeperContext")).Options))
            {
                var user = dbContext.Users
                    .FromSqlInterpolated($"SELECT * FROM Users WHERE Username = {username} AND Password = {password}")
                    .FirstOrDefault();

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "B³¹d logowania. SprawdŸ dane i spróbuj ponownie.");
                    return Page();
                }
            }
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
    }

}
