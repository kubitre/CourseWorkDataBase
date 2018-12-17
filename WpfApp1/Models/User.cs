using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdminPanel.Models
{
    public class User : INotifyPropertyChanged
    {
        //Fileds
        private Guid id;
        private string name;
        private string cooperator;
        private DateTime lastEnter;
        private string role;

        //properties
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
        public DateTime LastEnter
        {
            get
            {
                return lastEnter;
            }
            set
            {
                lastEnter = value;
                OnPropertyChanged("LastEnter");
            }
        }
        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                OnPropertyChanged("Role");
            }
        }

        public string Cooperator
        {
            get
            {
                return cooperator;
            }
            set
            {
                cooperator = value;
                OnPropertyChanged("Cooperator");
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
