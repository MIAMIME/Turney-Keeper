using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turney_Keeper.Data;
using Turney_Keeper.Models;

namespace Turney_Keeper.Pages.User
{

    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Turney_KeeperContext _context;
        [BindProperty]
        public Users Users { get; set; }
        [BindProperty]
        public string OldPassword { get; set; }

        public EditModel(Turney_KeeperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Users = new Users();
        }

        public async Task<IActionResult> OnPost()
        {
            int UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            if (Request.Form["changeUsername"].Any())
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == Users.Username && u.Id != UserId);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Users.Username", "This username is already taken.");
                    return Page();
                }
                var userToUpdate = await _context.Users.FindAsync(UserId);

                userToUpdate.Username = Users.Username;

                _context.Attach(userToUpdate).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return RedirectToPage("/User/Details");
            }
            else if (Request.Form["changeNameAndSurname"].Any())
            {

                var userToUpdate = await _context.Users.FindAsync(UserId);

                if (userToUpdate == null)
                {
                    return NotFound();
                }

                userToUpdate.Name = Users.Name;
                userToUpdate.Surname = Users.Surname;

                _context.Attach(userToUpdate).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return RedirectToPage("/User/Details");
            }
            else if (Request.Form["changePassword"].Any())
            {

                var userToUpdate = await _context.Users.FindAsync(UserId);

                if (userToUpdate == null)
                {
                    return NotFound();
                }


                if (!string.Equals(userToUpdate.Password, OldPassword, StringComparison.Ordinal))
                {
                    ModelState.AddModelError("OldPassword", "Old password is incorrect.");
                    return Page();
                }
                else if (Users.Password != Users.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                    return Page();
                }

                userToUpdate.Password = Users.Password;
                userToUpdate.ConfirmPassword = Users.ConfirmPassword;
                _context.Attach(userToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToPage("/User/Details");
            }
            else if (Request.Form["changeEmail"].Any())
            {

                var userToUpdate = await _context.Users.FindAsync(UserId);

                if (userToUpdate == null)
                {
                    return NotFound();
                }

                if (!IsValidEmail(Users.Email))
                {
                    ModelState.AddModelError(nameof(Users.Email), "Invalid email address format.");
                    return Page();
                }

                userToUpdate.Email = Users.Email;
                _context.Attach(userToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/User/Details");
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
