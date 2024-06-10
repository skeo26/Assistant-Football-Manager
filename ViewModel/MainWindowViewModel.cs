using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TMHWPF.Business;
using WPFTMH.Views;

namespace WPFTMH.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _coachInfo;
        public string CoachInfo
        {
            get { return _coachInfo; }
            set
            {
                if (_coachInfo != value)
                {
                    _coachInfo = value;
                    OnPropertyChanged(nameof(CoachInfo));
                }
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        public TeamViewModel TeamViewModel { get; set; }
        public TransferViewModel TransferViewModel { get; set; }
        public ClubViewModel ClubViewModel { get; set; }

        public ICommand ShowClubInfoCommand { get; }
        public ICommand ShowTransfersCommand { get; }
        public ICommand ShowTeamInfoCommand { get; }
        public ICommand ShowTransferDetailsCommand { get; }
        public ICommand ShowTransferCommand { get; private set; }
        public ICommand CloseDetailsCommand { get; private set; }

        private TeamManager _currentCoach;
        private Club _currentClub;

        public MainWindowViewModel()
        {
            
            TeamViewModel = new TeamViewModel();
            TransferViewModel = new TransferViewModel();
            ClubViewModel = new ClubViewModel();

            CurrentView = null;
            ShowClubInfoCommand = new RelayCommand(ShowClubInfo);
            ShowTransfersCommand = new RelayCommand(ShowTransfers);
            ShowTeamInfoCommand = new RelayCommand(ShowTeamInfo);
            ShowTransferDetailsCommand = new RelayCommand(ShowTransferDetails);
            CloseDetailsCommand = new RelayCommand(CloseDetails);
            ShowTransferCommand = new RelayCommand(ShowTransfer);

        }

        public void Initialize(TeamManager currentCoach, Club currentClub)
        {
            _currentCoach = currentCoach;
            _currentClub = currentClub;
            CoachInfo = $" Name: {currentCoach.FirstName} {currentCoach.LastName}, Age: {currentCoach.Age}, Team: {currentClub.Name}";
        }

        private void ShowClubInfo()
        {
            TransferViewModel.TransferDetailsButtonVisibility = Visibility.Collapsed;
            TransferViewModel.ExecuteTransferButtonVisibility = Visibility.Collapsed;
            ClubViewModel.LoadClubInfo(_currentClub, _currentCoach);
        }

        private void ShowTransfers()
        {
            TeamViewModel.TeamDataGridVisibility = Visibility.Collapsed;
            ClubViewModel.ClubDataGridVisibility = Visibility.Collapsed;
            TransferViewModel.LoadAvailablePlayers();
            TransferViewModel.TransferrablePlayersDataGridVisibility = Visibility.Visible;
        }

        private void ShowTeamInfo()
        {
            TransferViewModel.TransferDetailsButtonVisibility = Visibility.Collapsed;
            TransferViewModel.ExecuteTransferButtonVisibility = Visibility.Collapsed;
            ClubViewModel.ClubDataGridVisibility = Visibility.Collapsed;
            TransferViewModel.TransferrablePlayersDataGridVisibility = Visibility.Collapsed;
            TeamViewModel.LoadTeamPlayers(_currentClub);
            TeamViewModel.TeamDataGridVisibility = Visibility.Visible;
        }
        
        private void ShowTransfer()
        {
            var selectedPlayer = TransferViewModel.SelectedPlayer;
            if (selectedPlayer != null)
            {
                TransferWindow dialog = new TransferWindow(selectedPlayer, _currentClub);
                dialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Игрок для трансфера не выбран!");
            }
        }

        private void ShowTransferDetails()
        {
            CurrentView = new DetailsTransferWindow();
        }

        private void CloseDetails()
        {
            CurrentView = null;
        }
    }
}
