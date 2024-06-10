using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TMHWPF.Business;

namespace WPFTMH.ViewModel
{
    public class TeamViewModel : BaseViewModel
    {
        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set
            {
                if (_players != value)
                {
                    _players = value;
                    OnPropertyChanged(nameof(Players));
                }
            }
        }

        private Visibility _teamDataGridVisibility;
        public Visibility TeamDataGridVisibility
        {
            get { return _teamDataGridVisibility; }
            set
            {
                if (_teamDataGridVisibility != value)
                {
                    _teamDataGridVisibility = value;
                    OnPropertyChanged(nameof(TeamDataGridVisibility));
                }
            }
        }

        public TeamViewModel()
        {
            Players = new ObservableCollection<Player>();
            TeamDataGridVisibility = Visibility.Collapsed;
        }

        public void LoadTeamPlayers(Club currentClub)
        {
            var playerData = currentClub.TeamPlayers
                .Where(p => p.Team == currentClub)
                .Select(p => new Player.Builder()
                .SetFirstName(p.FirstName)
                .SetLastName(p.LastName)
                .SetTeam(currentClub)
                .SetNationality(p.Nationality)
                .SetAge(p.Age)
                .SetSkills(p.Skills)
                .SetPosition(p.Position)
                .SetSalary(p.Salary)
                .SetMarketValue(p.MarketValue)
                .SetReadyToTransfer(p.IsReadyToTransfer)
                .Build()).ToList();

            Players = new ObservableCollection<Player>(playerData);
        }
    }
}
