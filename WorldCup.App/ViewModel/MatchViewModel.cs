using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;

namespace WorldCup.App.ViewModel
{
    public class MatchViewModel
    {
        public MatchViewModel()
        {

        }

        public MatchViewModel(Match match,Guid userId,IList<User> users)
        {
            Id = match.Id;
            Result = match.Result;
            AwayTeamName = match.AwayTeam.Name;
            HomeTeamName = match.HomeTeam.Name;
            Date = match.Date;
            BetForUser = match.Bets.Where(c => userId == c.UserId).OrderByDescending(c => c.TimeStamp).FirstOrDefault();
            Bets=new List<BetViewModel>();
            if (Result != null || Date.AddMinutes(-1) < DateTime.Now)
            {
                foreach (var matchBet in match.Bets)
                {
                    if (matchBet.UserId == userId) continue;
                    var user = users.FirstOrDefault(c => c.Id == matchBet.UserId);
                    Bets.Add(new BetViewModel(matchBet, user.DisplayName));
                }
                Bets = Bets.OrderByDescending(c => c.Bet?.Result?.AddedPoints ?? 0).ToList();

            }

        }
        public Guid Id { get; set; }
        public Result Result { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public Bet BetForUser { get; set; }
        public DateTime Date { get; set; }
        public List<BetViewModel> Bets { get; set; }
  
    }

    public class BetViewModel
    {
        public BetViewModel() { }

        public BetViewModel(Bet bet, string userName)
        {
            UserName = userName;
            Bet = bet;
        }

        public Bet Bet { get; set; }
        public string UserName { get; set; }
    }
}
