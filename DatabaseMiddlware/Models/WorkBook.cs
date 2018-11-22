namespace DatabaseMiddlware.Models
{
    using System;

    public class WorkBook
    {
        public Guid Id { get; set; }
        public Cooperator Cooperator { get; set; }
        public Position Position { get; set; }
        public int OrderNumber { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}
