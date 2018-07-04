using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorldCup.App.Data;
using WorldCup.App.ViewModel;

namespace WorldCup.App.Pages.Bets
{
    public class ListModel : PageModel
    {
        private readonly WorldCupContext _dbContext;

        public ListModel(WorldCupContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BetDetailsViewModel Bets;
        public async Task<IActionResult> OnGetAsync(Guid matchId)
        {
            var matchEntity = await _dbContext.Matches
                .Include(c => c.Result)
                .Include(match => match.AwayTeam)
                .Include(match => match.HomeTeam)
                .Include(match => match.Bets)
                .FirstOrDefaultAsync(match => match.Id == matchId);
            Bets= new BetDetailsViewModel();
            Bets.Bets=new List<BetItemViewModel>();

            foreach (var matchEntityBet in matchEntity.Bets)
            {
                Bets.Bets.Add(new BetItemViewModel
                {
                    Bet = matchEntityBet,
                    UserName = (await GetUserById(matchEntityBet.UserId)).DisplayName
                });
            }

            return Page();
        }

        private async Task<User> GetUserById(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == userId);
        }
    }
}