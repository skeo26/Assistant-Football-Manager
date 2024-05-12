using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace WPFTMH.ViewModel
{
    public class TeamViewModel : INotifyPropertyChanged
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

        public TeamViewModel()
        {
            Players = new ObservableCollection<Player>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
