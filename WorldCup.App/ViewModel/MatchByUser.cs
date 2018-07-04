using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;

namespace WorldCup.App.ViewModel
{
    public class MatchByUser
    {

        public static async Task<IList<MatchByUser>> GetByUserId(WorldCupContext context,Guid userId)
        {
            List<MatchByUser> result=new List<MatchByUser>();
            var matches=await context.Matches.Include(match => match.Bets).Include(match => match.Result).Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam).ToListAsync();
            foreach (var match in matches)
            {
                var bet = match.Bets.FirstOrDefault(c => c.UserId == userId);
                result.Add(new MatchByUser{Bet=bet,Match = match});


            }

            return result;
        }


        public Match Match { get; set; }
        public Bet Bet { get; set; }

    }
}
