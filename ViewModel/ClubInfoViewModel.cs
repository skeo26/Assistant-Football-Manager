using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TMHWPF.Business;

namespace WPFTMH.ViewModel
{
    public class ClubViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KeyValuePair<string, string>> ClubInfo { get; set; }


        public ClubViewModel()
        {
            ClubInfo = new ObservableCollection<KeyValuePair<string, string>>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadClubInfo(Club club, TeamMananger coach)
        {
            ClubInfo.Clear();
            ClubInfo.Add(new KeyValuePair<string, string>("Название", club.Name));
            ClubInfo.Add(new KeyValuePair<string, string>("Страна", club.Country));
            ClubInfo.Add(new KeyValuePair<string, string>("Бюджет", club.Balance.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))));
            ClubInfo.Add(new KeyValuePair<string, string>("Тренер", coach.FirstName + " " + coach.LastName));
            ClubInfo.Add(new KeyValuePair<string, string>("Количество Игроков", club.CountPlayers.ToString()));
        }
    }
}
