using AdminPanel.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdminPanel.ViewModel
{
    public class StreetViewModel : INotifyPropertyChanged
    {
        private Street selectedStreet;
        public ObservableCollection<Street> Streets { get; set; }

        public Street SelectedStreet
        {
            get
            {
                return selectedStreet;
            }
            set
            {
                selectedStreet = value;
                OnPropertyChanged("SelectedStreet");
            }
        }

        public StreetViewModel()
        {
            this.Streets = new ObservableCollection<Street>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
