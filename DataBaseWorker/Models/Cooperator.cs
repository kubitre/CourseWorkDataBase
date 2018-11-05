using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAgregator.Models
{
    class Cooperator
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int StreetId { get; set; }
        public int Building { get; set; }
        public int Flat { get; set; }
        public DateTime BirthDay { get; set; }
        public double Salary { get; set; }
        public int PositionId { get; set; }
        public int CategoryId { get; set; }
    }
}
