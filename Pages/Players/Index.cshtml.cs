using League.Data;
using League.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public List<Player> Players { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedTeam { get; set; }

        public List<Team> Teams { get; set; }

        public SelectList AllTeams { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedPosition { get; set; }

        public List<string> Positions { get; set; }

        public SelectList AllPositions { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Teams = await _context.Teams.ToListAsync();
            Players = await _context.Players.ToListAsync();
            Positions = await _context.Players.Select(p => p.Position).Distinct().OrderBy(p => p).ToListAsync();

            var teamList = await  _context.Teams.OrderBy(t => t.TeamId).Select(t => t.TeamId).ToListAsync();
            AllTeams = new SelectList(teamList);

            AllPositions = new SelectList(Positions);

            if (!string.IsNullOrWhiteSpace(SelectedTeam) && SelectedTeam != "All")
            {
                Players = Players.Where(p => p.TeamId == SelectedTeam).ToList();
            }

            if (!string.IsNullOrWhiteSpace(SelectedPosition) && SelectedPosition != "All")
            {
                Players = Players.Where(p => p.Position == SelectedPosition).ToList();
            }

            if (!string.IsNullOrWhiteSpace(SearchName))
            {
                Players = Players.Where(p => p.Name.ToUpper().Contains(SearchName.ToUpper())).ToList();
            }

            switch (SortBy)
            {
                case "Name":
                    Players = Players.OrderBy(p => p.Name).ToList();
                    break;

                case "Number":
                    Players = Players.OrderBy(p => p.Number).ToList();
                    break;

                case "Position":
                    Players = Players.OrderBy(p => p.Position).ToList();
                    break;

                default:
                    Players = Players.OrderBy(p => p.Name).ToList();
                    break;
            }
        }
    }
}
