using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using TMHWPF.Business;

namespace WPFTMH.ViewModel
{
    public class ClubViewModel : BaseViewModel
    {
        private ObservableCollection<KeyValuePair<string, string>> _clubInfo;
        public ObservableCollection<KeyValuePair<string, string>> ClubInfo
        {
            get { return _clubInfo; }
            set
            {
                if (_clubInfo != value)
                {
                    _clubInfo = value;
                    OnPropertyChanged(nameof(ClubInfo));
                }
            }
        }

        private Visibility _clubDataGridVisibility;
        public Visibility ClubDataGridVisibility
        {
            get { return _clubDataGridVisibility; }
            set
            {
                if (_clubDataGridVisibility != value)
                {
                    _clubDataGridVisibility = value;
                    OnPropertyChanged(nameof(ClubDataGridVisibility));
                }
            }
        }

        public ClubViewModel()
        {
            ClubInfo = new ObservableCollection<KeyValuePair<string, string>>();
            ClubDataGridVisibility = Visibility.Collapsed;
        }

        public void LoadClubInfo(Club club, TeamManager coach)
        {
            ClubInfo.Clear();
            ClubInfo.Add(new KeyValuePair<string, string>("Название", club.Name));
            ClubInfo.Add(new KeyValuePair<string, string>("Страна", club.Country));
            ClubInfo.Add(new KeyValuePair<string, string>("Бюджет", club.Balance.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))));
            ClubInfo.Add(new KeyValuePair<string, string>("Тренер", coach.FirstName + " " + coach.LastName));
            ClubInfo.Add(new KeyValuePair<string, string>("Количество Игроков", club.CountPlayers.ToString()));
            ClubDataGridVisibility = Visibility.Visible;
        }
    }
}
