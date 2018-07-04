using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldCup.App.Data;

namespace WorldCup.App.ViewModel
{
    public class AddResultViewModel:IResult
    {
        public AddResultViewModel() { }

        public AddResultViewModel(Match match)
        {
            MatchId = match.Id;
            HomeTeamName = match.HomeTeam.Name;
            AwayTeamName = match.AwayTeam.Name;
        }

        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
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

        public Result GetResult()
        {
            return new Result
            {
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
                Id = Guid.NewGuid(),
                TimeStamp = DateTime.Now

            };
        }
    }
}
