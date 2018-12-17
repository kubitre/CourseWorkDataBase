using AdminPanel.Models;
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
    public class MenuViewModel : INotifyPropertyChanged
    {
        private Menu selectedMenu;
        public ObservableCollection<Menu> Menus { get; set; }

        public Menu SelectedMenu
        {
            get
            {
                return selectedMenu;
            }
            set
            {
                selectedMenu = value;
                OnPropertyChanged("SelectedMenu");
            }
        }

        public MenuViewModel()
        {
            this.Menus = new ObservableCollection<Menu>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
