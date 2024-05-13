using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TMHWPF.Business
{
    public class InitilizationService
    {
        private readonly ICoachProvider coachProvider;
        private readonly ITeamDataProvider teamDataProvider;
        private readonly IUserProvider userProvider;

        public InitilizationService(ICoachProvider coachProvider, ITeamDataProvider teamDataProvider, IUserProvider userProvider)
        {
            this.coachProvider = coachProvider;
            this.teamDataProvider = teamDataProvider;
            this.userProvider = userProvider;
        }

        public async Task<(TeamMananger, Club)> InitializeTeamManangerAndClubAsync(string coachesFilePath, string clubsFilePath, string playersFilePath, string usersFilePath, string login, string password)
        {
            var appState = ApplicationState.Instance;
            var users = await userProvider.LoadUsersAsync(usersFilePath);
            var user = users.FirstOrDefault(u => u.Login == login && u.Password == password)
                            ?? throw new Exception("Ошибка аутентификации.");

            var coaches = await coachProvider.LoadCoachesAsync(coachesFilePath);
            var coach = coaches.FirstOrDefault(c => c.FirstName == user.FirstName && c.LastName == user.LastName)
                            ?? throw new Exception("Тренер для данного пользователя не найден.");

            var clubs = await teamDataProvider.LoadClubsAsync(clubsFilePath, coaches);
            appState.Clubs = clubs;
            var club = clubs.FirstOrDefault(c => c.Coach.LastName == coach.LastName)
                            ?? throw new Exception("Клуб для данного тренера не найден.");

            var players = await teamDataProvider.LoadPlayersAsync(playersFilePath, clubs);
            appState.Players = players;
            club.SetPlayers(players.Where(p => p.Team == club).ToList());

            var listPlayers = players.Where(x => x.Team.Name == club.Name).ToList();

            var teamMananger = new TeamMananger.Builder()
                    .SetFirstName(coach.FirstName)
                    .SetLastName(coach.LastName)
                    .SetAge(coach.Age)
                    .SetClub(club)
                    .SetPlayersTeam(listPlayers)
                    .Build();

            var transferMarket = TransferMarket.Instance;
            transferMarket.AvailablePlayers = players.Where(p => p.IsReadyToTransfer == true).ToList();

            return (teamMananger, club);
        }
    }
}
