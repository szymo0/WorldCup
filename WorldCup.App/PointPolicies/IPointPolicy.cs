using WorldCup.App.Data;

namespace WorldCup.App.PointPolicies
{
    public interface IPointPolicy
    {
        string Name { get; }
        string Description { get; }
        int Points { get;  }
        bool CanApply(Result result, Bet bet);
    }
}