namespace WorldCup.App.Data
{
    public interface IResult
    {
        int AwayGoals { get; set; }
        int? AwayGoalsInExtraTime { get; set; }
        int AwayGoalsInFirstHalf { get; set; }
        int? AwayPenatly { get; set; }
        bool HasExtraTime { get; set; }
        bool HasPenatly { get; set; }
        int HomeGoals { get; set; }
        int? HomeGoalsInExtraTime { get; set; }
        int HomeGoalsInFirstHalf { get; set; }
        int? HomePenatly { get; set; }
    }
}