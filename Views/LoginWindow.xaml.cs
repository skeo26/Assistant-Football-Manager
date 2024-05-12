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
using System.Windows.Shapes;
using TMHWPF.Business;
using TMHWPF.DataAccess;

namespace WPFTMH.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly InitilizationService _initilizationService;
        private readonly ApplicationState _appState;

        public LoginWindow()
        {
            InitializeComponent();
            var dataLoader = new DataLoader();
            _initilizationService = new InitilizationService(dataLoader, dataLoader, dataLoader);
            _appState = ApplicationState.Instance;
            _appState.ClubsFilePath = FilePathConfig.ClubsFilePath;
            _appState.CoachesFilePath = FilePathConfig.CoachesFilePath;
            _appState.PlayersFilePath = FilePathConfig.PlayersFilePath;
            _appState.UsersFilePath = FilePathConfig.UsersFilePath;
        }

        private async void Authorisation_Click(object sender, RoutedEventArgs e)
        {
            string login = AuthorisationLoginTextBox.Text;
            string password = AuthorisationPasswordTextBox.Text;
            if (IsCorrectAuthorizationData(login, password))
            {
                try
                {
                    var (currentCoach, club) = await _initilizationService.InitializeTeamManangerAndClubAsync(
                        _appState.CoachesFilePath,
                        _appState.ClubsFilePath,
                        _appState.PlayersFilePath,
                        _appState.UsersFilePath,
                        login, password);

                    SuccessLabel.Visibility = Visibility.Visible;
                    await Task.Delay(2500);

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Initialize(currentCoach, club);
                    mainWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    ErrorLabel.Content = ex.Message; 
                    ErrorLabel.Visibility = Visibility.Visible;
                    AuthorisationLoginTextBox.Clear();
                    AuthorisationPasswordTextBox.Clear();
                    await Task.Delay(1000);
                    ErrorLabel.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                ErrorLabel.Content = "Please enter both username and password.";
                ErrorLabel.Visibility = Visibility.Visible;
                AuthorisationLoginTextBox.Clear();
                AuthorisationPasswordTextBox.Clear();
                await Task.Delay(1000);
                ErrorLabel.Visibility = Visibility.Hidden;
            }
        }

        private bool IsCorrectAuthorizationData(string login, string password)
        {
            return !(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password));
        }
    }
}
