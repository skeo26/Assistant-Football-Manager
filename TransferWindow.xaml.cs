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
using WPFTMH.ViewModel;

namespace WPFTMH
{
    /// <summary>
    /// Логика взаимодействия для TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        public TransferWindow(Player selectedPlayer, Club currentClub)
        {
            InitializeComponent();
            DataContext = new TransferWindowViewModel(selectedPlayer, currentClub);
        }
    }      
}

