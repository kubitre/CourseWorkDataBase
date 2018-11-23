using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.NetworkMiddlware.NetworkData
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }
        public Cooperator Cooker { get; set; }
    }
}
