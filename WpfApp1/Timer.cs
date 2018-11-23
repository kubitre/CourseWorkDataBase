using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class Timer : INotifyPropertyChanged
    {
        private string timeOutP;

        public string TimeOutP
        {
            get
            {
                return timeOutP;
            }
            set
            {   

                this.timeOutP = $" {value}" ;
                OnProperyChanged("TimeOutP");
            }
        }

        public void ChangeTime(int time) => this.TimeOutP = prefix + $" {time}" + postfix;
        

        public static string prefix = "Переподключение через";
        public static string postfix = " секунд";


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperyChanged([CallerMemberName]string prop = "te")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
