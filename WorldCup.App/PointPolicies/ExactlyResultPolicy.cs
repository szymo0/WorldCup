using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldCup.App.Data;

namespace WorldCup.App.PointPolicies
{
    public class ExactlyResultPolicy:IPointPolicy
    {
        public string Name => "Dokładny wynik";
        public string Description => $"Za wytypowanie dokładnego wyniku dostaje się {Points} punktów";
        public int Points => 2;

        public bool CanApply(Result result, Bet bet)
        {
            if (result.HomeGoals != bet.HomeGoals) return false;
            if (result.AwayGoals != bet.AwayGoals) return false;
            if (result.HasExtraTime != bet.HasExtraTime) return false;
            if (result.HasPenatly != bet.HasPenatly) return false;
            return true;
        }
    }
}
