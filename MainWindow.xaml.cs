using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMHWPF.Business;
using WPFTMH.Views;
using TMHWPF.DataAccess;
using WPFTMH.ViewModel;
using System.Collections.ObjectModel;

namespace WPFTMH
{
    /// <summary>
    /// Interaction logic for MainWindow.xamlf
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private TeamViewModel _teamViewModel;
        private TransferViewModel _transferViewModel;
        private ClubViewModel _clubViewModel;


        private TeamMananger _currentCoach;
        private Club _currentClub;
        public string CoachInfo { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            _teamViewModel = new TeamViewModel();
            _transferViewModel = new TransferViewModel();
            _clubViewModel = new ClubViewModel();
            DataContext = _viewModel;
            TeamDataGrid.DataContext = _teamViewModel;
            TransferrablePlayersDataGrid.DataContext = _transferViewModel; 
            ClubDataGrid.DataContext = _clubViewModel;
        }

        public void Initialize(TeamMananger currentCoach, Club currentClub)
        {
            _currentCoach = currentCoach;
            _currentClub = currentClub;
            _viewModel.CoachInfo = $" Name: {currentCoach.FirstName} {currentCoach.LastName}, Age: {currentCoach.Age}, Team: {currentClub.Name}";
        }

        private void ClubInfo_Click(object sender, RoutedEventArgs e)
        {
            TransferDetailsButton.Visibility = Visibility.Collapsed;
            ExecuteTransferButton.Visibility = Visibility.Collapsed;
            TransferrablePlayersDataGrid.Visibility = Visibility.Collapsed;
            TeamDataGrid.Visibility = Visibility.Visible;
            _clubViewModel.LoadClubInfo(_currentClub, _currentCoach);
            ClubDataGrid.Visibility = Visibility.Visible;
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            TeamDataGrid.Visibility = Visibility.Collapsed;
            ClubDataGrid.Visibility = Visibility.Collapsed;
            _transferViewModel.AvailablePlayers = new ObservableCollection<Player>(TransferMarket.Instance.AvailablePlayers.Where(p => p.IsReadyToTransfer));
            TransferrablePlayersDataGrid.Visibility = Visibility.Visible;
            TransferrablePlayersDataGrid.SelectionChanged += TransferrablePlayersDataGrid_SelectionChanged;
        }

        private void TeamInfo_Click(object sender, RoutedEventArgs e)
        {
            TransferDetailsButton.Visibility = Visibility.Collapsed;
            ExecuteTransferButton.Visibility = Visibility.Collapsed;
            ClubDataGrid.Visibility = Visibility.Collapsed;
            TransferrablePlayersDataGrid.Visibility = Visibility.Collapsed;
            _teamViewModel.Players = new ObservableCollection<Player>(_currentClub.TeamPlayers.Where(p => p.Team == _currentClub));
            TeamDataGrid.Visibility = Visibility.Visible;
        }

        private void ExecuteTransfer_Click(object sender, RoutedEventArgs e)
        {
            TransferWindow dialog = new TransferWindow(_transferViewModel.SelectedPlayer, _currentClub);
            dialog.ShowDialog();
        }

        private void TransferrablePlayersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPlayer = TransferrablePlayersDataGrid.SelectedItem as Player;
            if (selectedPlayer != null)
            {
                TransferDetailsButton.Visibility = Visibility.Visible;
                ExecuteTransferButton.Visibility = Visibility.Visible; 
            }
            else
            {
                TransferDetailsButton.Visibility= Visibility.Collapsed;
                ExecuteTransferButton.Visibility = Visibility.Collapsed; 
            }
        }

        private void TransferDetails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Детали трансферов\n " +
                "1. Вы не можете подписать своего же игрока!\n" +
                "2. Минимальный остаток бюджета клуба 150млн$!\n" +
                "3. Число игроков в клубе не может быть больше 28!\n");
        }
    }
}
