using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ObjectsAfterDb
{
    class Cooperator
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public string Category { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public int Flat { get; set; }
        public DateTime BirthDay { get; set; }
        public double Salary { get; set; }
    }
}
