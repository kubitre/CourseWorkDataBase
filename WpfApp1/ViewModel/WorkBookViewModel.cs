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
    public class WorkBookViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.WorkBook> WorkBooks { get; set; }
        private Models.WorkBook selectedWorkBook;

        public Models.WorkBook SelectedWorkBook
        {
            get
            {
                return selectedWorkBook;
            }
            set
            {
                selectedWorkBook = value;
                OnPropertyChanged("SelectedWorkBook");
            }
        }

        public WorkBookViewModel()
        {
            this.WorkBooks = new ObservableCollection<Models.WorkBook>();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
