using System;
using System.Collections.Generic;

namespace DatabaseMiddlware.ObjectsAfterDb
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Products { get; set; }
        public double Outer { get; set; }
    }
}
