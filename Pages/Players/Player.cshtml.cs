using League.Data;
using League.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace League.Pages.Players
{
    public class PlayerModel : PageModel
    {
        private readonly LeagueContext _context;

        public Player Player { get; set; }

        public PlayerModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string id)
        {
            Player = await _context.Players.Where(p => p.PlayerId == id).FirstOrDefaultAsync();
        }
    }
}
