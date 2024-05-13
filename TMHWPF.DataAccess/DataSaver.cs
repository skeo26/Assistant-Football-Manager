using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace TMHWPF.DataAccess
{
    public class DataSaver : IObserver
    {

        public void SaveClubsToFile(List<Club> clubs, string path)
        {
            var lines = new List<string>
            {
                "Club                  | Country       | Balance     | Coach            | Players"
            };

            foreach (var club in clubs)
            {
                lines.Add($"{club.Name,-20} | {club.Country,-12} | {club.Balance,10} | {club.Coach.LastName,-15} | {club.CountPlayers,7}");
            }

            File.WriteAllLines(path, lines);
        }


        public void SavePlayersToFile(List<Player> players, string filePath)
        {
            var lines = new List<string>
            {
                "Club                  | First Name         | Last Name          | Nationality   | Age | Skills | Position | Salary     | Market Value | Transfer"
            };

            foreach (var player in players)
            {
                lines.Add($"{player.Team.Name,-20} | {player.FirstName,-18} | {player.LastName,-18} | " +
                          $"{player.Nationality,-13} | {player.Age,3} | {player.Skills,6} | {player.Position,-8} | " +
                          $"{player.Salary,10} | {player.MarketValue,12} | {player.IsReadyToTransfer,8}");
            }

            File.WriteAllLines(filePath, lines);
        }

        public void Update(string filePath, object data)
        {
            var appState = ApplicationState.Instance;
            if (filePath == appState.ClubsFilePath)
            {
                SaveClubsToFile((List<Club>)data, filePath);
            }
            else if (filePath == appState.PlayersFilePath)
            {
                SavePlayersToFile((List<Player>)data, filePath);
            }
        }
    }
}
