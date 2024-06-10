using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TMHWPF.Business;
using TMHWPF.DataAccess;

namespace WPFTMH.ViewModel
{
    public class TransferWindowViewModel : BaseViewModel
    {
        
        private Player _selectedPlayer;
        private Club _currentClub;
        public ICommand SubmitCommand { get; }

        public TransferWindowViewModel(Player selectedPlayer, Club currentClub)
        {
            _selectedPlayer = selectedPlayer;
            _currentClub = currentClub;
            SubmitCommand = new RelayCommand(async () => await SubmitAsync());
        }

        private string _amount;
        public string Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private string _salary;
        public string Salary
        {
            get
            {
                return _salary;
            }

            set
            {
                _salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        private bool _isProgressBarVisible;
        public bool IsProgressBarVisible
        {
            get
            {
                return _isProgressBarVisible;
            }

            set
            {
                _isProgressBarVisible = value;
                OnPropertyChanged(nameof(IsProgressBarVisible));
            }
        }


        public async Task<bool> SubmitAsync()
        {
            if (!int.TryParse(Amount, out var transferAmount) || transferAmount <= 0)
            {
                MessageBox.Show("Неверная сумма трансфера");
                return false;
            }
            if (!int.TryParse(Salary, out var salary) || salary <= 0)
            {
                MessageBox.Show("Неверная зарплата");
                return false;
            }

            var transferMarket = TransferMarket.Instance;
            var observerManager = new ObserverManager();
            var dataSaver = new DataBaseSaver();
            var dataAccess = new DataBaseAccess();
            observerManager.Attach(dataSaver);

            Player player = TransferMarket.FindPlayerByName(_selectedPlayer, transferMarket);

            if (player == null || TransferMarket.PlayerIsMyTeam(player, _currentClub))
            {
                MessageBox.Show("Трансфер невозможен. Игрок уже в вашей команде или не найден.");
                return false; 
            }

            int originalClubId = player.ClubID;

            ITransferService transferService = new TransferService(transferMarket);
            _currentClub.TransferService = transferService;

            IsProgressBarVisible = true;

            bool isTransferSuccessful;
            try
            {
                await Task.Delay(1000); 
                isTransferSuccessful = await _currentClub.TryTransferPlayer(player, transferAmount, salary);
            }
            finally
            {
                IsProgressBarVisible = false;
            }

            if (isTransferSuccessful)
            {
                observerManager.Notify("SavePlayer", player);
                observerManager.Notify("SaveClub", _currentClub);

                var oldClub = ApplicationState.Instance.Clubs.FirstOrDefault(x => x.ClubID == originalClubId);
                if (oldClub == null)
                {
                    oldClub = await dataAccess.LoadClubByIdAsync(originalClubId);
                    if (oldClub == null)
                    {
                        throw new InvalidOperationException("Не удалось обновить предыдущий клуб игрока");
                    }
                    else
                    {
                        observerManager.Notify("SaveClub", oldClub);
                    }
                }

                var transfer = new Transfer(player.PlayerID, originalClubId, _currentClub.ClubID, transferAmount, salary);
                observerManager.Notify("SaveTransfer", transfer);

                MessageBox.Show("Трансфер успешно выполнен!");
                return true;
            }
            else
            {
                MessageBox.Show("Трансфер не выполнен.");
                return false;
            }
        }
    }
}
