using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;
using WorldCup.App.ViewModel;

namespace WorldCup.App.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly WorldCupContext _context;

        public DashboardModel(WorldCupContext context)
        {
            _context = context;
        }
        public IList<MatchViewModel> MatchesToPlay { get; set; }
        public IList<MatchViewModel> MatchesPlayed { get; set; }
        public IList<MatchViewModel> MatchesPlaying { get; set; }
        public IList<User> Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if(!User.Identity.IsAuthenticated)
                throw new Exception("nice try!:)");
            var matches = await _context.Matches.Include(match => match.Bets)
                .Include(match => match.Result)
                .Include(match => match.HomeTeam)
                .Include(match => match.Result)
                .Include(match => match.AwayTeam)
                .Include(match => match.Bets)
                .ThenInclude(c=>c.Result)
                .OrderBy(match => match.Date ).ToListAsync();
            Users = await _context.Users
                //.Include(user => user.Bets)
                //.Include(user => user.PointResults)
                .OrderByDescending(user => user.Points)
                .ToListAsync();

            MatchesToPlay = matches.Where(c => c.Date > DateTime.Now).Select(c => new MatchViewModel(c, GetUserId(), Users)).OrderBy(c=>c.Date).ToList();
            MatchesPlayed = matches.Where(c => c.Date.AddHours(2) < DateTime.Now).Select(c => new MatchViewModel(c, GetUserId(), Users)).OrderByDescending(c=>c.Date).ToList();
            MatchesPlaying = matches.Where(c => c.Date.AddHours(2) > DateTime.Now && c.Date < DateTime.Now).Select(c => new MatchViewModel(c, GetUserId(), Users)).ToList();


            return Page();

        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value);
        }
    }
}