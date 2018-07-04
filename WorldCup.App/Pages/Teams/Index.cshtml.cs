using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.Data;

namespace WorldCup.App.Pages
{
    public class TeamsModel : PageModel
    {
        private readonly WorldCup.App.Data.WorldCupContext _context;

        public TeamsModel(WorldCup.App.Data.WorldCupContext context)
        {
            _context = context;
        }

        public IList<Team> Team { get;set; }

        public async Task OnGetAsync()
        {
            Team = await _context.Teams.ToListAsync();
        }
    }
}
