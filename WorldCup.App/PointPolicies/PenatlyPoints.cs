using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldCup.App.Data;

namespace WorldCup.App.PointPolicies
{
    public class PenatlyPoints:IPointPolicy
    {
        public string Name => "Punkty za karne";
        public string Description => "Trafienie w fazie pucharowej, że w meczu będą karne";
        public int Points => 1;
        public bool CanApply(Result result, Bet bet)
        {
            return result.AwayGoals==result.HomeGoals && bet.HasExtraTime && result.HasPenatly && bet.HasPenatly;
        }
    }
}
