using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;
using WorldCup.App.ViewModel;

namespace WorldCup.App.Pages.Matches
{
    public class BetForModel : PageModel
    {
        private readonly WorldCup.App.Data.WorldCupContext _context;

        public BetForModel(WorldCup.App.Data.WorldCupContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid? matchId)
        {
            if (!User.Identity.IsAuthenticated)
                return NotFound();
            Users = await _context.Users.ToListAsync();
            var match = await _context.Matches.Include(c=>c.HomeTeam).Include(c=>c.AwayTeam).FirstOrDefaultAsync(m => m.Id == matchId);
            var userId = Guid.Parse(User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value);
            Bet =new CreateBetViewModel(match, userId);
            if (match.Date.AddMinutes(1) < DateTime.Now)
                NotFound();
            return Page();
        }



        [BindProperty]
        public CreateBetViewModel Bet { get; set; }
        public List<User> Users { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {

            var name = User.FindFirst("name").Value;

            var bet =await Bet.CreateBet(_context);

            //if (bet.Match.Date.AddMinutes(1) <= DateTime.Now)
            //    return NotFound();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Data.User user = await _context.Users.FirstOrDefaultAsync(c => c.Id == bet.UserId);
            if (user == null)
            {
                _context.Users.Add(new User
                {
                    Id = bet.UserId,
                    Points = 0,
                    DisplayName = name,
                    TimeStamp = DateTime.Now
                });
            }

            using (var trans = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                if (await _context.Bets.AnyAsync(c => c.Match.Id == Bet.MatchId && c.UserId == Bet.UserId))
                {
                    trans.Rollback();
                    return NotFound();
                }

                _context.Bets.Add(bet);
                await _context.SaveChangesAsync();
                trans.Commit();
            }

            return RedirectToPage("../Dashboard/Index");
        }
    }
}