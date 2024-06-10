using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TMHWPF.Business;
using TMHWPF.DataAccess;

namespace WPFTMH.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly InitializationService _initilizationService;

        public LoginViewModel()
        {
            var dataBaseAccess = new DataBaseAccess();
            _initilizationService = new InitializationService(dataBaseAccess);
            AuthorizeCommand = new RelayCommand(async () => await Authorize(), CanAuthorize);
        }

        private string _login;
        public string Login
        {
            get
            {
                return _login;
            }

            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
                ((RelayCommand)AuthorizeCommand).RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((RelayCommand)AuthorizeCommand).RaiseCanExecuteChanged();
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private bool _isSuccessVisible;
        public bool IsSuccessVisible
        {
            get
            {
                return _isSuccessVisible;
            }

            set
            {
                _isSuccessVisible = value;
                OnPropertyChanged(nameof(IsSuccessVisible));
            }
        }

        private bool _isErrorVisible;
        public bool IsErrorVisible
        {
            get
            {
                return _isErrorVisible;
            }

            set
            {
                _isErrorVisible = value;
                OnPropertyChanged(nameof(IsErrorVisible));
            }
        }

        public ICommand AuthorizeCommand { get; }

        private bool CanAuthorize() => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);

        private async Task Authorize()
        {
            if (IsCorrectAuthorizationData(Login, Password))
            {
                try
                {
                    var (currentCoach, club) = await _initilizationService.InitializeTeamManangerAndClubAsync(
                        Login, Password);

                    IsSuccessVisible = true;
                    await Task.Delay(2500);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainWindow mainWindow = new MainWindow();
                        var mainWindowViewModel = (MainWindowViewModel)mainWindow.DataContext;
                        mainWindowViewModel.Initialize(currentCoach, club);
                        mainWindow.Show();
                        Application.Current.MainWindow.Close();
                    });
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsErrorVisible = true;
                    await Task.Delay(1000);
                    IsErrorVisible = false;
                }
            }
            else
            {
                Message = "Логин или пароль введены неверно!";
                IsErrorVisible = true;
                await Task.Delay(1000);
                IsErrorVisible = false;
            }
        }

        private bool IsCorrectAuthorizationData(string login, string password)
        {
            return !(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password));
        }
    }
}
