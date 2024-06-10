using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TMHWPF.Business;

namespace WPFTMH.ViewModel
{
    public class TransferViewModel : BaseViewModel
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
                    if (_selectedPlayer != null)
                    {
                        TransferDetailsButtonVisibility = Visibility.Visible;
                    }
                    else
                    {
                        TransferDetailsButtonVisibility = Visibility.Collapsed;
                    }
                    if (_selectedPlayer != null)
                    {
                        ExecuteTransferButtonVisibility = Visibility.Visible;
                    }
                    else
                    {
                        ExecuteTransferButtonVisibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private Visibility _transferrablePlayersDataGridVisibility;
        public Visibility TransferrablePlayersDataGridVisibility
        {
            get { return _transferrablePlayersDataGridVisibility; }
            set
            {
                if (_transferrablePlayersDataGridVisibility != value)
                {
                    _transferrablePlayersDataGridVisibility = value;
                    OnPropertyChanged(nameof(TransferrablePlayersDataGridVisibility));
                }
            }
        }

        private Visibility _transferDetailsButtonVisibility;
        public Visibility TransferDetailsButtonVisibility
        {
            get { return _transferDetailsButtonVisibility; }
            set
            {
                if (_transferDetailsButtonVisibility != value)
                {
                    _transferDetailsButtonVisibility = value;
                    OnPropertyChanged(nameof(TransferDetailsButtonVisibility));
                }
            }
        }

        private Visibility _executeTransferButtonVisibility;
        public Visibility ExecuteTransferButtonVisibility
        {
            get { return _executeTransferButtonVisibility; }
            set
            {
                if (_executeTransferButtonVisibility != value)
                {
                    _executeTransferButtonVisibility = value;
                    OnPropertyChanged(nameof(ExecuteTransferButtonVisibility));
                }
            }
        }

        public ICommand ExecuteTransferCommand { get; private set; }
        public ICommand LoadPlayersCommand { get; private set; }

        public TransferViewModel()
        {
            AvailablePlayers = new ObservableCollection<Player>();
            TransferrablePlayersDataGridVisibility = Visibility.Collapsed;
            TransferDetailsButtonVisibility = Visibility.Collapsed;
            ExecuteTransferButtonVisibility = Visibility.Collapsed;

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

        public void LoadAvailablePlayers()
        {
            AvailablePlayers = new ObservableCollection<Player>(TransferMarket.Instance.AvailablePlayers.Where(p => p.IsReadyToTransfer));
        }
    }
}
