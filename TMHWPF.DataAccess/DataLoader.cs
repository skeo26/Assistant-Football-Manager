using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace TMHWPF.DataAccess
{
    public class DataLoader : ICoachProvider, ITeamDataProvider, IUserProvider
    {

        public async Task<List<User>> LoadUsersAsync(string path)
        {
            var lines = await File.ReadAllLinesAsync(path);
            return lines.Skip(1).Select(ParseUser).ToList();
        }

        private User ParseUser(string line)
        {
            var fields = line.Split('|').Select(f => f.Trim()).ToArray();
            return new User.Builder()
                .SetLogin(fields[0])
                .SetPassword(fields[1])
                .SetFirstName(fields[2])
                .SetLastName(fields[3])
                .Build();
        }

        public async Task<List<TeamMananger>> LoadCoachesAsync(string path)
        {
            var lines = await File.ReadAllLinesAsync(path);
            return lines.Skip(1).Select(ParseCoach).ToList();
        }

        private TeamMananger ParseCoach(string line)
        {
            var fields = line.Split('|').Select(f => f.Trim()).ToArray();
            return new TeamMananger.Builder()
                .SetFirstName(fields[0])
                .SetLastName(fields[1])
                .SetAge(int.Parse(fields[2]))
                .Build();
        }

        public async Task<List<Club>> LoadClubsAsync(string path, List<TeamMananger> coaches)
        {
            var lines = await File.ReadAllLinesAsync(path);
            return lines.Skip(1).Select(line => ParseClub(line, coaches)).ToList();
        }

        private Club ParseClub(string line, List<TeamMananger> coaches)
        {
            var fields = line.Split('|').Select(f => f.Trim()).ToArray();
            var coach = coaches.FirstOrDefault(c => c.LastName == fields[3]);
            return new Club.Builder()
                .SetName(fields[0])
                .SetCountry(fields[1])
                .SetBalance(double.Parse(fields[2]))
                .SetCoach(coach)
                .SetCountPlayers(int.Parse(fields[4]))
                .Build();
        }

        public async Task<List<Player>> LoadPlayersAsync(string path, List<Club> clubs)
        {
            var lines = await File.ReadAllLinesAsync(path);
            return lines.Skip(1).Select(line => ParsePlayer(line, clubs)).ToList();
        }

        private Player ParsePlayer(string line, List<Club> clubs)
        {
            var fields = line.Split('|').Select(f => f.Trim()).ToArray();
            var club = clubs.FirstOrDefault(c => c.Name == fields[0]);
            return new Player.Builder()
                .SetFirstName(fields[1])
                .SetLastName(fields[2])
                .SetNationality(fields[3])
                .SetAge(int.Parse(fields[4]))
                .SetSkills(int.Parse(fields[5]))
                .SetPosition(fields[6])
                .SetSalary(double.Parse(fields[7]))
                .SetMarketValue(double.Parse(fields[8]))
                .SetTeam(club)
                .SetReadyToTransfer(bool.Parse(fields[9]))
                .Build();
        }
    }
}
