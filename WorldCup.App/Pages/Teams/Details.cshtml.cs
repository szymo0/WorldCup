using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;
using WorldCup.App.ViewModel;

namespace WorldCup.App.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly WorldCup.App.Data.WorldCupContext _context;

        public DetailsModel(WorldCup.App.Data.WorldCupContext context)
        {
            _context = context;
        }

        public Team Team { get; set; }
        public IList<MatchViewModel> Matches { get; set; }
        public Guid UserId {get; private set;}

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            UserId = Guid.Parse(User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value);

            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
            
            if (Team == null)
            {
                return NotFound();
            }

            var user =await _context.Users.ToListAsync();
            var matches = await _context.Matches.Include(c => c.AwayTeam)
                .Include(c => c.HomeTeam)
                .Include(c => c.Bets)
                    .ThenInclude(bets => bets.Result)
                .Include(c => c.Result)
                .AsNoTracking().Where(c => c.HomeTeam.Id == Team.Id || c.AwayTeam.Id == Team.Id)
                .OrderBy(c=>c.Date).ToListAsync();
            Matches = matches.Select(c=>new MatchViewModel(c,UserId,user))
                    .ToList();
              //matches =matches..ToList();



            return Page();
        }
    }
}
