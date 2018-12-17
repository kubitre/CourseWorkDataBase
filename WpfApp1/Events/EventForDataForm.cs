using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Events
{
    public static class EventForDataForm
    {

        public delegate void AddNewData<T>(List<T> data);
    }
}
