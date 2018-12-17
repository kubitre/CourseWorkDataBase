using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Menu : INotifyPropertyChanged
    {
        //Fields 
        private Guid id;
        private string name;
        private string dish;
        private string calculator;
        private string outer;
        private DateTime date;

        //
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
        public string Dish
        {
            get
            {
                return dish;
            }
            set
            {
                dish = value;
                OnPropertyChanged("Dish");
            }
        }
        public string Calculator
        {
            get
            {
                return calculator;
            }
            set
            {
                calculator = value;
                OnPropertyChanged("Calculator");
            }
        }

        public string Outer
        {
            get
            {
                return outer;
            }
            set
            {
                outer = value;
                OnPropertyChanged("Outer");
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
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
