using League.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace League.Pages.Teams
{
    public class TeamModel : PageModel
    {
        private readonly LeagueContext _context;

        public Models.Team Team { get; set; }

        public TeamModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string id)
        {
            Team = await _context.Teams
                    .Include(t => t.Players)
                    .Include(t => t.Division)
                    .AsNoTracking()
                    .Where(t => t.TeamId == id)
                    .FirstOrDefaultAsync();
        }
    }
}
