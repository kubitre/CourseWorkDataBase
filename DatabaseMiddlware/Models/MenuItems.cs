namespace DatabaseMiddlware.Models
{
    using System;

    public class MenuItems
    {
        public Guid Id { get; set; }
        public Menu Menu { get; set; }
        public double Outer { get; set; }
        public Dish Dish { get; set; }
    }
}
