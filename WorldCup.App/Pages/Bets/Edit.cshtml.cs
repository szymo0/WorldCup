using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorldCup.App.ViewModel;

namespace WorldCup.App.Pages.Bets
{
    public class EditModel : PageModel
    {
        private readonly WorldCup.App.Data.WorldCupContext _context;

        public EditModel(WorldCup.App.Data.WorldCupContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditBetViewModel EditBetViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if(!User.Identity.IsAuthenticated)
                return NotFound();
            var userId = Guid.Parse(User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value);
            if (id == null)
            {
                return NotFound();
            }
            var bet = await _context.Bets.Include(b=>b.Match).ThenInclude(m=>m.AwayTeam).Include(b => b.Match).ThenInclude(m=>m.HomeTeam).FirstOrDefaultAsync(m => m.Id == id);

            EditBetViewModel =new EditBetViewModel(bet,userId);

            if (EditBetViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bet =await EditBetViewModel.CreateBet(_context);
            if(bet.Match.Date.AddMinutes(1)<DateTime.Now)
                throw new Exception("Cwaniaku nie ma tak dobrze:)");
            _context.Attach(bet).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditBetViewModelExists(EditBetViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index");
        }

        private bool EditBetViewModelExists(Guid id)
        {
            return _context.Bets.Any(e => e.Id == id);
        }
    }
}
