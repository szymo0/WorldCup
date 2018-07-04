using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.ViewModel;

namespace WorldCup.App.Data
{
    public class WorldCupContext : DbContext
    {
        public WorldCupContext(DbContextOptions<WorldCupContext> options) : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }
        public DbSet<PointPolicy> PointPolicies { get; set; }
        public DbSet<PointResult> PointResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

    public class Match
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual Result Result { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
        public DateTime TimeStamp { get; set; }


    }

    public class Team
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Nazwa skrócona")]
        public string ShortName { get; set; }
        [DisplayName("Data aktualizacji")]
        public DateTime TimeStamp { get; set; }

    }

    public class Bet : IResult
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [HiddenInput]
        public Guid BetId { get; set; }
        [HiddenInput]
        public Guid UserId { get; set; }
        public virtual Match Match { get; set; }
        public int AwayGoals { get; set; }
        public int? AwayGoalsInExtraTime { get; set; }
        public int AwayGoalsInFirstHalf { get; set; }
        public int? AwayPenatly { get; set; }
        public bool HasExtraTime { get; set; }
        public bool HasPenatly { get; set; }
        public int HomeGoals { get; set; }
        public int? HomeGoalsInExtraTime { get; set; }
        public int HomeGoalsInFirstHalf { get; set; }
        public int? HomePenatly { get; set; }
        [HiddenInput]
        public DateTime TimeStamp { get; set; }
        public virtual PointResult Result { get; set; }

        public ResultEnum GetResult()
        {
            if (HomeGoals == AwayGoals)
            {
                return ResultEnum.Draw;

            }
            else
            {
                if (HomeGoals > AwayGoals)
                    return ResultEnum.HomeWin;
                return ResultEnum.AwayWin;

            }
        }
    }

    public enum ResultEnum
    {
        HomeWin,
        AwayWin,
        Draw
    }

    public class Result : IResult
    {
        public Guid Id { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public bool HasExtraTime { get; set; }
        public int? HomeGoalsInExtraTime { get; set; }
        public int? AwayGoalsInExtraTime { get; set; }
        public bool HasPenatly { get; set; }
        public int? HomePenatly { get; set; }
        public int? AwayPenatly { get; set; }
        public int HomeGoalsInFirstHalf { get; set; }
        public int AwayGoalsInFirstHalf { get; set; }
        public DateTime TimeStamp { get; set; }

        public ResultEnum GetResult()
        {
            if (HomeGoals == AwayGoals)
            {
                if (HasExtraTime)
                {
                    if (HomeGoalsInExtraTime == AwayGoalsInExtraTime)
                    {
                        if (HasPenatly)
                        {
                            if (HomePenatly > AwayPenatly)
                                return ResultEnum.HomeWin;
                            return ResultEnum.AwayWin;
                        }

                        return ResultEnum.Draw;
                    }

                    if (HomeGoalsInExtraTime > AwayGoalsInExtraTime)
                        return ResultEnum.HomeWin;
                    return ResultEnum.AwayWin;
                }
                else
                {
                    return ResultEnum.Draw;
                }
            }
            else
            {
                if (HomeGoals > AwayGoals)
                    return ResultEnum.HomeWin;
                return ResultEnum.AwayWin;

            }
        }
    }

    public class User
    {
        [HiddenInput]
        public Guid Id { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
        public int Points { get; set; }
        [HiddenInput]
        public DateTime TimeStamp { get; set; }
        public string DisplayName { get; set; }
        public ICollection<PointResult> PointResults { get; set; }
    }


    public class PointHistory
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [HiddenInput]
        public DateTime TimeStamp { get; set; }
        public ICollection<PointResult> PointResults { get; set; }
    }

    public class PointResult
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        public int AddedPoints { get; set; }
        public int SumPoints { get; set; }
        public ICollection<PointPolicy> PointPolicies { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class PointPolicy
    {
        [HiddenInput]
        public Guid Id { get; set; }
        public PolicyType PolicyType { get; set; }
        public string Name { get; set; }
        public bool Applied { get; set; }
        public int? Points { get; set; }
        public string Description { get; set; }
        [HiddenInput]
        public DateTime TimeStamp { get; set; }

    }

    public enum PolicyType
    {
        Match,
        Groupe
    }

}
