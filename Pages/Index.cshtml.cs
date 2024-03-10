using League.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace League.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public Models.League League { get; set; }

        public IndexModel(LeagueContext context)
        {
          _context = context;
        }

        public async Task OnGetAsync()
        {
            League = await _context.Leagues.Where(l => l.LeagueId.ToUpper() == "NFL").FirstOrDefaultAsync();
        }
    }
}
