using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.NetworkMiddleware.NetworkData
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }
        public Cooperator Cooker { get; set; }
    }
}
