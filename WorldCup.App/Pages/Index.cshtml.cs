using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorldCup.App.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            //var claims = (User.Claims as ClaimsIdentity);
            //claims.FindFirst(c => c.Type == "");
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Dashboard/Index");
            }

            return RedirectToPage("/About");
        }
    }
}
