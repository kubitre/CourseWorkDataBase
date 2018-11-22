namespace DatabaseMiddlware.Models
{
    using System;

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
