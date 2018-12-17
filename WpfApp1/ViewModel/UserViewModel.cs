using AdminPanel.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdminPanel.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User selectedUser;
        public ObservableCollection<User> Users { get; set; }

        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public UserViewModel()
        {
            this.Users = new ObservableCollection<User>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
