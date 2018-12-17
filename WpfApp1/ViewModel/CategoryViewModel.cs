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
    public class CategoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Category> Categories { get; set; }
        private Models.Category selectedCategory;

        public Models.Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public CategoryViewModel()
        {
            this.Categories = new ObservableCollection<Models.Category>();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
