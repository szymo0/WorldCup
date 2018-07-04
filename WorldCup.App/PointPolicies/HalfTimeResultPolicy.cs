using WorldCup.App.Data;

namespace WorldCup.App.PointPolicies
{
    public class HalfTimeResultPolicy:IPointPolicy
    {
        public string Name => "Wytypuj wynik do przerwy";
        public string Description => $"Za wytypowanie dokładnego wyniku do przerwy jest {Points} punktów";
        public int Points => 1;
        public bool CanApply(Result result, Bet bet)
        {
            return result.HomeGoalsInFirstHalf == bet.HomeGoalsInFirstHalf &&
                   result.AwayGoalsInFirstHalf == bet.AwayGoalsInFirstHalf;
        }
    }
}