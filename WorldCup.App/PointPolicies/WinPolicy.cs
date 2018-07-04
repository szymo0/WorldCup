using WorldCup.App.Data;

namespace WorldCup.App.PointPolicies
{
    public class WinPolicy : IPointPolicy
    {
        public string Name => "Wytypuj zwycięstwe ";
        public string Description => $"Za wytypowanie zwycięstwy jest {Points} punktów";
        public int Points => 2;

        public bool CanApply(Result result, Bet bet)
        {
            return result.GetResult() == bet.GetResult();
        }
    }
}