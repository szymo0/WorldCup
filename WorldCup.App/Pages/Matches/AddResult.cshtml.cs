using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;
using WorldCup.App.PointPolicies;
using WorldCup.App.ViewModel;

namespace WorldCup.App.Pages.Matches
{
    [Authorize("Admin")]
    public class AddResultModel : PageModel
    {
        private readonly WorldCupContext _context;

        public AddResultModel(WorldCupContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(Guid? matchId)
        {
            var matchEntity = await GetMatchById(matchId);
            ResultModel=new AddResultViewModel(matchEntity);
            return Page();
        }

        private async Task<Match> GetMatchById(Guid? matchId)
        {
            var matchEntity = await _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .Include(match=>match.Bets)
                .FirstOrDefaultAsync(match => match.Id == matchId);
            return matchEntity;
        }

        [BindProperty]
        public AddResultViewModel ResultModel { get; set; }

        private List<IPointPolicy> PointPoliciese = new List<IPointPolicy>
        {
            new ExactlyResultPolicy(),
            new HalfTimeResultPolicy(),
            new WinPolicy()
        };

        private async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var matchEntity = await GetMatchById(ResultModel.MatchId);
            matchEntity.Result = ResultModel.GetResult();

            PointHistory pointHistory = new PointHistory
            {
                Id = Guid.NewGuid(),
                TimeStamp = DateTime.Now
            };
            List<PointResult> results=new List<PointResult>();
            foreach (var matchEntityBet in matchEntity.Bets)
            {
                List< PointPolicy > policies=new List<PointPolicy>();
                foreach (var pointPolicy in PointPoliciese)
                {

                    PointPolicy tmp = new PointPolicy
                    {
                        Applied = pointPolicy.CanApply(matchEntity.Result, matchEntityBet),
                        Description = pointPolicy.Description,
                        Name = pointPolicy.Name,
                        Id = Guid.NewGuid(),
                        Points = pointPolicy.Points,
                        PolicyType = PolicyType.Match,
                        TimeStamp = DateTime.Now

                    };
                    policies.Add(tmp);

                }

                var user = await GetUserById(matchEntityBet.UserId);
                matchEntityBet.Result = new PointResult
                {
                    User = user,
                    Id = Guid.NewGuid(),
                    AddedPoints = policies.Sum(c => c.Applied ? c.Points.Value : 0),
                    PointPolicies = policies,
                    SumPoints = user.Points + policies.Sum(c => c.Applied ? c.Points.Value : 0),
                    TimeStamp = DateTime.Now

                };
                user.Points = user.Points + policies.Sum(c => c.Applied ? c.Points.Value : 0);
                results.Add(matchEntityBet.Result);
            }
            pointHistory.PointResults = results;
            _context.PointHistories.Add(pointHistory);
            await _context.SaveChangesAsync();
            return RedirectToPage("../Dashboard/Index");
        }
        
    }
}