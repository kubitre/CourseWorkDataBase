using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.SignalsData
{
    [Serializable]
    public class MenuData
    {
        public string Name { get; set; }
        public string Coocker { get; set; }
        public List<string> Dishes { get; set; }
        public DateTime Date { get; set; }
    }
}
