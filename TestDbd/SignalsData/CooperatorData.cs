using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDB.SignalsData
{
    [Serializable]
    public class CooperatorData
    {
        public string FistName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirtDay { get; set; }
        public double Salary { get; set; }
        public string Position { get; set; }
        public string Category { get; set; }
    }
}
