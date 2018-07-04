using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldCup.App.Data;

namespace WorldCup.App.ViewModel
{
    public class BetDetailsViewModel
    {
        public List<BetItemViewModel> Bets { get; set; }
    }

    public class BetItemViewModel
    {
        public Bet Bet { get; set; }
        public string UserName { get; set; }
        public int? Points { get; set; }

    }
}
