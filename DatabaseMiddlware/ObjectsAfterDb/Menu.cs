using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ObjectsAfterDb
{
    class Menu
    {
        public Guid Id { get; set; }
        public List<Dish> Dishes { get; set; }
        public Cooperator Coocker { get; set; }
        public DateTime Date { get; set; }
    }
}
