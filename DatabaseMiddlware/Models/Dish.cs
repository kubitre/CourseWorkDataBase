namespace DatabaseMiddlware.Models
{
    using System;

    public class Dish
    {
        public Guid Id { get; set; }
        public Cooperator Cooperator { get; set; }
        public string Name { get; set; }
    }
}
