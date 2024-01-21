using System.ComponentModel.DataAnnotations;

namespace Turneys.Models
{
    public class Turney
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [StringLength(300, MinimumLength = 10)]
        [Required]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Start of tournament")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Starting { get; set; }
        [Display(Name = "End of tournament")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Ending { get; set; }
        public DateTime Opened { get; set; } = DateTime.Now;
        public int[]? UserIds { get; set; }
        public int Admin_id { get; set; }
        [Required(ErrorMessage = "You need to enter how many users can Join.")]
        [Range(1, 100, ErrorMessage = "Availability must be between 2 and 99.")]
        public int Availability { get; set; }
        public string? password { get; set; }
        public List<BracketRound> BracketRounds { get; set; }
    }

    public class BracketRound
    {

        public int Id { get; set; }
        public int TurneyId { get; set; }
        public int RoundNumber { get; set; }
        public List<BracketMatch> Matches { get; set; }
    }

    public class BracketMatch
    {
        [Key]
        public int MatchNumber { get; set; }
        public int User1Id { get; set; }
        public int User1Score { get; set; }
        public int? User2Id { get; set; }
        public int User2Score { get; set; }
    }
}