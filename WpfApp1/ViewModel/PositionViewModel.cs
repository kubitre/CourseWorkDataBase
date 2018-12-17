using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdminPanel.ViewModel
{
    public class PositionViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Position> Positions { get; set; }
        private Models.Position selectedPosition;
        
        public Models.Position SelectedPosition
        {
            get
            {
                return selectedPosition;
            }
            set
            {
                selectedPosition = value;
                OnPropertyChanged("SelectedPosition");
            }
        }

        public PositionViewModel()
        {
            this.Positions = new ObservableCollection<Models.Position>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
