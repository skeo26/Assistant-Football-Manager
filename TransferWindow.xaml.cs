using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TMHWPF.Business;
using TMHWPF.DataAccess;

namespace WPFTMH
{
    /// <summary>
    /// Логика взаимодействия для TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        private Player _selectedPlayer;
        private Club _currentClub;

        public TransferWindow(Player selectedPlayer, Club currentClub)
        {
            InitializeComponent();
            _selectedPlayer = selectedPlayer;
            _currentClub = currentClub;
        }
        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AmountBox.Text, out var transferAmount) || transferAmount <= 0)
            {
                MessageBox.Show("Неверная сумма трансфера");
                return;
            }
            if (!int.TryParse(SalaryBox.Text, out var salary) || salary <= 0)
            {
                MessageBox.Show("Неверная зарплата");
                return;
            }

            var transferMarket = TransferMarket.Instance;
            var observerManager = new ObserverMananger(); 
            var dataSaver = new DataSaver();
            observerManager.Attach(dataSaver);

            Player player = FindPlayerByName(_selectedPlayer, transferMarket);

            if (player == null || PlayerIsMyTeam(player, _currentClub))
            {
                MessageBox.Show("Трансфер невозможен. Игрок уже в вашей команде или не найден.");
                this.Close();
                return;
            }

            ITransferService transferService = new TransferService(transferMarket);
            _currentClub.TransferService = transferService;
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;
            bool isTransferSuccessful = await _currentClub.TryTransferPlayer(player, transferAmount, salary);

            ProgressBar.Visibility = Visibility.Collapsed;

            if (isTransferSuccessful)
            {
                _currentClub.AddPlayer(player);
                observerManager.Notify(ApplicationState.Instance.ClubsFilePath, ApplicationState.Instance.Clubs);
                observerManager.Notify(ApplicationState.Instance.PlayersFilePath, ApplicationState.Instance.Players);
                MessageBox.Show("Трансфер успешно выполнен!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при выполнении трансфера.");
            }
        }


        private static bool PlayerIsMyTeam(Player player, Club club)
        {
            return player.Team == club;
        }

        private static Player FindPlayerByName(Player playerName, TransferMarket transferMarket)
        {
            return transferMarket.AvailablePlayers.FirstOrDefault(p => p.FirstName == playerName.FirstName && p.LastName == playerName.LastName);
        }
    }
}

