using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModel
{
    public class DishViewModel: INotifyPropertyChanged
    {
        private Models.Dish selectedDish;
        public ObservableCollection<Models.Dish> Dishes { get; set; }

        public Models.Dish SelectedDish
        {
            get
            {
                return selectedDish;
            }
            set
            {
                selectedDish = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public DishViewModel()
        {
            Dishes = new ObservableCollection<Models.Dish>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
