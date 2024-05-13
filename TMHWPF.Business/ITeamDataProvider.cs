using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public interface ITeamDataProvider
    {
        Task<List<Club>> LoadClubsAsync(string path,List<TeamMananger> coaches);
        Task<List<Player>> LoadPlayersAsync(string path,List<Club> clubs);
    }
}
