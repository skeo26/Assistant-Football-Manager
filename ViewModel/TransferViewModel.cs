using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMHWPF.Business;

namespace WPFTMH.ViewModel
{
    public class TransferViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Player> _availablePlayers;
        public ObservableCollection<Player> AvailablePlayers
        {
            get { return _availablePlayers; }
            set
            {
                _availablePlayers = value;
                OnPropertyChanged(nameof(AvailablePlayers));
            }
        }

        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                if (_selectedPlayer != value)
                {
                    _selectedPlayer = value;
                    OnPropertyChanged(nameof(SelectedPlayer));
                    ((RelayCommand)ExecuteTransferCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand ExecuteTransferCommand { get; private set; }
        public ICommand LoadPlayersCommand { get; private set; }

        public TransferViewModel()
        {
            ExecuteTransferCommand = new RelayCommand(ExecuteTransfer, CanExecuteTransfer);
            LoadPlayersCommand = new RelayCommand(LoadAvailablePlayers);
        }

        private bool CanExecuteTransfer()
        {
            return SelectedPlayer != null && SelectedPlayer.IsReadyToTransfer;
        }

        private void ExecuteTransfer()
        {
            
        }

        private void LoadAvailablePlayers()
        {
            AvailablePlayers = new ObservableCollection<Player>(TransferMarket.Instance.AvailablePlayers.Where(p => p.IsReadyToTransfer));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
