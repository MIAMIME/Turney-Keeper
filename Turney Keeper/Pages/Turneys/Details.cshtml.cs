using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turneys.Models;

namespace Turney_Keeper.Pages.Turneys
{
    public class DetailsModel : PageModel
    {
        public Turney_Keeper.Data.Turney_KeeperContext _context;

        public DetailsModel(Turney_Keeper.Data.Turney_KeeperContext context)
        {
            _context = context;
        }
        public Turney Turneys { get; set; } = default!;
        public BracketRound BracketRounds { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Turneys == null)
            {
                return NotFound();
            }
            Turneys = await _context.Turneys
            .Include(t => t.BracketRounds)
            .ThenInclude(r => r.Matches)
            .FirstOrDefaultAsync(t => t.Id == id);


            if (Turneys == null)
            {
                return NotFound();
            }

            if(Turneys.Starting.Date<DateTime.Now && !Turneys.BracketRounds.Any(br => br.TurneyId == Turneys.Id))
            {
                if (Turneys.UserIds != null)
                {
                    var initialRound = new BracketRound
                    {
                        TurneyId = Turneys.Id,
                        RoundNumber = 1,
                        Matches = new List<BracketMatch>()
                    };

                    for (int i = 0; i < Turneys.UserIds.Length; i += 2)
                    {
                        var match = new BracketMatch
                        {
                            User1Id = Turneys.UserIds[i],
                            User2Id = (i + 1 < Turneys.UserIds.Length) ? Turneys.UserIds[i + 1] : (int?)null,
                            User1Score = 0,
                            User2Score = 0,
                        };

                        initialRound.Matches.Add(match);
                    }

                    Turneys.BracketRounds = new List<BracketRound> { initialRound };

                    await _context.SaveChangesAsync();
                }
                }
                return Page(); ;
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Turneys == null)
            {
                return NotFound();
            }

            var turneys = await _context.Turneys.FirstOrDefaultAsync(m => m.Id == id);
            if (turneys == null)
            {
                return NotFound();
            }
            else
            {
                Turneys = turneys;
            }
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (Request.Form["joinButton"].Any())
            {
                if (string.IsNullOrEmpty(Turneys.password))
                {
                    if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int userIdInt))
                    {
                        if ((Turneys.UserIds == null || !Turneys.UserIds.Contains(userIdInt)) && (Turneys.UserIds?.Length ?? 0) < Turneys.Availability)
                        {
                            Turneys.UserIds ??= new int[] { };
                            Turneys.UserIds = Turneys.UserIds.Concat(new[] { userIdInt }).ToArray();

                            await _context.SaveChangesAsync();
                        }
                        return Page();
                    }
                }
                else
                {
                    var enteredPassword = Request.Form["Turneys.password"];

                    if (enteredPassword == Turneys.password)
                    {
                        if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int userIdInt))
                        {
                            if ((Turneys.UserIds == null || !Turneys.UserIds.Contains(userIdInt)) && (Turneys.UserIds?.Length ?? 0) < Turneys.Availability)
                            {
                                Turneys.UserIds ??= new int[] { };
                                Turneys.UserIds = Turneys.UserIds.Concat(new[] { userIdInt }).ToArray();

                                await _context.SaveChangesAsync();
                            }

                            return Page();
                        }
                    }
                }
            }

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