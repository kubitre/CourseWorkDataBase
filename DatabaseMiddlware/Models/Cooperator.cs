namespace DatabaseMiddlware.Models
{
    using System;

    public class Cooperator
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Position Position { get; set; }
        public Category Category { get; set; }
        public Street Street { get; set; }
        public int Building { get; set; }
        public int Flat { get; set; }
        public DateTime BirthDay { get; set; }
        public double Salary { get; set; }
    }
}
