using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldCup.App.Data;

namespace WorldCup.App.PointPolicies
{
    public class ExtraTimePoints:IPointPolicy
    {
        public string Name => "Punkty za dogrywkę";
        public string Description => "Trafienie w fazie pucharowej, że w meczu będzie dogrywka";
        public int Points => 1;
        public bool CanApply(Result result, Bet bet)
        {
            return result.AwayGoals==result.HomeGoals && result.HasExtraTime && bet.HasExtraTime;
        }
    }
}
