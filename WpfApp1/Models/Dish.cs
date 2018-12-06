using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Dish : INotifyPropertyChanged
    {
        private Guid id;
        private string name;
        private string recipe;
        private string outPrice;

        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Recipe
        {
            get
            {
                return recipe;
            }
            set
            {
                recipe = value;
                OnPropertyChanged("Recipe");
            }
        }

        public string OutPrice
        {
            get
            {
                return outPrice;
            }
            set
            {
                outPrice = value;
                OnPropertyChanged("OutPrice");
            }
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
