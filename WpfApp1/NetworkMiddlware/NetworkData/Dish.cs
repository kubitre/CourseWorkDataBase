using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.NetworkMiddlware.NetworkData
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string DishName { get; set; }
        public string Recipe { get; set; }
        public double OutPrice { get; set; }
    }
}
