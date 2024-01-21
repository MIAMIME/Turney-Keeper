using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turney_Keeper.Data;
using Microsoft.EntityFrameworkCore;
using Turneys.Models;

namespace Turney_Keeper.Pages.Turneys
{
    public class ScoreInputModel : PageModel
    {
        public Turney_KeeperContext _context;

        public ScoreInputModel(Turney_KeeperContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RoundNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MatchNumber { get; set; }

        [BindProperty]
        public int User1Score { get; set; }

        [BindProperty]
        public int User2Score { get; set; }

        [BindProperty]
        public int AdvanceUser { get; set; }

        [BindProperty]
        public BracketMatch match { get; set; }
        public IActionResult OnGet(int id, int roundNumber, int matchNumber)
        {
            Id = id;
            RoundNumber = roundNumber;
            MatchNumber = matchNumber;

            var turneys = _context.Turneys
                .Include(t => t.BracketRounds)
                .ThenInclude(r => r.Matches)
                .FirstOrDefault(t => t.Id == Id);

            if (turneys == null)
            {
                return NotFound();
            }

            var round = turneys.BracketRounds.FirstOrDefault(r => r.RoundNumber == RoundNumber);

            if (round == null || round.Matches == null)
            {
                return NotFound();
            }

            match = round.Matches.FirstOrDefault(m => m.MatchNumber == MatchNumber);

            if (match == null)
            {
                return NotFound();
            }

            return Page();
        }




        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var turneys = _context.Turneys.Include(t => t.BracketRounds).FirstOrDefault(t => t.Id == Id);
            var round = turneys?.BracketRounds.FirstOrDefault(r => r.RoundNumber == RoundNumber);
            if (round != null)
            {
                var match = round.Matches.FirstOrDefault(m => m.MatchNumber == MatchNumber);
            }

            if (turneys == null || round == null || match == null)
            {
                return NotFound();
            }

            match.User1Score = User1Score;
            match.User2Score = User2Score;
            var currentRoundMatchesCount = round.Matches.Count();

            if (currentRoundMatchesCount >= 2)
            {
                var previousRound = turneys.BracketRounds.FirstOrDefault(r => r.RoundNumber == RoundNumber - 1);

                if (previousRound != null && previousRound.Matches.Count() == 4)
                {
                    var advanceUserId = AdvanceUser;

                    var nextRoundNumber = RoundNumber + 1;
                    var nextRound = turneys.BracketRounds.FirstOrDefault(r => r.RoundNumber == nextRoundNumber);

                    if (nextRound != null)
                    {
                        if (nextRound != null)
                        {
                            var availableMatch = nextRound.Matches.FirstOrDefault(m => m.User1Id == null || m.User2Id == null);

                            if (availableMatch != null)
                            {
                                if (availableMatch.User1Id == null)
                                {
                                    availableMatch.User1Id = advanceUserId;
                                    availableMatch.User1Score = 0;
                                }
                                else
                                {
                                    availableMatch.User2Id = advanceUserId;
                                    availableMatch.User2Score = 0;
                                }

                            }
                        }
                    }
                }
            }

            _context.SaveChanges();

            return Page();
        }
        public string GetUserName(int userId)
        {
            var userName = _context.Users
               .Where(u => u.Id == userId)
               .Select(u => u.Username)
               .FirstOrDefault();
            return userName;
        }
    }

}
