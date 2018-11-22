namespace DatabaseMiddlware.Models
{
    using System;

    public class Recipe
    {
        public Guid Id { get; set; }
        public Dish Dish { get; set; }
        public Product Product { get; set; }
        public double Amount { get; set; }
    }
}
