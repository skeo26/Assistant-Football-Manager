using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTMH.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
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
                    OnPropertyChanged("CoachInfo");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
