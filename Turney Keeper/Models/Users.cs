using System.ComponentModel.DataAnnotations;

namespace Turney_Keeper.Models
{
    public class Users
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Username { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Surname { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Password { get; set; } = string.Empty;

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Email { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
