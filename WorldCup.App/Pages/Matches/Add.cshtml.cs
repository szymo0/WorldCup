using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorldCup.App.Data;

namespace WorldCup.App.Pages.Matches
{
    public class AddModel : PageModel
    {
        private readonly WorldCupContext _context;

        public AddModel(WorldCupContext context)
        {
            _context = context;
        }
        public IEnumerable<Team> Teams;
        [BindProperty]
        public Guid HomeTeamId { get; set; }
        [BindProperty]
        public Guid AwayTeamId { get; set; }

        [BindProperty]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Date { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Teams = _context.Teams.OrderBy(c=>c.Name).ToList();
            Date=DateTime.Now;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Match match = new Match
            {
                Date = Date,
                AwayTeam = _context.Teams.First(c => c.Id == AwayTeamId),
                HomeTeam = _context.Teams.First(c => c.Id == HomeTeamId)
            };
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return RedirectToPage("../Dashboard/Index");
        }
    }
}