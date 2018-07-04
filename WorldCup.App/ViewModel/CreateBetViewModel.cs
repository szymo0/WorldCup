using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;

namespace WorldCup.App.ViewModel
{
    public class CreateBetViewModel:IResult
    {
        public CreateBetViewModel() { }
        public CreateBetViewModel(Match match,Guid userId)
        {
            Title = $"Wytypuj wynik meczu: {match.HomeTeam.Name} - {match.AwayTeam.Name}";
            Desciption =
                $"Mecz {match.HomeTeam.Name} - {match.AwayTeam.Name} który odbędzie się {match.Date.ToLongDateString()}";
            MatchId = match.Id;
            UserId = userId;
            HomeTeamName = match.HomeTeam.Name;
            AwayTeamName = match.AwayTeam.Name;
        }
        public string  HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }

        [HiddenInput]
        public Guid UserId { get; set; }
        [HiddenInput]
        public Guid MatchId { get; set; }

        [DisplayName("Gole gości po 90 min")]
        [Required]
        public int AwayGoals { get; set; }
        [DisplayName("Gole gości po dogrywce")]
        public int? AwayGoalsInExtraTime { get; set; }
        [DisplayName("Gole gości po 45 min")]
        [Required]
        public int AwayGoalsInFirstHalf { get; set; }
        [DisplayName("Karne strzelone przez gości")]
        public int? AwayPenatly { get; set; }
        [DisplayName("Czy dogrywka")]
        public bool HasExtraTime { get; set; }
        [DisplayName("Czy karne")]
        public bool HasPenatly { get; set; }
        [DisplayName("Gole gospodarzy po 90 min")]
        public int HomeGoals { get; set; }
        [DisplayName("Gole gospodarzy po dogrywce")]
        public int? HomeGoalsInExtraTime { get; set; }
        [DisplayName("Gole gospodarzy po 45 min")]
        public int HomeGoalsInFirstHalf { get; set; }
        [DisplayName("Karne strzelone przez gospodarzy")]
        public int? HomePenatly { get; set; }

        public async Task<Bet> CreateBet(WorldCupContext context)
        {
            var match = await context.Matches.FirstOrDefaultAsync(c => c.Id == MatchId);
            if (match == null)
                throw new Exception("No match found for MatchId");

            return new Bet
            {
                UserId = UserId,
                AwayGoals = AwayGoals,
                AwayGoalsInExtraTime = AwayGoalsInExtraTime,
                AwayGoalsInFirstHalf = AwayGoalsInFirstHalf,
                AwayPenatly = AwayPenatly,
                BetId = Guid.NewGuid(),
                HasExtraTime = HasExtraTime,
                HasPenatly = HasPenatly,
                HomeGoals = HomeGoals,
                HomeGoalsInExtraTime = HomeGoalsInExtraTime,
                HomeGoalsInFirstHalf = HomeGoalsInFirstHalf,
                HomePenatly = HomePenatly,
                Id = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                Match = match
            };
        }
    }
    public class EditBetViewModel : IResult
    {
        public EditBetViewModel() { }
        public EditBetViewModel(Bet bet, Guid userId)
        {
            Title = $"Wytypuj wynik meczu: {bet.Match.HomeTeam.Name} - {bet.Match.AwayTeam.Name}";
            Desciption =
                $"Mecz {bet.Match.HomeTeam.Name} - {bet.Match.AwayTeam.Name} który odbędzie się {bet.Match.Date.ToLongDateString()}";
            MatchId = bet.Match.Id;
            UserId = userId;

            Id = bet.Id;
            BetId = bet.Id;
            AwayGoals = bet.AwayGoals;
            AwayGoalsInExtraTime = bet.AwayGoalsInExtraTime;
            AwayGoalsInFirstHalf = bet.AwayGoalsInFirstHalf;
            AwayPenatly = bet.AwayPenatly;
            HasExtraTime = bet.HasExtraTime;
            HasPenatly = bet.HasPenatly;
            HomeGoals = bet.HomeGoals;
            HomeGoalsInExtraTime = bet.HomeGoalsInExtraTime;
            HomeGoalsInFirstHalf = bet.HomeGoalsInFirstHalf;
            HomePenatly = bet.HomePenatly;
            HomeTeamName = bet.Match.HomeTeam.Name;
            AwayTeamName = bet.Match.AwayTeam.Name;
        }

        public string Title { get; set; }
        public string Desciption { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        [HiddenInput]
        public Guid UserId { get; set; }
        [HiddenInput]
        public Guid MatchId { get; set; }

        [HiddenInput]
        public Guid Id { get; set; }
        [HiddenInput]
        public Guid BetId { get; set; }


        [DisplayName("Gole gości po 90 min")]
        [Required]
        public int AwayGoals { get; set; }
        [DisplayName("Gole gości po dogrywce")]
        public int? AwayGoalsInExtraTime { get; set; }
        [DisplayName("Gole gości po 45 min")]
        [Required]
        public int AwayGoalsInFirstHalf { get; set; }
        [DisplayName("Karne strzelone przez gości")]
        public int? AwayPenatly { get; set; }
        [DisplayName("Czy dogrywka")]
        public bool HasExtraTime { get; set; }
        [DisplayName("Czy karne")]
        public bool HasPenatly { get; set; }
        [DisplayName("Gole gospodarzy po 90 min")]
        public int HomeGoals { get; set; }
        [DisplayName("Gole gospodarzy po dogrywce")]
        public int? HomeGoalsInExtraTime { get; set; }
        [DisplayName("Gole gospodarzy po 45 min")]
        public int HomeGoalsInFirstHalf { get; set; }
        [DisplayName("Karne strzelone przez gospodarzy")]
        public int? HomePenatly { get; set; }

        public async Task<Bet> CreateBet(WorldCupContext context)
        {
            var match = await context.Matches.FirstOrDefaultAsync(c => c.Id == MatchId);
            if (match == null)
                throw new Exception("No match found for MatchId");

            return new Bet
            {
                UserId = UserId,
                BetId = BetId,
                Id=Id,
                AwayGoals = AwayGoals,
                AwayGoalsInExtraTime = AwayGoalsInExtraTime,
                AwayGoalsInFirstHalf = AwayGoalsInFirstHalf,
                AwayPenatly = AwayPenatly,
                HasExtraTime = HasExtraTime,
                HasPenatly = HasPenatly,
                HomeGoals = HomeGoals,
                HomeGoalsInExtraTime = HomeGoalsInExtraTime,
                HomeGoalsInFirstHalf = HomeGoalsInFirstHalf,
                HomePenatly = HomePenatly,
                TimeStamp = DateTime.Now,
                Match = match
            };
        }
    }
}
