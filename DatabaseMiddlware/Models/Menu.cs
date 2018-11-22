namespace DatabaseMiddlware.Models
{
    using System;

    public class Menu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Cooperator Cooperator { get; set; }
        public DateTime Date { get; set; }
    }
}
