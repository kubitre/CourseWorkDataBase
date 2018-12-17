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
    public class CooperatorViewModel : INotifyPropertyChanged
    {
        private Cooperator selectedCooperator;
        public ObservableCollection<Cooperator> Cooperators { get; set; }

        public Cooperator SelectedCooperator
        {
            get
            {
                return selectedCooperator;
            }
            set
            {
                selectedCooperator = value;
                OnPropertyChanged("SelectedCooperator");
            }
        }

        public CooperatorViewModel()
        {
            this.Cooperators = new ObservableCollection<Cooperator>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
